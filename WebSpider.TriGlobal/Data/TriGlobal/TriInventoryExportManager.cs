﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.TriGlobal.Objects.TriGlobal;

namespace WebSpider.TriGlobal.Data.TriGlobal
{
    public class TriInventoryExportManager : DataManager
    {
        #region [ Constructor ]
        public TriInventoryExportManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        public List<TriInventoryExport> GetData()
        {
            String Query = "SELECT * FROM TriInventoryExport WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<TriInventoryExport>(oDm.GetTable());
        }

        //public List<TriInventoryExport> FillByProduct(String PART_NUM)
        //{
        //    String Query = "SELECT * FROM TriInventoryExport WHERE PART_NUM = @PART_NUM";
        //    SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
        //    oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
        //    return DataParser.ToList<TriInventoryExport>(oDm.GetTable());
        //}

        public int ProductCount(String PART_NUM)
        {
            String Query = "SELECT COUNT(1) FROM TriInventoryExport WITH (NOLOCK) WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return (int) oDm.GetTable().Rows[0][0];
        }

        public int UpdateByPartNum(String PART_NUM, int TotalInventory, int Dallas, int DC_AtlantaHub, int DC_Dallas_Hub, int DC_Elk_Grove_Hub, int DC_Feura_Bush, int DC_Louisville_Hub
            , int DC_Reno_Hub, int DC_Richmond_Dist_Ctr, int Oklahama, int RemainingBranches, DateTime LastUpdate)
        {
            String Query = "UPDATE TriInventoryExport WITH (ROWLOCK) SET TotalInventory = @TotalInventory, Dallas = @Dallas, DC_AtlantaHub = @DC_AtlantaHub, DC_Dallas_Hub = @DC_Dallas_Hub, DC_Elk_Grove_Hub = @DC_Elk_Grove_Hub, DC_Feura_Bush = @DC_Feura_Bush, DC_Louisville_Hub = @DC_Louisville_Hub, DC_Reno_Hub = @DC_Reno_Hub,DC_Richmond_Dist_Ctr = @DC_Richmond_Dist_Ctr, Oklahama = @Oklahama, RemainingBranches = @RemainingBranches, LastUpdate = @LastUpdate WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddIntegerPara("TotalInventory", TotalInventory);
            oDm.AddIntegerPara("Dallas", Dallas);
            oDm.AddIntegerPara("DC_AtlantaHub", DC_AtlantaHub);
            oDm.AddIntegerPara("DC_Dallas_Hub", DC_Dallas_Hub);
            oDm.AddIntegerPara("DC_Elk_Grove_Hub", DC_Elk_Grove_Hub);
            oDm.AddIntegerPara("DC_Feura_Bush", DC_Feura_Bush);
            oDm.AddIntegerPara("DC_Louisville_Hub", DC_Louisville_Hub);
            oDm.AddIntegerPara("DC_Reno_Hub", DC_Reno_Hub);
            oDm.AddIntegerPara("DC_Richmond_Dist_Ctr", DC_Richmond_Dist_Ctr);
            oDm.AddIntegerPara("Oklahama", Oklahama);
            oDm.AddIntegerPara("RemainingBranches", RemainingBranches);
            oDm.AddDateTimePara("LastUpdate", LastUpdate);
            return oDm.RunActionQuery();
        }

        public int Insert(String PART_NUM, int TotalInventory, int Dallas, int DC_AtlantaHub, int DC_Dallas_Hub, int DC_Elk_Grove_Hub, int DC_Feura_Bush, int DC_Louisville_Hub
            , int DC_Reno_Hub, int DC_Richmond_Dist_Ctr, int Oklahama, int RemainingBranches, DateTime LastUpdate)
        {
            String Query = "INSERT INTO TriInventoryExport WITH (ROWLOCK) (PART_NUM, TotalInventory, Dallas, DC_AtlantaHub, DC_Dallas_Hub, DC_Elk_Grove_Hub, DC_Feura_Bush, DC_Louisville_Hub, DC_Reno_Hub, DC_Richmond_Dist_Ctr, Oklahama, RemainingBranches, LastUpdate) "
             + "VALUES (@PART_NUM, @TotalInventory, @Dallas, @DC_AtlantaHub, @DC_Dallas_Hub, @DC_Elk_Grove_Hub, @DC_Feura_Bush, @DC_Louisville_Hub, @DC_Reno_Hub, @DC_Richmond_Dist_Ctr, @Oklahama, @RemainingBranches, @LastUpdate)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddIntegerPara("TotalInventory", TotalInventory);
            oDm.AddIntegerPara("Dallas", Dallas);
            oDm.AddIntegerPara("DC_AtlantaHub", DC_AtlantaHub);
            oDm.AddIntegerPara("DC_Dallas_Hub", DC_Dallas_Hub);
            oDm.AddIntegerPara("DC_Elk_Grove_Hub", DC_Elk_Grove_Hub);
            oDm.AddIntegerPara("DC_Feura_Bush", DC_Feura_Bush);
            oDm.AddIntegerPara("DC_Louisville_Hub", DC_Louisville_Hub);
            oDm.AddIntegerPara("DC_Reno_Hub", DC_Reno_Hub);
            oDm.AddIntegerPara("DC_Richmond_Dist_Ctr", DC_Richmond_Dist_Ctr);
            oDm.AddIntegerPara("Oklahama", Oklahama);
            oDm.AddIntegerPara("RemainingBranches", RemainingBranches);
            oDm.AddDateTimePara("LastUpdate", LastUpdate);
            return oDm.RunActionQuery();
        }



        public TriInventoryExport GetDataByProduct(String PART_NUM)
        {
            String Query = "SELECT * FROM TriInventoryExport WITH (NOLOCK) WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            var list = DataParser.ToList<TriInventoryExport>(oDm.GetTable());
            return list.Count == 1 ? list[0] : null;
        }
    }
}
