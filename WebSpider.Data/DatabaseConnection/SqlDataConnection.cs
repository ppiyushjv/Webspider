using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

#region Change Log
// =============================================================================
// NAME         :   Data Connection
// CLASS NAME   :   DataConnection
// PURPOSE      :   Data Connection
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
// $Log: DataConnection.cs,v 1.0
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
namespace WebSpider.Data.DatabaseConnection
{
    /// <summary>
    /// Encapsulates the functionalities of the database connection class.
    /// This class is used by the business logic component only when it needs
    /// to perform database related operations of different objects within a
    /// single transaction boundary.
    /// </summary>
    public class SqlDataConnection : DataAccessBase, IDisposable
    {
        #region FIELDS
        private string sConnStr = string.Empty;
        internal SqlConnection oConn = null;

        internal SqlTransaction oTran = null;
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public SqlDataConnection()
        {
            //sConnStr = WebConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            //sConnStr = ""; // ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }
        #endregion


        // #################### M E T H O D S ####################
        #region Sets the database connection string
        /// <summary>
        /// Static function to set the database connection string.
        /// </summary>
        /// <param name="ConnectionString">The connection string to set.</param>
        public void SetConnectionString(string ConnectionString)
        {
            sConnStr = ConnectionString;
        }
        #endregion

        #region Opens a connection
        /// <summary>
        /// Opens the the database connection.
        /// </summary>
        /// <returns>Whether the connection has been successfully opened or not.</returns>
        public bool Open()
        {
            return Open(sConnStr);
        }
        /// <summary>
        /// Opens the the database connection with a given connection string.
        /// </summary>
        /// <param name="ConnectionString">The connection string.</param>
        /// <returns>Whether the connection has been successfully opened or not.</returns>
        public bool Open(string ConnectionString)
        {
            blnIsOpen = false;

            oConn = new SqlConnection(ConnectionString);
            oConn.Open();
            blnIsOpen = true;

            return blnIsOpen;
        }
        #endregion

        #region Closes a connection
        /// <summary>
        /// Closes the connection (if open).
        /// </summary>
        public void Close()
        {
            if (oConn.State == ConnectionState.Open)
            {
                oConn.Close();
                oConn = null;
                blnIsOpen = false;
            }
        }
        #endregion

        #region Transaction handling related methods
        /// <summary>
        /// Starts a Transaction session on the current connection.
        /// </summary>
        public void BeginTran()
        {
            if (oConn.State == ConnectionState.Open)
            {
                oTran = oConn.BeginTransaction();
                blnTranActive = true;
            }
        }
        /// <summary>
        /// Commits the currently active Transaction (if any).
        /// </summary>
        public void CommitTran()
        {
            if ((oConn.State == ConnectionState.Open) && blnTranActive)
            {
                oTran.Commit();
                blnTranActive = false;
            }
        }
        /// <summary>
        /// Rools back the currently active Transaction (if any).
        /// </summary>
        public void RollbackTran()
        {
            if ((oConn.State == ConnectionState.Open) && blnTranActive)
            {
                oTran.Rollback();
                blnTranActive = false;
            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Releases the resources.
        /// </summary>
        public void Dispose()
        {
            if (oConn != null)
            {
                oConn.Dispose();
            }
        }
        #endregion


        // ################## P R O P E R T I E S ##################
        #region IsOpen
        private bool blnIsOpen = false;
        /// <summary>
        /// Indicates whether the connection is currently open or not.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return blnIsOpen;
            }
        }
        #endregion

        #region IsTransactionActive
        private bool blnTranActive = false;
        /// <summary>
        /// Indicates whether there is any active Transaction or not.
        /// </summary>
        public bool IsTransactionActive
        {
            get
            {
                return blnTranActive;
            }
        }
        #endregion
    }
}
