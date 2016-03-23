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
    public class UpdateFinalTableManager : DataManager
    {
        #region [Constructror]
        public UpdateFinalTableManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<Final_Table> GetData()
        {
            String Query = "SELECT * FROM Final_Table";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<Final_Table>(oDm.GetTable());
        }

        public List<Final_Table> GetDataByAdiPart(String AID_PART)
        {
            String Query = "SELECT * FROM Final_Table WHERE AID_PART = \"" + AID_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            return DataParser.ToList<Final_Table>(oDm.GetTable());
        }

        #endregion


        #region [ Save ]
        public int SaveAdiPart(Final_Table finalTable)
        {
            return SaveAdiPart(finalTable.ID, finalTable.UPC, finalTable.VDR_PART, finalTable.VDR_IT_DSC
                , finalTable.Image_Folder, finalTable.AID_SOURCE_ID, finalTable.AID_PART, finalTable.AID_COST == null ? 0 : (decimal) finalTable.AID_COST
                , finalTable.AID_IMG1, finalTable.AID_IMG2, finalTable.AID_VENDOR, finalTable.AID_INV
                , finalTable.AID_LastUpdate);
        }

        public int SaveAdiPart(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, String AID_LastUpdate)
        {
            var products = GetDataByAdiPart(AID_PART);
            if (products.Count == 1)
                return Update(products[0].ID, UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_LastUpdate);
            else
                return Insert(ID, UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_LastUpdate);
        }
        #endregion

        #region [ Insert / Update ]

        public int Insert(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, String AID_LastUpdate)
        {
            String Query = "INSERT INTO Final_Table (UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_LastUpdate)"
                + " VALUES (@UPC, @VDR_PART, @VDR_IT_DSC, @Image_Folder, @AID_SOURCE_ID, @AID_PART, @AID_COST, @AID_IMG1, @AID_IMG2, @AID_VENDOR, @AID_INV, @AID_LastUpdate)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddIntegerBigPara("ID", ID);
            oDm.AddVarcharPara("UPC", 4000, UPC);
            oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddVarcharPara("VDR_IT_DSC", 4000, VDR_IT_DSC);
            oDm.AddVarcharPara("Image_Folder", 4000, Image_Folder);
            oDm.AddVarcharPara("AID_SOURCE_ID", 4000, AID_SOURCE_ID);
            oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            oDm.AddDecimalPara("AID_COST", 2, 10, AID_COST);
            oDm.AddVarcharPara("AID_IMG1", 4000, AID_IMG1);
            oDm.AddVarcharPara("AID_IMG2", 4000, AID_IMG2);
            oDm.AddVarcharPara("AID_VENDOR", 4000, AID_VENDOR);
            oDm.AddVarcharPara("AID_INV", 4000, AID_INV);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            //oDm.AddIntegerPara("ADI_LeastCount", ADI_LeastCount);
            //oDm.AddBoolPara("ADI_Priority", ADI_Priority);
            return oDm.RunActionQuery();
        }

        public int Update(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, String AID_LastUpdate)
        {
            String Query = "UPDATE Final_Table SET [UPC] = @UPC, [VDR_PART] = @VDR_PART, [VDR_IT_DSC] = @VDR_IT_DSC, [Image_Folder] = @Image_Folder, [AID_SOURCE_ID] = @AID_SOURCE_ID,"
                + " [AID_PART] = @AID_PART, [AID_COST] = @AID_COST, [AID_IMG1] = @AID_IMG1, [AID_IMG2] = @AID_IMG2, [AID_VENDOR] = @AID_VENDOR, [AID_INV] = @AID_INV, [AID_LastUpdate] = @AID_LastUpdate"
                + " WHERE [ID] = " + ID.ToString(); // " @ID";

            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddIntegerPara("@ID", (int) ID);
            oDm.AddVarcharPara("@UPC", 4000, UPC);
            oDm.AddVarcharPara("@VDR_PART", 4000, VDR_PART);
            oDm.AddVarcharPara("@VDR_IT_DSC", 4000, VDR_IT_DSC);
            oDm.AddVarcharPara("@Image_Folder", 4000, Image_Folder);
            oDm.AddVarcharPara("@AID_SOURCE_ID", 4000, AID_SOURCE_ID);
            oDm.AddVarcharPara("@AID_PART", 4000, AID_PART);
            oDm.AddDecimalPara("@AID_COST", 2, 10, AID_COST);
            oDm.AddVarcharPara("@AID_IMG1", 4000, AID_IMG1);
            oDm.AddVarcharPara("@AID_IMG2", 4000, AID_IMG2);
            oDm.AddVarcharPara("@AID_VENDOR", 4000, AID_VENDOR);
            oDm.AddVarcharPara("@AID_INV", 4000, AID_INV);
            oDm.AddVarcharPara("@AID_LastUpdate", 4000, AID_LastUpdate);
            return oDm.RunActionQuery();
        }

        #endregion
        

        public int Delete(Int64 ID)
        {
            String Query = "DELETE FROM Final_Table SET WHERE ID = \"" + ID + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddIntegerBigPara("ID", ID);
            //oDm.AddVarcharPara("UPC", 4000, UPC);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            //oDm.AddVarcharPara("VDR_IT_DSC", 4000, VDR_IT_DSC);
            //oDm.AddVarcharPara("Image_Folder", 4000, Image_Folder);
            //oDm.AddVarcharPara("AID_SOURCE_ID", 4000, AID_SOURCE_ID);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            //oDm.AddDecimalPara("AID_COST", 2, 10, AID_COST);
            //oDm.AddVarcharPara("AID_IMG1", 4000, AID_IMG1);
            //oDm.AddVarcharPara("AID_IMG2", 4000, AID_IMG2);
            //oDm.AddVarcharPara("AID_VENDOR", 4000, AID_VENDOR);
            //oDm.AddVarcharPara("AID_INV", 4000, AID_INV);
            //oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            return oDm.RunActionQuery();
        }

        public List<FinalTable> GetProductByPARTNO(String VDR_PART)
        {
            String Query = "SELECT * FROM Final_Table WHERE VDR_PART = \"" + VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        public int UpdatePriceByPartNO(decimal? AID_COST, string VDR_PART)
        {
            String Query = "UPDATE Final_Table SET AID_COST = @AID_COST, AID_LastUpdate = @AID_LastUpdate WHERE VDR_PART = \"" + VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddDecimalPara("AID_COST", 2, 10, AID_COST);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, DateTime.Now.ToString());
            return oDm.RunActionQuery();
        }

        public int UpdateImageByPartNo(string Image_Folder, string AID_IMG1, string AID_IMG2, string VDR_PART)
        {
            String Query = "UPDATE Final_Table SET Image_Folder = @Image_Folder, AID_IMG1 = @AID_IMG1, AID_IMG2 = @AID_IMG2, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE VDR_PART = \"" + VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddVarcharPara("Image_Folder", 4000, Image_Folder);
            oDm.AddVarcharPara("AID_IMG1", 4000, AID_IMG1);
            oDm.AddVarcharPara("AID_IMG2", 4000, AID_IMG2);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, DateTime.Now.ToString());
            return oDm.RunActionQuery();
        }

        public int ValidAidProduct(string VDR_PART)
        {
            String Query = "SELECT COUNT(1) FROM Final_Table WHERE VDR_PART = \"" + VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            return (int) oDm.GetTable().Rows[0][0];
        }

        public int UpdateInvBYPartNO(String AID_INV, String AID_LastUpdate, String VDR_PART)
        {
            String Query = "UPDATE Final_Table SET AID_INV = @AID_INV, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE VDR_PART = \"" + VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddVarcharPara("AID_INV", 4000, AID_INV);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            return oDm.RunActionQuery();
        }

        //public int UpdateFlagsByAdiProduct(String AID_PART, Boolean ADI_ClearanceZone
        //    , Boolean ADI_HotDeals, Boolean ADI_OnlineSpecials, Boolean ADI_SaleCenter, Boolean ADI_InStock, String AID_LastUpdate)
        //{
        //    String Query = "UPDATE Final_Table SET ADI_ClearanceZone = @ADI_ClearanceZone, "
        //        + "ADI_HotDeals = @ADI_HotDeals, ADI_OnlineSpecials = @ADI_OnlineSpecials, ADI_SaleCenter = @ADI_SaleCenter, "
        //        + "ADI_InStock = @ADI_InStock, AID_LastUpdate = @AID_LastUpdate"
        //        + " WHERE AID_PART = \"" + AID_PART + "\"";
        //    OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
        //    oDm.AddBoolPara("ADI_ClearanceZone",  ADI_ClearanceZone);
        //    oDm.AddBoolPara("ADI_HotDeals",  ADI_HotDeals);
        //    oDm.AddBoolPara("ADI_OnlineSpecials",  ADI_OnlineSpecials);
        //    oDm.AddBoolPara("ADI_SaleCenter",  ADI_SaleCenter);
        //    oDm.AddBoolPara("ADI_InStock",  ADI_InStock);
        //    //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
        //    oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
        //    return oDm.RunActionQuery();
        //}

        public List<FinalTable> GetPriorityData(Boolean ADI_Priority)
        {
            String Query = "SELECT * FROM Final_Table WHERE ADI_Priority = \"" + ADI_Priority + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddBoolPara("ADI_Priority", ADI_Priority);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        public List<Final_Table> GetAllAdiProducts()
        {
            String Query = "SELECT * FROM Final_Table WHERE AID_PART IS NOT NULL";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<Final_Table>(oDm.GetTable());
        }
    }
}
