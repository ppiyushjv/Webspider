using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.SecLock.Objects.Internal;

namespace WebSpider.SecLock.Data.Internal
{
    public class InManufacturerManager : DataManager
    {
        #region [Constructror]
        public InManufacturerManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<InManufacturer> GetData()
        {
            String Query = "SELECT * FROM SecLockManufacturer WITH (NOLOCK) ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InManufacturer>(oDm.GetTable());
        }
        public DataTable GetDataTable()
        {
            String Query = "SELECT * FROM SecLockManufacturer WITH (NOLOCK) ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.GetTable();
        }
        public List<InManufacturer> GetData(InManufacturer manufacturer)
        {
            String Query = "SELECT * FROM SecLockManufacturer WITH (NOLOCK) WHERE Code = '" + manufacturer.Code +"' ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InManufacturer>(oDm.GetTable());
        }
        #endregion

        #region [Count]
        public int Count()
        {
            String Query = "SELECT COUNT(1) FROM SecLockManufacturer WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion

        #region [ Save ]
        public int Save(InManufacturer manufacturer)
        {
            var x = GetData(manufacturer);
            if (x.Count > 0)
                return Update(manufacturer);
            else
                return Insert(manufacturer);
        }
        #endregion

        #region [ Insert ]
        private int Insert(InManufacturer manufacturer)
        {
            String Query = "INSERT INTO [SecLockManufacturer]([Code],[Name],[ImagePath],[Url]) VALUES('"
                + manufacturer.Code + "','" + manufacturer.Name + "','" + manufacturer.ImagePath + "','" + manufacturer.Url + "');";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        private int  Update(InManufacturer manufacturer)
        {
            String Query = "UPDATE [SecLockManufacturer] SET [Name] = '" + manufacturer.Name
                + "',[ImagePath] = '" + manufacturer.ImagePath
                + "', [Url] = '" + manufacturer.Url
                + "' WHERE [Code] = '" + manufacturer.Code + "'";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Delete All ]
        public void DeleteAll()
        {
            String Query = "DELETE FROM [SecLockManufacturer]";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }
        #endregion
    }
}
