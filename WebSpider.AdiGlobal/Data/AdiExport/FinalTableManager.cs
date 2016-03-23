using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseConnection;
using WebSpider.Data.DatabaseManager;
using WebSpider.AdiGlobal.Objects.AdiExport;

namespace WebSpider.AdiGlobal.Data.AdiExport
{
    public class FinalTableManager : DataManager
    {
        #region [Constructror]
        public FinalTableManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        public void CreateSchema()
        {
            String Query = "CREATE TABLE Final_Table ("
                + "ID AUTOINCREMENT PRIMARY KEY, "
                + "UPC TEXT(255), "
                + "VDR_PART TEXT(255), "
                + "VDR_IT_DSC TEXT(255), "
                + "Image_Folder TEXT(255), "
                + "AID_SOURCE_ID TEXT(255), "
                + "AID_PART TEXT(255), "
                + "AID_COST DECIMAL, "
                + "AID_IMG1 TEXT(255), "
                + "AID_IMG2 TEXT(255), "
                + "AID_VENDOR TEXT(255), "
                + "AID_INV TEXT(255), "
                + "AID_ClearanceZone BIT"
                + "AID_HotDeals BIT"
                + "AID_OnlineSpecials BIT"
                + "AID_SaleCenter BIT"
                + "AID_InStock BIT"
                + "AID_LastUpdate TEXT(255), "
                + "AID_LeastCount INT, "
                + "AID_Priority BIT "
                + ")";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.RunActionQuery();
        }

        //public void AlterSchema()
        //{
        //    List<KeyValuePair<String, Type>> columns = new List<KeyValuePair<string,Type>>();

        //    String Query = "SELECT * FROM Final_Table_Test";
        //    OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
        //    var dt = oDm.GetTable();
        //    foreach (System.Data.DataColumn col in dt.Columns)
        //    {
        //        columns.Add(new KeyValuePair<string, Type>(col.ColumnName, col.DataType));
                 
        //    }

        //    Query = "ALTER TABLE Final_Table_Test "
        //        + "ADD COLUMN ID AUTOINCREMENT PRIMARY KEY, "
        //        + "ADD COLUMN UPC TEXT(255), "
        //        + "ADD COLUMN VDR_PART TEXT(255), "
        //        + "ADD COLUMN VDR_IT_DSC TEXT(255), "
        //        + "ADD COLUMN Image_Folder TEXT(255), "
        //        + "ADD COLUMN AID_SOURCE_ID TEXT(255), "
        //        + "ADD COLUMN AID_PART TEXT(255), "
        //        + "ADD COLUMN AID_COST DECIMAL, "
        //        + "ADD COLUMN AID_IMG1 TEXT(255), "
        //        + "ADD COLUMN AID_IMG2 TEXT(255), "
        //        + "ADD COLUMN AID_VENDOR TEXT(255), "
        //        + "ADD COLUMN AID_INV TEXT(255), "
        //        + "ADD COLUMN AID_LastUpdate TEXT(255), "
        //        + "ADD COLUMN AID_LeastCount INT, "
        //        + "ADD COLUMN AID_Priority BIT "
        //        + ")";
        //    //oDm = new OleDbDataManager(ConnectionString, Query, true);
        //    //oDm.RunActionQuery();
        //}

        #region [ Get Data ]
        public List<FinalTable> GetData()
        {
            String Query = "SELECT * FROM Final_Table WHERE AID_PART IS NOT NULL AND AID_PART <> ''";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        public List<FinalTable> GetDataByAdiPart(String AID_PART)
        {
            String Query = "SELECT * FROM Final_Table WHERE AID_PART = \"" + AID_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        #endregion


        #region [ Save ]
        public int SaveAdiPart(FinalTable finalTable)
        {
            return SaveAdiPart(finalTable.ID, finalTable.UPC, finalTable.VDR_PART, finalTable.VDR_IT_DSC
                , finalTable.Image_Folder, finalTable.ADI_SOURCE_ID, finalTable.ADI_PART, finalTable.ADI_COST == null ? 0 : (decimal) finalTable.ADI_COST
                , finalTable.ADI_IMG1, finalTable.ADI_IMG2, finalTable.ADI_VENDOR, finalTable.ADI_INV
                , finalTable.ADI_LastUpdate);
        }

        public int SaveAdiPart(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, String AID_LastUpdate)
        {
            var count = GetDataByAdiPart(AID_PART).Count();
            if (count == 1)
                return Update(ID, UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_LastUpdate);
            else
                return Insert(ID, UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_LastUpdate);
        }

        public int SaveAdiPart(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, 
            String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, 
            Boolean AID_ClearanceZone, Boolean AID_HotDeals, Boolean AID_OnlineSpecials, Boolean AID_SaleCenter, Boolean AID_InStock, String AID_LastUpdate)
        {
            var count = GetDataByAdiPart(AID_PART).Count();
            if (count == 1)
                return Update(ID, UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_ClearanceZone, AID_HotDeals, AID_OnlineSpecials, AID_SaleCenter, AID_InStock, AID_LastUpdate);
            else
                return Insert(ID, UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_ClearanceZone, AID_HotDeals, AID_OnlineSpecials, AID_SaleCenter, AID_InStock, AID_LastUpdate);
        }
        #endregion

        #region [ Insert / Update ]

        public int Insert(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, String AID_LastUpdate)
        {
            //String Query = "INSERT INTO Final_Table (UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, AID_LastUpdate, AID_LeastCount, AID_Priority)"
            //    + " VALUES (@UPC, @VDR_PART, @VDR_IT_DSC, @Image_Folder, @AID_SOURCE_ID, @AID_PART, @AID_COST, @AID_IMG1, @AID_IMG2, @AID_VENDOR, @AID_INV, @AID_LastUpdate, @AID_LeastCount, @AID_Priority)";

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
            //oDm.AddIntegerPara("AID_LeastCount", AID_LeastCount);
            //oDm.AddBoolPara("AID_Priority", AID_Priority);
            return oDm.RunActionQuery();
        }

        public int Insert(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, 
            String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, 
            Boolean AID_ClearanceZone, Boolean AID_HotDeals, Boolean AID_OnlineSpecials, Boolean AID_SaleCenter, Boolean AID_InStock, String AID_LastUpdate)
        {
            String Query = "INSERT INTO Final_Table (UPC, VDR_PART, VDR_IT_DSC, Image_Folder, AID_SOURCE_ID, AID_PART, AID_COST, AID_IMG1, AID_IMG2, AID_VENDOR, AID_INV, "
                + "AID_ClearanceZone, AID_HotDeals, AID_OnlineSpecials, AID_SaleCenter, AID_InStock, AID_LastUpdate)"
                + " VALUES (@UPC, @VDR_PART, @VDR_IT_DSC, @Image_Folder, @AID_SOURCE_ID, @AID_PART, @AID_COST, @AID_IMG1, @AID_IMG2, @AID_VENDOR, @AID_INV, "
                + "@AID_ClearanceZone, @AID_HotDeals, @AID_OnlineSpecials, @AID_SaleCenter, @AID_InStock, @AID_LastUpdate)";
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
            oDm.AddBoolPara("AID_ClearanceZone", AID_ClearanceZone);
            oDm.AddBoolPara("AID_HotDeals", AID_HotDeals);
            oDm.AddBoolPara("AID_OnlineSpecials", AID_OnlineSpecials);
            oDm.AddBoolPara("AID_SaleCenter", AID_SaleCenter);
            oDm.AddBoolPara("AID_InStock", AID_InStock);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            //oDm.AddIntegerPara("AID_LeastCount", AID_LeastCount);
            //oDm.AddBoolPara("AID_Priority", AID_Priority);
            return oDm.RunActionQuery();
        }

        public int Update(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, String AID_LastUpdate)
        {
            //String Query = "UPDATE Final_Table SET UPC = @UPC, VDR_PART = @VDR_PART, VDR_IT_DSC = @VDR_IT_DSC, Image_Folder = @Image_Folder, AID_SOURCE_ID = @AID_SOURCE_ID, AID_PART = @AID_PART, AID_COST = @AID_COST, AID_IMG1 = @AID_IMG1, AID_IMG2 = @AID_IMG2, AID_VENDOR = @AID_VENDOR, AID_INV = @AID_INV, AID_LastUpdate = @AID_LastUpdate, AID_LeastCount = @AID_LeastCount, AID_Priority = @AID_Priority"
            //    + " WHERE ID = @ID";

            String Query = "UPDATE Final_Table SET UPC = @UPC, VDR_PART = @VDR_PART, VDR_IT_DSC = @VDR_IT_DSC, Image_Folder = @Image_Folder, AID_SOURCE_ID = @AID_SOURCE_ID, AID_PART = @AID_PART, AID_COST = @AID_COST, AID_IMG1 = @AID_IMG1, AID_IMG2 = @AID_IMG2, AID_VENDOR = @AID_VENDOR, AID_INV = @AID_INV, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE ID = " + ID.ToString();
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
            ////oDm.AddIntegerPara("AID_LeastCount", AID_LeastCount);
            ////oDm.AddBoolPara("AID_Priority", AID_Priority);
            return oDm.RunActionQuery();
        }

        public int Update(Int64 ID, String UPC, String VDR_PART, String VDR_IT_DSC, String Image_Folder, String AID_SOURCE_ID, String AID_PART, 
            decimal AID_COST, String AID_IMG1, String AID_IMG2, String AID_VENDOR, String AID_INV, 
            Boolean AID_ClearanceZone, Boolean AID_HotDeals, Boolean AID_OnlineSpecials, Boolean AID_SaleCenter, Boolean AID_InStock, String AID_LastUpdate)
        {
            //String Query = "UPDATE Final_Table SET UPC = @UPC, VDR_PART = @VDR_PART, VDR_IT_DSC = @VDR_IT_DSC, Image_Folder = @Image_Folder, AID_SOURCE_ID = @AID_SOURCE_ID, AID_PART = @AID_PART, AID_COST = @AID_COST, AID_IMG1 = @AID_IMG1, AID_IMG2 = @AID_IMG2, AID_VENDOR = @AID_VENDOR, AID_INV = @AID_INV, AID_LastUpdate = @AID_LastUpdate, AID_LeastCount = @AID_LeastCount, AID_Priority = @AID_Priority"
            //    + " WHERE ID = @ID";

            String Query = "UPDATE Final_Table SET UPC = @UPC, VDR_PART = @VDR_PART, VDR_IT_DSC = @VDR_IT_DSC, Image_Folder = @Image_Folder"
                + ", AID_SOURCE_ID = @AID_SOURCE_ID, AID_PART = @AID_PART, AID_COST = @AID_COST, AID_IMG1 = @AID_IMG1, AID_IMG2 = @AID_IMG2"
                + ", AID_VENDOR = @AID_VENDOR, AID_INV = @AID_INV, AID_ClearanceZone = @AID_ClearanceZone, AID_HotDeals = @AID_HotDeals"
                + ", AID_OnlineSpecials= @AID_OnlineSpecials, AID_SaleCenter = @AID_SaleCenter, AID_InStock = @AID_InStock, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE ID = " + ID.ToString();
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
            oDm.AddBoolPara("AID_ClearanceZone", AID_ClearanceZone);
            oDm.AddBoolPara("AID_HotDeals", AID_HotDeals);
            oDm.AddBoolPara("AID_OnlineSpecials", AID_OnlineSpecials);
            oDm.AddBoolPara("AID_SaleCenter", AID_SaleCenter);
            oDm.AddBoolPara("AID_InStock", AID_InStock);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            ////oDm.AddIntegerPara("AID_LeastCount", AID_LeastCount);
            ////oDm.AddBoolPara("AID_Priority", AID_Priority);
            return oDm.RunActionQuery();
        }
        #endregion
        

        public int Delete(Int64 ID)
        {
            String Query = "DELETE FROM Final_Table SET WHERE ID = @ID";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ID", ID);
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
            String Query = "SELECT * FROM Final_Table WHERE VDR_PART = @VDR_PART";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        public int UpdatePriceByPartNO(decimal? AID_COST, string VDR_PART)
        {
            String Query = "UPDATE Final_Table SET AID_COST = @AID_COST, AID_LastUpdate = @AID_LastUpdate WHERE VDR_PART = \"" + @VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddDecimalPara("AID_COST", 2, 10, AID_COST);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, DateTime.Now.ToString());
            return oDm.RunActionQuery();
        }

        public int UpdateImageByPartNo(string Image_Folder, string AID_IMG1, string AID_IMG2, string VDR_PART)
        {
            String Query = "UPDATE Final_Table SET Image_Folder = @Image_Folder, AID_IMG1 = @AID_IMG1, AID_IMG2 = @AID_IMG2, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE VDR_PART = \"" + @VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddVarcharPara("Image_Folder", 4000, Image_Folder);
            oDm.AddVarcharPara("AID_IMG1", 4000, AID_IMG1);
            oDm.AddVarcharPara("AID_IMG2", 4000, AID_IMG2);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, DateTime.Now.ToString());
            return oDm.RunActionQuery();
        }

        public int ValidADIProduct(string VDR_PART)
        {
            String Query = "SELECT COUNT(1) FROM Final_Table WHERE VDR_PART = @VDR_PART";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            return (int) oDm.GetTable().Rows[0][0];
        }

        public int UpdateInvBYPartNO(String AID_INV, String AID_LastUpdate, String VDR_PART)
        {
            String Query = "UPDATE Final_Table SET AID_INV = @AID_INV, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE VDR_PART = \"" + @VDR_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("VDR_PART", 4000, VDR_PART);
            oDm.AddVarcharPara("AID_INV", 4000, AID_INV);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            return oDm.RunActionQuery();
        }

        public int UpdateFlagsByAdiProduct(String AID_PART, Boolean AID_ClearanceZone
            , Boolean AID_HotDeals, Boolean AID_OnlineSpecials, Boolean AID_SaleCenter, Boolean AID_InStock, String AID_LastUpdate)
        {
            String Query = "UPDATE Final_Table SET AID_ClearanceZone = @AID_ClearanceZone, "
                + "AID_HotDeals = @AID_HotDeals, AID_OnlineSpecials = @AID_OnlineSpecials, AID_SaleCenter = @AID_SaleCenter, "
                + "AID_InStock = @AID_InStock, AID_LastUpdate = @AID_LastUpdate"
                + " WHERE AID_PART = \"" + AID_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddBoolPara("AID_ClearanceZone",  AID_ClearanceZone);
            oDm.AddBoolPara("AID_HotDeals", AID_HotDeals);
            oDm.AddBoolPara("AID_OnlineSpecials",  AID_OnlineSpecials);
            oDm.AddBoolPara("AID_SaleCenter",  AID_SaleCenter);
            oDm.AddBoolPara("AID_InStock",  AID_InStock);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            oDm.AddVarcharPara("AID_LastUpdate", 4000, AID_LastUpdate);
            return oDm.RunActionQuery();
        }

        public List<FinalTable> GetPriorityData(Boolean AID_Priority)
        {
            String Query = "SELECT * FROM Final_Table WHERE AID_Priority = \"" + AID_Priority + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddBoolPara("AID_Priority", AID_Priority);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        public List<FinalTable> GetAllAdiProducts()
        {
            String Query = "SELECT * FROM Final_Table WHERE AID_PART IS NOT NULL";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }
    }
}
