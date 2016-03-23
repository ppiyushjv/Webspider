using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Data.DatabaseManager
{
    public static class DataParser
    {
        #region [Parser]
        /// <summary>
        /// Converts datatable to list<T> dynamically
        /// </summary>
        /// <typeparam name="T">Class name</typeparam>
        /// <param name="dataTable">data table to convert</param>
        /// <returns>List<T></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {
                        if (dataRow[dtField.Name] == DBNull.Value)
                            propertyInfos.SetValue(classObj, null, null);
                        else
                        {
                            //Convert.ChangeType does not handle conversion to nullable types
                            //if the property type is nullable, we need to get the underlying type of the property
                            var targetType = IsNullableType(propertyInfos.PropertyType) ? Nullable.GetUnderlyingType(propertyInfos.PropertyType) : propertyInfos.PropertyType;

                            //Returns an System.Object with the specified System.Type and whose value is
                            //equivalent to the specified object.
                            var propertyVal = dataRow[dtField.Name];
                            propertyVal = Convert.ChangeType(propertyVal, targetType);

                            propertyInfos.SetValue(classObj, propertyVal, null);
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
        #endregion

        //#region [Backup]
        //if (propertyInfos.PropertyType == typeof(DateTime))
        //{
        //    propertyInfos.SetValue
        //    (classObj, convertToDateTime(dataRow[dtField.Name]), null);
        //}
        //else if (propertyInfos.PropertyType == typeof(int))
        //{
        //    propertyInfos.SetValue
        //    (classObj, ConvertToInt(dataRow[dtField.Name]), null);
        //}
        //else if (propertyInfos.PropertyType == typeof(long))
        //{
        //    propertyInfos.SetValue
        //    (classObj, ConvertToLong(dataRow[dtField.Name]), null);
        //}
        //else if (propertyInfos.PropertyType == typeof(decimal))
        //{
        //    propertyInfos.SetValue
        //    (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
        //}
        //else if (propertyInfos.PropertyType == typeof(String))
        //{
        //    if (dataRow[dtField.Name].GetType() == typeof(DateTime))
        //    {
        //        propertyInfos.SetValue
        //        (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
        //    }
        //    else
        //    {
        //        propertyInfos.SetValue
        //        (classObj, ConvertToString(dataRow[dtField.Name]), null);
        //    }
        //}
        //#endregion


        //#region [Helper Methods]


        //private static string ConvertToDateString(object date)
        //{
        //    if (date == null)
        //        return string.Empty;

        //    return SpecialDateTime.ConvertDate(Convert.ToDateTime(date));
        //}

        //private static string ConvertToString(object value)
        //{
        //    return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
        //}

        //private static int ConvertToInt(object value)
        //{
        //    return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
        //}

        //private static long ConvertToLong(object value)
        //{
        //    return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
        //}

        //private static decimal ConvertToDecimal(object value)
        //{
        //    return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
        //}

        //private static DateTime convertToDateTime(object date)
        //{
        //    return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        //}
        //#endregion


        #region [ To DataTable ]
        // List<int> ListOfInt;
        // DataTable ListTable = BuildDataTable<int>(ListOfInt);
        public static DataTable BuildDataTable<T>(IList<T> lst)
        {
            try
            {
                //create DataTable Structure
                DataTable tbl = CreateTable<T>();
                Type entType = typeof(T);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
                //get the list item and add into the list
                foreach (T item in lst)
                {
                    DataRow row = tbl.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    tbl.Rows.Add(row);
                }
                return tbl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DataTable CreateTable<T>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                //add property as column
                tbl.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType.UnderlyingSystemType) ?? prop.PropertyType.UnderlyingSystemType);
            }
            return tbl;
        }
        #endregion
    }
}
