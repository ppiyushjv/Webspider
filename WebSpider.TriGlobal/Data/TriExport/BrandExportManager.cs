﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.TriGlobal.Objects.TriExport;

namespace WebSpider.TriGlobal.Data.TriExport
{
    public class BrandExportManager :DataManager
    {
        #region [Constructror]
        public BrandExportManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<Tri_Brand> GetData()
        {
            String Query = "SELECT * FROM TriBrand";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<Tri_Brand>(oDm.GetTable());
        }

        public List<Tri_Brand> GetData(String Value)
        {
            String Query = "SELECT * FROM TriBrand WHERE Value = @Value";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return DataParser.ToList<Tri_Brand>(oDm.GetTable());
        }
        #endregion

        #region [ Save ]
        public int Save(String Value, String DisplayName, bool ClearanceZone, bool SaleCenter, bool OnlineSpecials, bool HotDeals)
        {
            if (GetData(Value).Count == 0)
                return Insert(Value, DisplayName, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals);
            else
                return Update(Value, DisplayName, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals);
        }
        #endregion

        #region [ Insert ]
        public int Insert(String Value, String DisplayName, bool ClearanceZone, bool SaleCenter, bool OnlineSpecials, bool HotDeals)
        {
            String Query = "INSERT INTO TriBrand ([Value], DisplayName, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals)"
                + " VALUES (@Value, @DisplayName, @ClearanceZone, @SaleCenter, @OnlineSpecials, @HotDeals)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
            oDm.AddBoolPara("ClearanceZone", ClearanceZone);
            oDm.AddBoolPara("SaleCenter", SaleCenter);
            oDm.AddBoolPara("OnlineSpecials", OnlineSpecials);
            oDm.AddBoolPara("HotDeals", HotDeals);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        public int Update(String Value, String DisplayName, bool ClearanceZone, bool SaleCenter, bool OnlineSpecials, bool HotDeals)
        {
            String Query = "UPDATE TriBrand SET DisplayName = @DisplayName, "
                + "ClearanceZone = @ClearanceZone, SaleCenter = @SaleCenter, OnlineSpecials = @OnlineSpecials, HotDeals = @HotDeals "
                + "WHERE [Value] = @Value";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
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
            String Query = "DELETE FROM TriBrand WHERE Value = @Value";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 4000, Value);
            return oDm.RunActionQuery();
        }

        public int DeleteAll()
        {
            String Query = "DELETE FROM TriBrand";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion
    }
}
