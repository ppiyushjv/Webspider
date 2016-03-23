using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.TriGlobal.Objects.TriGlobal;

namespace WebSpider.TriGlobal.Data.TriGlobal
{
    public class TriProductManager : DataManager
    {
        #region [ Constructor ]
        public TriProductManager(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion
        

        private const String Table = "TriProduct";

        public List<TriProduct> GetData()
        {
            String Query = "SELECT * FROM TriProduct1 WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.ConnectionString = this.ConnectionString;
            return DataParser.ToList<TriProduct>(oDm.GetTable());
        }

        public List<TriProduct> GetDataByNextUpdateDateTime()
        {
            String Query = "SELECT * FROM TriProduct1 WITH (NOLOCK) WHERE DATEADD(n, UpdateInterval, LastUpdate)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<TriProduct>(oDm.GetTable());
        }

        public List<TriProduct> GetDataByPartNum(String TriNumber)
        {
            String Query = "SELECT * FROM TriProduct1 WITH (NOLOCK) WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("@TriNumber", 4000, TriNumber);
            return DataParser.ToList<TriProduct>(oDm.GetTable());
        }

        public Int64? GetIDByPartNumber(String TriNumber)
        {
            String Query = "SELECT ID FROM TriProduct1 WITH (NOLOCK) WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("@TriNumber", 4000, TriNumber);
            var oDt = oDm.GetTable();
            if (oDt.Rows.Count == 0)
                return null;
            else
                return (Int64) oDt.Rows[0][0];
        }

        public List<TriProduct> GetProductsByCatagory(String CatagoryID)
        {
            String Query = "SELECT ID FROM TriProduct1 WITH (NOLOCK) WHERE CatagoryID = @CatagoryID";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("@CatagoryID", 4000, CatagoryID);
            return DataParser.ToList<TriProduct>(oDm.GetTable());
        }

        public List<TriProduct> GetAllPriorityProducts()
        {
            String Query = "SELECT * FROM TriProduct1 WITH (NOLOCK) WHERE PriorityProduct = 1 AND LastUpdateDatetime <= DATEADD(d, -1, GETDATE())";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.ConnectionString = this.ConnectionString;
            return DataParser.ToList<TriProduct>(oDm.GetTable());
        }

        #region [ Insert / Update ]
        public int InsertNew(
            Int64 ID,
            String TriNumber,
            String VendorName,
            String VendorNumber,
            String VendorModel,
            String PartNumber,
            String Name,
            String Url,
            String AllowedToBuy,
            String DangerousGoodsMessage,
            String InventoryMessage,
            String MarketingMessage,
            decimal? MinQty,
            String ModelNumber,
            decimal? Price,
            String ProductDescription,
            String ProductImagePath,
            String RecycleFee,
            String SaleMessageIndicator,
            String SaleType,
            String ST,
            String SMI,
            String InventoryMessageCode,
            String CatagoryID,
            String SmallImage,
            String BigImage,
            bool IsUpdating,
            int UpdateInterval,
            DateTime LastUpdateDatetime
        )
        {
            String @Query = "INSERT INTO TriProduct1 WITH (ROWLOCK) (TriNumber, VendorName, VendorNumber, VendorModel, PartNumber, Name, "
                + "Url, AllowedToBuy, DangerousGoodsMessage, InventoryMessage, MarketingMessage, MinQty, ModelNumber, Price, ProductDescription, "
                + "ProductImagePath, RecycleFee, SaleMessageIndicator, SaleType, ST, SMI, InventoryMessageCode, CatagoryID, SmallImage, BigImage, "
                + "IsUpdating, UpdateInterval, LastUpdateDatetime) "
                + "VALUES ( @TriNumber, @VendorName, @VendorNumber, @VendorModel, @PartNumber, @Name, "
                + "@Url, @AllowedToBuy, @DangerousGoodsMessage, @InventoryMessage, @MarketingMessage, @MinQty, @ModelNumber, @Price, @ProductDescription, "
                + "@ProductImagePath, @RecycleFee, @SaleMessageIndicator, @SaleType, @ST, @SMI, @InventoryMessageCode, @CatagoryID, @SmallImage, @BigImage, "
                + "@IsUpdating, @UpdateInterval, @LastUpdateDatetime)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddVarcharPara("VendorName", 4000, VendorName);
            oDm.AddVarcharPara("VendorNumber", 4000, VendorNumber);
            oDm.AddVarcharPara("VendorModel", 4000, VendorModel);
            oDm.AddVarcharPara("PartNumber", 4000, PartNumber);
            oDm.AddVarcharPara("Name", 4000, Name);
            oDm.AddVarcharPara("Url", 4000, Url);
            oDm.AddVarcharPara("AllowedToBuy", 4000, AllowedToBuy);
            oDm.AddVarcharPara("DangerousGoodsMessage", 4000, DangerousGoodsMessage);
            oDm.AddVarcharPara("InventoryMessage", 4000, InventoryMessage);
            oDm.AddVarcharPara("MarketingMessage", 4000, MarketingMessage);
            oDm.AddDecimalPara("MinQty", 2, 10, MinQty);
            oDm.AddVarcharPara("ModelNumber", 4000, ModelNumber);
            oDm.AddDecimalPara("Price", 2, 10, Price);
            oDm.AddVarcharPara("ProductDescription", 4000, ProductDescription);
            oDm.AddVarcharPara("ProductImagePath", 4000, ProductImagePath);
            oDm.AddVarcharPara("RecycleFee", 4000, RecycleFee);
            oDm.AddVarcharPara("SaleMessageIndicator", 4000, SaleMessageIndicator);
            oDm.AddVarcharPara("SaleType", 4000, SaleType);
            oDm.AddVarcharPara("ST", 4000, ST);
            oDm.AddVarcharPara("SMI", 4000, SMI);
            oDm.AddVarcharPara("InventoryMessageCode", 4000, InventoryMessageCode);
            oDm.AddVarcharPara("CatagoryID", 4000, CatagoryID);
            oDm.AddVarcharPara("SmallImage", 4000, SmallImage);
            oDm.AddVarcharPara("BigImage", 4000, BigImage);
            oDm.AddBoolPara("IsUpdating", IsUpdating);
            oDm.AddIntegerPara("UpdateInterval", UpdateInterval);
            oDm.AddDateTimePara("LastUpdateDatetime", LastUpdateDatetime);
            return oDm.RunActionQuery();
        }

        public int UpdateByID(
            Int64 ID,
            String TriNumber,
            String VendorName,
            String VendorNumber,
            String VendorModel,
            String PartNumber,
            String Name,
            String Url,
            String AllowedToBuy,
            String DangerousGoodsMessage,
            String InventoryMessage,
            String MarketingMessage,
            decimal? MinQty,
            String ModelNumber,
            decimal? Price,
            String ProductDescription,
            String ProductImagePath,
            String RecycleFee,
            String SaleMessageIndicator,
            String SaleType,
            String ST,
            String SMI,
            String InventoryMessageCode,
            String CatagoryID,
            String SmallImage,
            String BigImage,
            bool IsUpdating,
            int UpdateInterval,
            DateTime LastUpdateDatetime
        )
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET TriNumber = @TriNumber, VendorName = @VendorName, VendorNumber = @VendorNumber, "
                + "VendorModel = @VendorModel, PartNumber = @PartNumber, Name = @Name, Url = @Url, AllowedToBuy = @AllowedToBuy, "
                + "DangerousGoodsMessage = @DangerousGoodsMessage, InventoryMessage = @InventoryMessage, MarketingMessage = @MarketingMessage, "
                + "MinQty = @MinQty, ModelNumber = @ModelNumber, Price = @Price, ProductDescription = @ProductDescription, ProductImagePath = @ProductImagePath, "
                + "RecycleFee = @RecycleFee, SaleMessageIndicator = @SaleMessageIndicator, SaleType = @SaleType, ST = @ST, SMI = @SMI, "
                + "InventoryMessageCode = @InventoryMessageCode, CatagoryID = @CatagoryID, SmallImage = @SmallImage, BigImage = @BigImage, "
                + "IsUpdating = @IsUpdating, UpdateInterval = @UpdateInterval, LastUpdateDatetime = @LastUpdateDatetime "
                + "WHERE ID = @ID";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddIntegerBigPara("ID", ID);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddVarcharPara("VendorName", 4000, VendorName);
            oDm.AddVarcharPara("VendorNumber", 4000, VendorNumber);
            oDm.AddVarcharPara("VendorModel", 4000, VendorModel);
            oDm.AddVarcharPara("PartNumber", 4000, PartNumber);
            oDm.AddVarcharPara("Name", 4000, Name);
            oDm.AddVarcharPara("Url", 4000, Url);
            oDm.AddVarcharPara("AllowedToBuy", 4000, AllowedToBuy);
            oDm.AddVarcharPara("DangerousGoodsMessage", 4000, DangerousGoodsMessage);
            oDm.AddVarcharPara("InventoryMessage", 4000, InventoryMessage);
            oDm.AddVarcharPara("MarketingMessage", 4000, MarketingMessage);
            oDm.AddDecimalPara("MinQty", 2, 10, MinQty);
            oDm.AddVarcharPara("ModelNumber", 4000, ModelNumber);
            oDm.AddDecimalPara("Price", 2, 10, Price);
            oDm.AddVarcharPara("ProductDescription", 4000, ProductDescription);
            oDm.AddVarcharPara("ProductImagePath", 4000, ProductImagePath);
            oDm.AddVarcharPara("RecycleFee", 4000, RecycleFee);
            oDm.AddVarcharPara("SaleMessageIndicator", 4000, SaleMessageIndicator);
            oDm.AddVarcharPara("SaleType", 4000, SaleType);
            oDm.AddVarcharPara("ST", 4000, ST);
            oDm.AddVarcharPara("SMI", 4000, SMI);
            oDm.AddVarcharPara("InventoryMessageCode", 4000, InventoryMessageCode);
            oDm.AddVarcharPara("CatagoryID", 4000, CatagoryID);
            oDm.AddVarcharPara("SmallImage", 4000, SmallImage);
            oDm.AddVarcharPara("BigImage", 4000, BigImage);
            oDm.AddBoolPara("IsUpdating", IsUpdating);
            oDm.AddIntegerPara("UpdateInterval", UpdateInterval);
            oDm.AddDateTimePara("LastUpdateDatetime", LastUpdateDatetime);
            return oDm.RunActionQuery();
        }
        #endregion

        public bool IsUpdating(String TriNumber)
        {
            String Query = "SELECT IsUpdating FROM WITH (NOLOCK) TriProduct1 WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            var dt = oDm.GetTable();
            if (dt.Rows.Count == 0)
                return false;
            else
                return (bool)dt.Rows[0][0];
        }

        public int SetUpdateInterval(String TriNumber, int UpdateInterval)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET UpdateInterval = @UpdateInterval WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddIntegerPara("UpdateInterval", UpdateInterval);
            return oDm.RunActionQuery();
        }

        public int SetProductPriority(String TriNumber, Boolean PriorityProduct, Int32 LeastCount)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET PriorityProduct = @PriorityProduct, LeastCount = @LeastCount WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddBoolPara("PriorityProduct", PriorityProduct);
            oDm.AddIntegerPara("LeastCount", LeastCount);
            return oDm.RunActionQuery();
        }

        public int SetUpdating(String TriNumber, Boolean IsUpdating)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET IsUpdating = @IsUpdating WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddBoolPara("IsUpdating", IsUpdating);
            return oDm.RunActionQuery();
        }

        public int UpdateImageByID(Int64 ID, String SmallImage, String BigImage)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET SmallImage = @SmallImage, BigImage = @BigImage WHERE ID = @ID";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("SmallImage", 4000, SmallImage);
            oDm.AddVarcharPara("BigImage", 4000, BigImage);
            oDm.AddIntegerBigPara("ID", ID);
            return oDm.RunActionQuery();
        }

        #region [ Flagging ]
        public int SetClearanceZone(String TriNumber)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET ClearanceZone = 1 WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            return oDm.RunActionQuery();
        }
        public int SetSaleCenter(String TriNumber)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET SaleCenter = 1 WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            return oDm.RunActionQuery();
        }
        public int SetOnlineSpecials(String TriNumber)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET OnlineSpecials = 1 WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            return oDm.RunActionQuery();
        }
        public int SetHotDeals(String TriNumber)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET HotDeals = 1 WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            return oDm.RunActionQuery();
        }
        public int SetStockAvailability(String TriNumber)
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET InStock = 1 WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            return oDm.RunActionQuery();
        }

        #endregion

        #region [ Flag Removal ]
        public int ClearAllClearanceZone()
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET ClearanceZone = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearAllHotDeals()
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET HotDeals = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearAllSaleCenter()
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET SaleCenter = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearAllOnlineSpecials()
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET OnlineSpecials = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }

        public int ClearStockAvailability()
        {
            String Query = "UPDATE TriProduct1 WITH (ROWLOCK) SET InStock = 0";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion





        //public List<TriProduct> GetPriorityData(Boolean PriorityProduct)
        //{
        //    String Query = "SELECT * FROM TriProduct1 WHERE PriorityProduct = @PriorityProduct";
        //    OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
        //    oDm.AddBoolPara("PriorityProduct", PriorityProduct);
        //    return DataParser.ToList<TriProduct>(oDm.GetTable());
        //}
    }
}
