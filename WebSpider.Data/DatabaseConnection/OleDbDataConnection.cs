using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace WebSpider.Data.DatabaseConnection
{
    public class OleDbDataConnection
    {
        #region FIELDS
        private string sConnStr = string.Empty;
        internal OleDbConnection oConn = null;

        internal OleDbTransaction oTran = null;
        #endregion

        public String ConnectionString
        {
            get
            {
                return sConnStr;
            }
            set
            {
                sConnStr = value;
            }
        }

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public OleDbDataConnection()
        {
            //sConnStr = WebConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            //sConnStr = "";//ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
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

            oConn = new OleDbConnection(ConnectionString);
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
