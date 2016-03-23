using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseConnection;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.AdiExport;

namespace WebSpider.Data.AdiExport
{
    public class AdiInventoryManager : DataManager
    {
        #region [Constructror]
        public AdiInventoryManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<ADI_Inventory> GetData()
        {
            String Query = "SELECT * FROM ADIInventory";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<ADI_Inventory>(oDm.GetTable());
        }

        public List<ADI_Inventory> GetData(String PART_NUM)
        {
            String Query = "SELECT * FROM ADIInventory WHERE PART_NUM = \"" + PART_NUM + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return DataParser.ToList<ADI_Inventory>(oDm.GetTable());
        }
        #endregion


        #region [ Save ]
        public int Save(ADI_Inventory ADIInventory)
        {
            return Save(ADIInventory.ID, ADIInventory.PART_NUM, ADIInventory.TotalInventory, ADIInventory.Dallas, ADIInventory.DC_AtlantaHub
                , ADIInventory.DC_Dallas_Hub, ADIInventory.DC_Elk_Grove_Hub, ADIInventory.DC_Feura_Bush, ADIInventory.DC_Louisville_Hub
                , ADIInventory.DC_Reno_Hub, ADIInventory.DC_Richmond_Dist_Ctr, ADIInventory.Oklahama, ADIInventory.RemainingBranches, ADIInventory.LastUpdate);
        }

        public int Save(long ID, string PART_NUM, int? TotalInventory, int? Dallas, int? DC_AtlantaHub, int? DC_Dallas_Hub, int? DC_Elk_Grove_Hub, int? DC_Feura_Bush, int? DC_Louisville_Hub, int? DC_Reno_Hub, int? DC_Richmond_Dist_Ctr, int? Oklahama, int? RemainingBranches, DateTime? LastUpdate)
        {
            var inv = GetData(PART_NUM);
            var count = inv.Count();
            if (count == 0)
                return Insert(ID, PART_NUM, TotalInventory, Dallas, DC_AtlantaHub, DC_Dallas_Hub, DC_Elk_Grove_Hub, DC_Feura_Bush, DC_Louisville_Hub, DC_Reno_Hub, DC_Richmond_Dist_Ctr, Oklahama, RemainingBranches, LastUpdate);
            else
                return Update(inv[0].ID, PART_NUM, TotalInventory, Dallas, DC_AtlantaHub, DC_Dallas_Hub, DC_Elk_Grove_Hub, DC_Feura_Bush, DC_Louisville_Hub, DC_Reno_Hub, DC_Richmond_Dist_Ctr, Oklahama, RemainingBranches, LastUpdate);
        }

        
        #endregion

        #region [ Insert / Update ]

        public int Insert(long ID, string PART_NUM, int? TotalInventory, int? Dallas, int? DC_AtlantaHub, int? DC_Dallas_Hub, int? DC_Elk_Grove_Hub
            , int? DC_Feura_Bush, int? DC_Louisville_Hub, int? DC_Reno_Hub, int? DC_Richmond_Dist_Ctr, int? Oklahama, int? RemainingBranches, DateTime? LastUpdate)
        {
            String Query = "INSERT INTO ADIInventory (PART_NUM, TotalInventory, Dallas, DC_AtlantaHub, DC_Dallas_Hub, DC_Elk_Grove_Hub, DC_Feura_Bush, DC_Louisville_Hub, DC_Reno_Hub, DC_Richmond_Dist_Ctr, Oklahama, RemainingBranches, LastUpdate)"
                + " VALUES ( @PART_NUM, @TotalInventory, @Dallas, @DC_AtlantaHub, @DC_Dallas_Hub, @DC_Elk_Grove_Hub, @DC_Feura_Bush, @DC_Louisville_Hub, @DC_Reno_Hub, @DC_Richmond_Dist_Ctr, @Oklahama, @RemainingBranches, @LastUpdate)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddIntegerBigPara("ID", ID);
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

        public int Update(long ID, string PART_NUM, int? TotalInventory, int? Dallas, int? DC_AtlantaHub, int? DC_Dallas_Hub, int? DC_Elk_Grove_Hub
            , int? DC_Feura_Bush, int? DC_Louisville_Hub, int? DC_Reno_Hub, int? DC_Richmond_Dist_Ctr, int? Oklahama, int? RemainingBranches, DateTime? LastUpdate)
        {
            String Query = "UPDATE ADIInventory SET TotalInventory = @TotalInventory, Dallas = @Dallas, "
                + " DC_AtlantaHub = @DC_AtlantaHub, DC_Dallas_Hub = @DC_Dallas_Hub, DC_Elk_Grove_Hub = @DC_Elk_Grove_Hub, "
                + " DC_Feura_Bush = @DC_Feura_Bush, DC_Louisville_Hub = @DC_Louisville_Hub, DC_Reno_Hub = @DC_Reno_Hub, "
                + " DC_Richmond_Dist_Ctr = @DC_Richmond_Dist_Ctr, Oklahama = @Oklahama, RemainingBranches = @RemainingBranches, LastUpdate = @LastUpdate"
                + " WHERE PART_NUM = \"" + PART_NUM + "\" AND ID = " + ID.ToString();
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddIntegerBigPara("ID", ID);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
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

        #endregion

        #region [ Delete ]
        public int Delete(String PART_NUM)
        {
            String Query = "DELETE FROM ADIInventory SET WHERE PART_NUM = \"" + PART_NUM + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return oDm.RunActionQuery();
        }
        public int Delete(String PART_NUM, String PropertyName)
        {
            String Query = "DELETE FROM ADIInventory SET WHERE PART_NUM = \"" + PART_NUM + "\" AND PropertyName = \"" + PropertyName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            //oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            return oDm.RunActionQuery();
        }
        #endregion
        
    }
}
