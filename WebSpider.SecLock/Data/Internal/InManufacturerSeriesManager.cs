using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.SecLock.Objects.Internal;

namespace WebSpider.SecLock.Data.Internal
{
    public class InManufacturerSeriesManager : DataManager
    {
        #region [Constructror]
        public InManufacturerSeriesManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<InManufacturerSeries> GetData()
        {
            String Query = "SELECT * FROM SecLockManufacturerSeries WITH (NOLOCK) ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InManufacturerSeries>(oDm.GetTable());
        }
        public List<InManufacturerSeries> GetData(InManufacturer manufacturer)
        {
            String Query = "SELECT * FROM SecLockManufacturerSeries WITH (NOLOCK) WHERE ManufacturerCode = '" + manufacturer.Code + "' ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InManufacturerSeries>(oDm.GetTable());
        }
        public List<InManufacturerSeries> GetData(InManufacturerSeries manufacturer)
        {
            String Query = "SELECT * FROM SecLockManufacturerSeries WITH (NOLOCK) WHERE ID = '" + manufacturer.ID +"' ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InManufacturerSeries>(oDm.GetTable());
        }
        #endregion

        #region [Count]
        public int Count()
        {
            String Query = "SELECT COUNT(1) FROM SecLockManufacturerSeries WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int Count(InManufacturer manufacturer)
        {
            String Query = "SELECT COUNT(1) FROM SecLockManufacturerSeries WITH (NOLOCK) WHERE ManufacturerCode = '" + manufacturer.Code + "'";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion

        #region [ Save ]
        public int Save(String ManufacturerCode, InManufacturerSeries manufacturer)
        {
            var x = GetData(manufacturer);
            if (x.Count > 0)
                return Update(ManufacturerCode, manufacturer);
            else
                return Insert(ManufacturerCode, manufacturer);
        }
        #endregion

        #region [ Insert ]
        private int Insert(String ManufacturerCode, InManufacturerSeries manufacturer)
        {
            String Query = "INSERT INTO [SecLockManufacturerSeries]([ManufacturerCode],[Name]) VALUES('"
                +  ManufacturerCode + "','" + manufacturer.Name + "');";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        private int Update(String ManufacturerCode, InManufacturerSeries manufacturer)
        {
            String Query = "UPDATE [SecLockManufacturerSeries] SET [Name] = '" + manufacturer.Name
                + "',[ManufacturerCode] = '" + ManufacturerCode
                + "' WHERE [Name] = '" + manufacturer.Name + "'";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion
    }
}
