using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.AdiExport;

namespace WebSpider.Data.AdiExport
{
    public class CategoryExportManager :DataManager
    {
        #region [Constructror]
        public CategoryExportManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<Adi_Category> GetData()
        {
            String Query = "SELECT * FROM AdiCategory";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<Adi_Category>(oDm.GetTable());
        }

        public List<Adi_Category> GetData(String Value)
        {
            String Query = "SELECT * FROM AdiCategory WHERE Value = @Value";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return DataParser.ToList<Adi_Category>(oDm.GetTable());
        }
        #endregion

        #region [ Save ]
        public int Save(String Value, String DisplayName, String ParentValue, String CategoryUrl, bool ClearanceZone, bool SaleCenter, bool OnlineSpecials, bool HotDeals)
        {
            if (GetData(Value).Count == 0)
                return Insert(Value, DisplayName, ParentValue, CategoryUrl, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals);
            else
                return Update(Value, DisplayName, ParentValue, CategoryUrl, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals);
        }
        #endregion

        #region [ Insert ]
        public int Insert(String Value, String DisplayName, String ParentValue, String CategoryUrl, bool ClearanceZone, bool SaleCenter, bool OnlineSpecials, bool HotDeals)
        {
            String Query = "INSERT INTO AdiCategory ([Value], DisplayName, ParentValue, CategoryUrl, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals)"
                + " VALUES (@Value, @DisplayName, @ParentValue, @CategoryUrl, @ClearanceZone, @SaleCenter, @OnlineSpecials, @HotDeals)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
            oDm.AddVarcharPara("ParentValue", 4000, ParentValue);
            oDm.AddVarcharPara("CategoryUrl", 4000, CategoryUrl);
            oDm.AddBoolPara("ClearanceZone", ClearanceZone);
            oDm.AddBoolPara("SaleCenter", SaleCenter);
            oDm.AddBoolPara("OnlineSpecials", OnlineSpecials);
            oDm.AddBoolPara("HotDeals", HotDeals);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        public int Update(String Value, String DisplayName, String ParentValue, String CategoryUrl, bool ClearanceZone, bool SaleCenter, bool OnlineSpecials, bool HotDeals)
        {
            String Query = "UPDATE AdiCategory SET DisplayName = @DisplayName, ParentValue = @ParentValue, CategoryUrl = @CategoryUrl, "
                + "ClearanceZone = @ClearanceZone, SaleCenter = @SaleCenter, OnlineSpecials = @OnlineSpecials, HotDeals = @HotDeals "
                + "WHERE [Value] = @Value";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
            oDm.AddVarcharPara("ParentValue", 4000, ParentValue);
            oDm.AddVarcharPara("CategoryUrl", 4000, CategoryUrl);
            oDm.AddBoolPara("ClearanceZone", ClearanceZone);
            oDm.AddBoolPara("SaleCenter", SaleCenter);
            oDm.AddBoolPara("OnlineSpecials", OnlineSpecials);
            oDm.AddBoolPara("HotDeals", HotDeals);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Delete ]
        public int Delete(String Value)
        {
            String Query = "DELETE FROM AdiCategory WHERE Value = @Value";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }

        public int DeleteAll()
        {
            String Query = "DELETE FROM AdiCategory";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion
    }
}
