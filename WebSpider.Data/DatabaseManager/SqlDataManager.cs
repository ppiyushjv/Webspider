using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using WebSpider.Data.DatabaseConnection;

#region Change Log
// =============================================================================
// NAME         :   Data Manager
// CLASS NAME   :   DataManager
// PURPOSE      :   Data Manager
// AUTHORS      :   Anup Debnath
// DATE         :   May 27, 2013
// COPYRIGHT    :   Copyright © 2013 IFB.
//
// CHANGE HISTORY : 
// ==============================================================================
// UPDATE 1:
// DONE BY:
// DATE:
// ==============================================================================
//
// $Log: DataManager.cs,v 1.0
//
//==============================================================================
// This document contains confidential and proprietary information of IFB
// and may be protected by patents, trademarks, copyrights, trade secrets, 
// and/or other relevant state, federal, and foreign laws. Its receipt or 
// possession does not convey any rights to reproduce, disclose its contents, 
// or to manufacture, use or sell anything contained herein. Forwarding, 
// reproducing, disclosing or using without specific written authorisation of 
// IFB is strictly forbidden.
// © 2013 IFB. All rights reserved
//==============================================================================
#endregion
namespace WebSpider.Data.DatabaseManager
{
    /// <summary>
    /// Class to execute any kind of database query.
    /// </summary>
    public class SqlDataManager : DataAccessBase, IDisposable
    {
        
        #region FIELDS
        private string strCommandText = string.Empty;
        private bool blnSP = true;
        private ArrayList oParameters = new ArrayList();
        private bool blnLocalConn = true;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a Stored Procedure query with
        /// the given Stored Procedure name.
        /// </summary>
        /// <param name="StoredProcName">Name of the Stored Procedure.</param>
        public SqlDataManager(string StoredProcName)
            : this(StoredProcName, false)
        {
        }
        /// <summary>
        /// Initializes a query with the given Stored Procedure
        /// name or the query text and the query type (Text or Stored
        /// Procedure).
        /// </summary>
        /// <param name="SqlString">Query Text/ Stored Procedure name.</param>
        /// <param name="IsTextQuery">True->Text Query, False->Stored Procedure</param>
        public SqlDataManager(string SqlString, bool IsTextQuery)
        {
            blnSP = !IsTextQuery;
            strCommandText = SqlString;
        }
        #endregion

        // #################### M E T H O D S ####################
        #region Run Query
        #region DataTable
        /// <summary>
        /// Executes the current query and returns the result
        /// in a DataTable object.
        /// </summary>
        /// <returns>The query result set</returns>
        public DataTable GetTable()
        {
            DataTable dt = null;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);
            //if (oCmd.CommandType == CommandType.StoredProcedure)
            //{
            //    oCmd.Parameters.Add("ResultSet", SqlDbType.Structured).Direction=ParameterDirection.Output;                
            //}

            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);

                if ((null != ds) && (ds.Tables.Count > 0))
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return dt;
        }
             
        #endregion

         

        #region DataSet
        /// <summary>
        /// Executes the current query and returns the result Set
        /// in a DataSet object.
        /// </summary>
        /// <returns>The query result set</returns>
        public DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);
            //if (oCmd.CommandType == CommandType.StoredProcedure)
            //{
            //    for (int i = 0; i < recordsetNumber; i++)
            //    {
            //        oCmd.Parameters.Add("ResultSet" + i.ToString(), SqlDbType.Structured).Direction = ParameterDirection.Output; 
            //    } 
            //}

            SqlDataAdapter da = new SqlDataAdapter(oCmd);            

            try
            {
                da.Fill(ds);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return ds;
        }
        #endregion

        #region Command
        /// <summary>
        /// Executes the current query and returns the result Set
        /// in a DataSet object.
        /// </summary>
        /// <returns>The query result set</returns>
        public SqlCommand GetCommand()
        {
            DataSet ds = new DataSet();
            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);


            return oCmd;
        }
        #endregion


        #region NonQuery
        /// <summary>
        /// Executes a DML type query (with no result set).
        /// </summary>
        /// <returns>Number of affected rows.</returns>
        public int RunActionQuery()
        {
            int intRowsAffected = -1;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            try
            {
                intRowsAffected = oCmd.ExecuteNonQuery();
            }
            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return intRowsAffected;
        }
        #endregion

        #region Scalar
        /// <summary>
        /// Executes the query, and returns the first column of the
        /// first row in the result set returned by the query. 
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result
        /// set, or a null reference if the result set is empty.</returns>
        public object GetScalar()
        {
            object oRetVal = null;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            try
            {
                oRetVal = oCmd.ExecuteScalar();
            }

            finally
            {
                if (this.blnLocalConn)
                {
                    this.oConn.Close();
                }
                oCmd.Dispose();
            }

            return oRetVal;
        }
        #endregion

        #region Reader
        public SqlDataReader ExecuteReader()
        {
            SqlDataReader reader = null;
            try
            {
                SqlCommand oCmd = new SqlCommand();
                this.InitQuery(oCmd);
                reader = oCmd.ExecuteReader();
            }
            catch { }
            return reader;
        }
        #endregion

        #region Initializes a Query
        /// <summary>
        /// Performs the initial tasks before executing a query.
        /// </summary>
        /// <param name="oCmd">Command object holding the query.</param>
        private void InitQuery(SqlCommand oCmd)
        {
            // set Connection
            blnLocalConn = (this.oConn == null);
            if (blnLocalConn)
            {
                oConn = new SqlDataConnection();
                blnLocalConn = true;
                oConn.Open();
            }
            oCmd.Connection = oConn.oConn;

            // set Command
            oCmd.CommandTimeout = 0;
            oCmd.CommandText = this.strCommandText;
            oCmd.CommandType = (this.blnSP ? CommandType.StoredProcedure : CommandType.Text);

            // set Parameters
            foreach (object oItem in this.oParameters)
            {
                //System.Diagnostics.Debug.Print(oItem.ToString() +" : "+ ((SqlParameter)oItem).Value);
                oCmd.Parameters.Add((SqlParameter)oItem);
            }
        }
        #endregion
        #endregion

        #region Parameter handling
        #region Type: Integer
        /// <summary>
        /// Adds an Integer type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddIntegerPara(string Name, int Value)
        {
            AddIntegerPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds an Integer type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddIntegerPara(string Name, int? Value)
        {
            AddIntegerPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds an Integer type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddIntegerPara(string Name, int Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        /// <summary>
        /// Adds an Integer type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddIntegerPara(string Name, int? Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
            oPara.Direction = GetParaType(Direction);
            if (Value == null)
                oPara.Value = DBNull.Value;
            else
                oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: Bigint
        /// <summary>
        /// Adds an Integer type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddIntegerBigPara(string Name, Int64 Value)
        {
            AddIntegerBigPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds an Integer type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddIntegerBigPara(string Name, Int64? Value)
        {
            AddIntegerBigPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds an Integer type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddIntegerBigPara(string Name, Int64 Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.BigInt);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        /// <summary>
        /// Adds an Integer type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddIntegerBigPara(string Name, Int64? Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.BigInt);
            oPara.Direction = GetParaType(Direction);
            if (Value == null)
                oPara.Value = DBNull.Value;
            else
                oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: Char
        /// <summary>
        /// Adds a Char type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddCharPara(string Name, int Size, char Value)
        {
            AddCharPara(Name, Size, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Char type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddCharPara(string Name, int Size, char Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value.Equals(null))
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Char, Size);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: Varchar
        /// <summary>
        /// Adds a Varchar type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddVarcharPara(string Name, int Size, string Value)
        {
            AddVarcharPara(Name, Size, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Varchar type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddVarcharPara(string Name, int Size, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            else if (Value.Length == 0)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarChar, Size);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion
        
        #region Type: Boolean
        /// <summary>
        /// Adds a Blob type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddBoolPara(string Name, bool Value)
        {
            AddBoolPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Blob type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddBoolPara(string Name, bool Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Bit);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: Image
        /// <summary>
        /// Adds a Clob type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddImagePara(string Name, String Value)
        {
            AddImagePara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Clob type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddImagePara(string Name, String Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Image);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }


        #endregion

        #region Type: GUID
        /// <summary>
        /// Adds a Varchar type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddGuidPara(string Name, string Value)
        {
            AddGuidPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Varchar type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Size">Size of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddGuidPara(string Name, string Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name,SqlDbType.Char,38);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: DateTime
        /// <summary>
        /// Adds a DateTime type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddDateTimePara(string Name, DateTime Value)
        {
            AddDateTimePara(Name, Value, QueryParameterDirection.Input);
        }
        public void AddDateTimePara(string Name, DateTime? Value)
        {
            AddDateTimePara(Name, Value, QueryParameterDirection.Input);
        }

        /// <summary>
        /// Adds a DateTime type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddDateTimePara(string Name, DateTime Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.DateTime);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }

        public void AddDateTimePara(string Name, DateTime? Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.DateTime);
            oPara.Direction = GetParaType(Direction);
            if (Value == null)
                oPara.Value = DBNull.Value;
            else
                oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        
        #endregion

        #region Type: Decimal
        /// <summary>
        /// Adds a Decimal type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Scale">Scale of the decimal number</param>
        /// <param name="Precision">Precision of the decimal number</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddDecimalPara(string Name, byte Scale, byte Precision, decimal Value)
        {
            AddDecimalPara(Name, Scale, Precision, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Decimal type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Scale">Scale of the decimal number</param>
        /// <param name="Precision">Precision of the decimal number</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddDecimalPara(string Name, byte Scale, byte Precision, decimal Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Decimal);
            oPara.Scale = Scale;
            oPara.Precision = Precision;
            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: Float
        /// <summary>
        /// Adds a Float type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>

        /// <param name="Value">Value of the parameter.</param>
        public void AddFloatPara(string Name, float Value)
        {
            AddFloatPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds the float para.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Value">The value.</param>
        /// <param name="Direction">The direction.</param>
        /// <author>Debajit Mukhopadhyay</author>
        /// <createdDate>19-Oct-09</createdDate>
        public void AddFloatPara(string Name, float Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Decimal);

            oPara.Direction = GetParaType(Direction);
            oPara.Value = Value;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: TimeStamp
        /// <summary>
        /// Adds a Timestamp type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddTimeStampPara(string Name, System.Byte[] Value)
        {
            AddTimeStampPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Timestamp type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddTimeStampPara(string Name, System.Byte[] Value, QueryParameterDirection Direction)
        {
            object oValue = (object)Value;
            if (Value == null)
            {
                oValue = DBNull.Value;
            }
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Timestamp);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: VarBinary
        /// <summary>
        /// Adds a Binary type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddVarBinaryPara(string Name, byte[] Value)
        {
            AddVarBinaryPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Binary type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddVarBinaryPara(string Name, byte[] oValue, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarBinary);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Type: Structured
        /// <summary>
        /// Adds a Structured type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddStructuredPara(string Name, DataTable Value)
        {
            AddStructuredPara(Name, Value, QueryParameterDirection.Input);
        }
        /// <summary>
        /// Adds a Structured type query parameter with
        /// the given direction type. 
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddStructuredPara(string Name, DataTable oValue, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Structured);
            oPara.Direction = GetParaType(Direction);
            oPara.Value = oValue;
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Adds a NULL value Parameter
        /// <summary>
        /// Adds a NULL value query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        public void AddNullValuePara(string Name)
        {
            SqlParameter oPara = new SqlParameter(Name, DBNull.Value);
            oPara.Direction = ParameterDirection.Input;
            this.oParameters.Add(oPara);
        }

        /// <summary>
        /// Adds a NULL value query parameter with Parameter Direction
        /// </summary>
        /// <param name="Name">Name of parameter</param>
        /// <param name="Direction">Parameter Direction</param>
        public void AddNullValuePara(string Name, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, DBNull.Value);
            oPara.Direction = GetParaType(Direction);
            this.oParameters.Add(oPara);
        }
        #endregion

        #region Adds the Return Parameter
        /// <summary>
        /// Adds the return parameter.
        /// </summary>
        public void AddReturnPara()
        {
            this.AddIntegerPara("ReturnIntPara", 0, QueryParameterDirection.Return);
        }
        #endregion

        #region Returns the value of the passed parameter
        /// <summary>
        /// Returns the value of a parameter.
        /// </summary>
        /// <param name="ParaName">Name of the parameter.</param>
        /// <returns>Value of the parameter.</returns>
        public object GetParaValue(string ParaName)
        {
            object oValue = null;
            SqlParameter oPara = null;

            ParaName = ParaName.Trim().ToLower();
            foreach (object oItem in this.oParameters)
            {
                oPara = (SqlParameter)oItem;
                if (oPara.ParameterName.ToLower() == ParaName)
                {
                    oValue = oPara.Value;
                    break;
                }
            }

            return oValue;
        }
        #endregion

        #region Returns the value of the Return Parameter
        /// <summary>
        /// Returns the value of the Return Parameter.
        /// </summary>
        /// <returns>The value of the Return Parameter.</returns>
        public object GetReturnParaValue()
        {
            return this.GetParaValue("ReturnIntPara");
        }
        #endregion

        #region Clears the parameters
        /// <summary>
        /// Clears the parameters collection.
        /// </summary>
        public void ClearParameters()
        {
            this.oParameters.Clear();
        }
        #endregion

        #region Converts enum to parameter direction
        /// <summary>
        /// Converts parameter direction enum to the underlying sql type
        /// </summary>
        /// <param name="Direction">Enum value to convert</param>
        /// <returns>Underlying SqlClient value corresponding to the passed Enum</returns>
        private ParameterDirection GetParaType(QueryParameterDirection Direction)
        {
            switch (Direction)
            {
                case QueryParameterDirection.Output:
                    return ParameterDirection.InputOutput;
                case QueryParameterDirection.Return:
                    return ParameterDirection.ReturnValue;
                case QueryParameterDirection.InputOutput:
                    return ParameterDirection.InputOutput;
                default:
                    return ParameterDirection.Input;
            }
        }
        #endregion
        #endregion

        #region Dispose
        /// <summary>
        /// Releases the resources.
        /// </summary>
        public void Dispose()
        {
            this.oConn.Dispose();
            this.oParameters.Clear();
        }
        #endregion

        // ################## P R O P E R T I E S ##################
        #region Connection
        private SqlDataConnection oConn = null;
        /// <summary>
        /// Write Only: Connection object to run the query. To be used
        /// in transactional operations involving multiple objects.
        /// Also used in performing multiple database operations using
        /// the same connection.
        /// </summary>
        public SqlDataConnection Connection
        {
            set
            {
                oConn = value;
            }
        }
        #endregion

        internal void AddNullValuePara()
        {
            throw new NotImplementedException();
        }
    }
}
