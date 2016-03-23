using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Objects.AdiGlobal;
using WebSpider.Data.DatabaseConnection;
using System.Data;
using System.Reflection;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.Data.AdiGlobal
{
    public class ADICategoryManager :DataManager
    {
        #region [ Constructor ]
        public ADICategoryManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<AdiCategory> GetData()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }

        public DataTable GetDataTable()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.GetTable();
        }

        public List<AdiCategory> GetData(Boolean ClearanceZone, Boolean SaleCenter, Boolean OnlineSpecials, Boolean HotDeals, Boolean InStock)
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) "
                + "WHERE ClearanceZone = @ClearanceZone AND SaleCenter = @SaleCenter AND OnlineSpecials = @OnlineSpecials AND HotDeals = @HotDeals AND InStock = @InStock";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddBoolPara("ClearanceZone", ClearanceZone);
            oDm.AddBoolPara("SaleCenter", SaleCenter);
            oDm.AddBoolPara("OnlineSpecials", OnlineSpecials);
            oDm.AddBoolPara("HotDeals", HotDeals);
            oDm.AddBoolPara("InStock", InStock);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }
        public List<AdiCategory> GetClearanceZoneData()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) WHERE ClearanceZone = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }
        public List<AdiCategory> GetSaleCenterData()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) SaleCenter = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }
        public List<AdiCategory> GetOnlineSpecialsData()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) WHERE OnlineSpecials = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }
        public List<AdiCategory> GetHotDealsData()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) WHERE HotDeals = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }
        public List<AdiCategory> GetInStockData()
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) InStock = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }
        #endregion

        #region [ Count ]
        public int CategoryCount()
        {
            String Query = "SELECT COUNT(1) FROM ADICategory WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int ClearanceZoneCategoryCount()
        {
            String Query = "SELECT COUNT(1) FROM ADICategory WITH (NOLOCK) WHERE ClearanceZone = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int SaleCenterCategoryCount()
        {
            String Query = "SELECT COUNT(1) FROM ADICategory WITH (NOLOCK) WHERE SaleCenter = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int OnlineSpecialsCategoryCount()
        {
            String Query = "SELECT COUNT(1) FROM ADICategory WITH (NOLOCK) WHERE OnlineSpecials = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int HotDealsCategoryCount()
        {
            String Query = "SELECT COUNT(1) FROM ADICategory WITH (NOLOCK) WHERE HotDeals = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        public int InStockCategoryCount()
        {
            throw new NotImplementedException();
            String Query = "SELECT COUNT(1) FROM ADICategory WITH (NOLOCK) WHERE InStock = 1";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion
        
        #region [Clear]
        public void ClearCatagory()
        {
            String Query = "DELETE FROM ADICategory WITH (ROWLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }
        #endregion
        
        public List<AdiCategory> GetCategoryByParent(String ParentValue)
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) WHERE ParentValue = @ParentValue";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("ParentValue", 255, ParentValue);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }

        public List<AdiCategory> GetDataByCatagoryID(String Value)
        {
            String Query = "SELECT * FROM ADICategory WITH (NOLOCK) WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 255, Value);
            return DataParser.ToList<AdiCategory>(oDm.GetTable());
        }

        #region [ insert / Update ]
        public int Insert(String Value, String DisplayName, String ParentValue, String CategoryUrl)
        {

            String Query = "INSERT INTO ADICategory WITH (ROWLOCK) (ParentValue, Value, DisplayName, CategoryUrl) "
                + "VALUES (@ParentValue, @Value, @DisplayName, @CategoryUrl)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("ParentValue", 255, ParentValue);
            oDm.AddVarcharPara("Value", 255, Value);
            oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
            oDm.AddVarcharPara("CategoryUrl", 4000, CategoryUrl);
            return oDm.RunActionQuery();
        }

        public int UpdateByCatagoryID(String Value, String DisplayName, String ParentValue, String CategoryUrl)
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) "
                + "SET ParentValue = @ParentValue, DisplayName = @DisplayName, CategoryUrl = @CategoryUrl "
                + "WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("ParentValue", 255, ParentValue);
            oDm.AddVarcharPara("Value", 255, Value);
            oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
            oDm.AddVarcharPara("CategoryUrl", 4000, CategoryUrl);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [Save Catagory]
        public void SaveCategory(AdiCategory category)
        {
            SaveCategory(category.Value, category.DisplayName, category.ParentValue, category.CategoryUrl);
        }

        public void SaveCategory(string Value, string DisplayName, string ParentValue, string CategoryUrl)
        {
            List<AdiCategory> categoryList = GetDataByCatagoryID(Value);
            if (categoryList.Count != 0)
            {
                //Update
                UpdateByCatagoryID(Value, DisplayName, ParentValue, CategoryUrl);
            }
            else
            {
                //insert
                Insert(Value, DisplayName, ParentValue, CategoryUrl);
            }
        }
        #endregion

        #region [ Flagging ]
        public int SetClearanceZoneByCategoryValue(String Value)
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET ClearanceZone = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }

        public int SetSaleCenterByCategoryValue(String Value)
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET SaleCenter = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }

        public int SetOnlineSpecialsByCategoryValue(String Value)
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET OnlineSpecials = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }

        public int SetHotDealsByCategoryValue(String Value)
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET HotDeals = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }
        public int SetStockAvailability(String Value)
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET InStock = 1 WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Flag Removal ]
        public int ClearClearanceZone()
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET ClearanceZone = 0 ";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearSaleCenter()
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET SaleCenter = 0 ";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearOnlineSpecials()
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET OnlineSpecials = 0 ";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearHotDeals()
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET HotDeals = 0 ";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        public int StockAvailability()
        {
            String Query = "UPDATE ADICategory WITH (ROWLOCK) SET InStock = 0 ";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ EXPORT ]
        public List<ADICategoryExport> GetExport()
        {
            String Query = "SELECT ROOT.[Value] [RootValue],ROOT.[DisplayName] [RootDisplayName],PARENT.[Value] [ParentValue],PARENT.[DisplayName] [ParentDisplayName],CHILD.[Value] ,CHILD.[DisplayName] ,CHILD.[CategoryUrl] "
                + "FROM [ADICategory] ROOT WITH (NOLOCK) JOIN [ADICategory] PARENT  WITH (NOLOCK) ON ROOT.[Value] = PARENT.[ParentValue] "
                + "JOIN [ADICategory] CHILD WITH (NOLOCK) ON PARENT.[Value] = CHILD.[ParentValue] "
                + "WHERE ROOT.[ParentValue] IS NULL";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<ADICategoryExport>(oDm.GetTable());
        }
        #endregion




        
    }
}
