using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.TriGlobal.Objects.TriGlobal;
using WebSpider.Data.DatabaseConnection;
using System.Data;
using System.Reflection;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.TriGlobal.Data.TriGlobal
{
    public class TriBrandManager : DataManager
    {
        #region [Constructror]
        public TriBrandManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<TriBrand> GetData()
        {
            String Query = "SELECT * FROM TriBrands WITH (NOLOCK) ORDER BY DisplayName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<TriBrand>(oDm.GetTable());
        }

        public DataTable GetDataTable()
        {
            String Query = "SELECT * FROM TriBrands WITH (NOLOCK) ORDER BY DisplayName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.GetTable();
        }

        public List<TriBrand> GetData(Boolean ClearanceZone, Boolean SaleCenter, Boolean OnlineSpecials, Boolean HotDeals, Boolean InStock)
        {
            String Query = "SELECT * FROM TriBrands WITH (NOLOCK) "
                + "WHERE ClearanceZone = @ClearanceZone AND SaleCenter = @SaleCenter AND OnlineSpecials = @OnlineSpecials AND HotDeals = @HotDeals AND InStock = @InStock "
                + "ORDER BY DisplayName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddBoolPara("ClearanceZone", ClearanceZone);
            oDm.AddBoolPara("SaleCenter", SaleCenter);
            oDm.AddBoolPara("OnlineSpecials", OnlineSpecials);
            oDm.AddBoolPara("HotDeals", HotDeals);
            oDm.AddBoolPara("InStock", InStock);
            return DataParser.ToList<TriBrand>(oDm.GetTable());
        }
        #endregion
        
        #region [Count]
        public int BrandCount()
        {
            String Query = "SELECT COUNT(1) FROM TriBrands WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int ClearanceZoneBrandCount()
        {
            String Query = "SELECT COUNT(1) FROM TriBrands WITH (NOLOCK) WHERE ClearanceZone = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int SaleCenterBrandCount()
        {
            String Query = "SELECT COUNT(1) FROM TriBrands WITH (NOLOCK) WHERE SaleCenter = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int OnlineSpecialsBrandCount()
        {
            String Query = "SELECT COUNT(1) FROM TriBrands WITH (NOLOCK) WHERE OnlineSpecials = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int HotDealsBrandCount()
        {
            String Query = "SELECT COUNT(1) FROM TriBrands WITH (NOLOCK) WHERE HotDeals = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int InStockBrandCount()
        {
            String Query = "SELECT COUNT(1) FROM TriBrands WITH (NOLOCK) WHERE InStock = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion
        
        #region [ Clear ]
        public int ClearBrands()
        {
            String Query = "DELETE FROM TriBrands WITH (ROWLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        
        #endregion

        public List<TriBrand> GetDataByBrandValue(String Value)
        {
            String Query = "SELECT * FROM TriBrands WITH (NOLOCK) WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("@Value", 255, Value);
            return DataParser.ToList<TriBrand>(oDm.GetTable());
        }

        #region [ Insert / Update ]
        public int Insert(String Value, String DisplayName)
        {
            String Query = "INSERT INTO TriBrands WITH (ROWLOCK) (Value, DisplayName) "
                + "VALUES (@Value, @DisplayName)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("@Value", 255, Value);
            oDm.AddVarcharPara("@DisplayName", 255, DisplayName);
            return oDm.RunActionQuery();
        }

        public int UpdateByBrandValue(String Value, String DisplayName)
        {

            String Query = "UPDATE TriBrands WITH (ROWLOCK) "
                + "SET DisplayName = @DisplayName "
                + "WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 255, Value);
            oDm.AddVarcharPara("DisplayName", 255, DisplayName);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [Save Brand]
        public void SaveBrand(TriBrand brand)
        {
            SaveBrand(brand.Value, brand.DisplayName);
        }

        public void SaveBrand(String Value, String DisplayName)
        {
            var x = GetDataByBrandValue(Value);
            if (x.Count > 0)
            {
                // upadte
                UpdateByBrandValue(Value, DisplayName);
            }
            else
            {
                // insert
                Insert(Value, DisplayName);
            }
        }
        #endregion

        #region [ Flagging ]
        public int SetClearanceZone(String Value)
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET ClearanceZone = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }
        public int SetSaleCenter(String Value)
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET SaleCenter = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }
        public int SetOnlineSpecials(String Value)
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET OnlineSpecials = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }
        public int SetHotDeals(String Value)
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET HotDeals = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }

        public int SetStockAvailability(String Value)
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET InStock = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Flag Removal ]
        public int ClearClearanceZone()
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET ClearanceZone = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        public int ClearSaleCenter()
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET SaleCenter = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        public int ClearOnlineSpecials()
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET OnlineSpecials = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        public int ClearHotDeals()
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET HotDeals = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        public int ClearStockAvailability()
        {
            String Query = "UPDATE TriBrands WITH (ROWLOCK) SET InStock = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

    }
}
