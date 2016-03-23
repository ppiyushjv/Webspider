using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WebSpider.Core
{
    public sealed class Settings
    {
        #region [Data Members]
        private static String SettingsFile = Application.StartupPath + "\\Settings.xml";
        private static DataSet SettingsDB;

        private const string DBNAME = "WebSpiderSettings";
        private const string TABLENAME = "ConfigurationSetting";
        #endregion

        static Settings()
        {
            SettingsDB = new DataSet(DBNAME);
            SettingsDB.Tables.Add(TABLENAME);
            SettingsDB.Tables[TABLENAME].Columns.Add("Name", typeof(String));
            SettingsDB.Tables[TABLENAME].Columns.Add("Type", typeof(String));
            SettingsDB.Tables[TABLENAME].Columns.Add("Value", typeof(String));
            try
            {
                Load();
            }
            catch { }
        }

        #region [Public Methods]
        public static void SetValue(String Name, Type type, Object Value)
        {
            DataRow dRow = GetRow(Name);
            if (ReferenceEquals(dRow, null))
            {
                dRow = SettingsDB.Tables[TABLENAME].NewRow();
                dRow["Name"] = Name;
                dRow["Type"] = type.ToString();
                //if (type == typeof(DateTime))
                //    dRow["Value"] = ((DateTime)Value).ToUniversalTime();
                //else
                dRow["Value"] = Value;
                SettingsDB.Tables[TABLENAME].Rows.Add(dRow);
            }
            else
            {
                dRow["Value"] = Value;
            }
        }

        public static dynamic GetValue(String Name)
        {
            var row = GetRow(Name);

            if (!ReferenceEquals(row, null))
            {
                Type type = Type.GetType(row.Field<String>("Type"));
                String Value = row.Field<String>("Value");
                return GetValue(Value, type);
            }
            return null;
        }

        public static void Save()
        {
            SettingsDB.WriteXml(SettingsFile);
        }
        #endregion

        #region [Private Methods]
        private static void Load()
        {
            SettingsDB.ReadXml(SettingsFile);
        }

        private static DataRow GetRow(String Name)
        {
            try
            {
                if (SettingsDB.Tables[TABLENAME].Rows.Count > 0)
                {
                    var row = SettingsDB.Tables[TABLENAME].AsEnumerable()
                                    .Where(x => x.Field<String>("Name") == Name).First();

                    return row;
                }
            }
            catch { }
            return null;
        }

        private static dynamic GetValue(String Value, Type type)
        {
            switch (type.ToString())
            {
                case "System.Int16":
                    return Convert.ToInt16(Value);
                    break;
                case "System.Int32":
                    return Convert.ToInt32(Value);
                    break;
                case "System.Int64":
                    return Convert.ToInt64(Value);
                    break;
                case "System.Boolean":
                    return Convert.ToBoolean(Value);
                    break;
                case "System.String":
                    return Value;
                    break;
                case "System.DateTime":
                    return Convert.ToDateTime(Value);
                    break;
                default:
                    return Value;
                    break;
            }
        }
        #endregion

        
    }
}
