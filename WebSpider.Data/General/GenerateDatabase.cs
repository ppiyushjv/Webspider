using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSpider.Data.General
{
    public class GenerateDatabase
    {
        private const String DbFile = "WebSpiderDB.sdf";

        public static void Generate()
        {
            /* get the Path */
            String dirPath = Application.StartupPath + "\\App_Code\\";
            var directoryName = System.IO.Path.GetDirectoryName(dirPath);
            var fileName = System.IO.Path.Combine(directoryName, DbFile);

            string connStr = @"Data Source = " + fileName;// +";Mode = Exclusive";

            using (SqlCeConnection conn = new SqlCeConnection(connStr))
            {
                String Query = String.Empty;
                try
                {
                    Query = "DELETE FROM [TaskHeader]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [TaskDetail]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [FinalExport]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [Final_Table]";
                    RunDDL(conn, Query);
                } catch {}

                #region [ AdiGlobal ]
                try {
                    Query = "DELETE FROM [ADIProduct1]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIProduct]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIInventoryExport]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIInventory]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIChild]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADICategoryExport]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADICategory]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIBrands]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIBrands]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIBrands]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIBrands]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [ADIBrands]";
                    RunDDL(conn, Query);
                } catch {}
                #endregion

                #region [ SecLock ]
                try {
                    Query = "DELETE FROM [SecLockCategory]";
                    RunDDL(conn, Query);
                } catch {}
                try {
                    Query = "DELETE FROM [SecLockManufacturer]";
                    RunDDL(conn, Query);
                } catch {}
                try
                {
                    Query = "DELETE FROM [SecLockManufacturerSeries]";
                    RunDDL(conn, Query);
                }
                catch { }
                try {
                    Query = "DELETE FROM [SecLockProduct]";
                    RunDDL(conn, Query);
                } catch {}
                #endregion

                conn.Close();
                conn.Dispose();
            }


            /* check if exists */
            if (File.Exists(fileName))
                File.Delete(fileName);
            

            /* create Database */
            SqlCeEngine engine = new SqlCeEngine(connStr);
            engine.CreateDatabase();


            /* create table and columns */
            using (SqlCeConnection conn = new SqlCeConnection(connStr))
            {
                String Query = String.Empty;
                #region [ Temp Tables ]
                Query = "CREATE TABLE [TaskHeader] ([ScheduleID] bigint IDENTITY (1,1) NOT NULL, [TaskName] nvarchar(255) NOT NULL, [TaskDescription] nvarchar(4000) NOT NULL, [Site] nvarchar(255) NOT NULL, [ScheduleFrom] datetime NOT NULL, [TaskRepeat] bit NULL, [TaskRepeatInterval] int NOT NULL, [TaskRepeatUnit] nvarchar(20) NOT NULL, [Enabled] bit NULL, [LastRun] datetime NULL, [NextRun] datetime NULL, [CreatedDate] datetime NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [TaskDetail] ([TaskID] bigint IDENTITY (1,1) NOT NULL, [TaskHeaderID] bigint NOT NULL, [TaskNameText] nvarchar(255) NOT NULL, [TaskNameValue] nvarchar(255) NOT NULL, [TaskStatusText] nvarchar(255) NOT NULL, [TaskStatus] int NULL, [DownloadImages] bit DEFAULT (0) NOT NULL, [IgnitoMode] bit DEFAULT (0) NOT NULL, [TaskType] nvarchar(255) NOT NULL, [TaskMode] nvarchar(255) NOT NULL, [TaskSite] nvarchar(255) NOT NULL, [CreatedOn] datetime NOT NULL, [UpdatedOn] datetime NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [FinalExport] ([ExportSite] nvarchar(255) NULL, [ExportType] nvarchar(255) NULL, [ExportValue] nvarchar(255) NOT NULL, [CreatedDate] datetime DEFAULT (GETDATE()) NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [Final_Table] ([ID] bigint NULL, [UPC] nvarchar(255) NULL, [VDR_PART] nvarchar(255) NULL, [VDR_IT_DSC] nvarchar(255) NULL, [Image_Folder] nvarchar(255) NULL, [AID_SOURCE_ID] nvarchar(225) NULL, [AID_PART] nvarchar(255) NULL, [AID_COST] numeric(10,2) NULL, [AID_IMG1] nvarchar(255) NULL, [AID_IMG2] nvarchar(255) NULL, [AID_VENDOR] nvarchar(255) NULL, [AID_INV] nvarchar(255) NULL, [AID_LastUpdate] nvarchar(255) NULL)";
                RunDDL(conn, Query);
                #endregion
                
                #region [ AdiGlobal ]
                Query = "CREATE TABLE [ADIProduct1] ([ID] bigint IDENTITY (27468,1) NOT NULL, [AdiNumber] nvarchar(255) NULL, [VendorName] nvarchar(255) NULL, [VendorNumber] nvarchar(255) NULL, [VendorModel] nvarchar(255) NULL, [PartNumber] nvarchar(255) NULL, [Name] nvarchar(255) NULL, [Url] nvarchar(255) NULL, [AllowedToBuy] nvarchar(255) NULL, [DangerousGoodsMessage] nvarchar(255) NULL, [InventoryMessage] nvarchar(255) NULL, [MarketingMessage] nvarchar(255) NULL, [MinQty] numeric(10,2) NULL, [ModelNumber] nvarchar(255) NULL, [Price] numeric(10,2) NULL, [ProductDescription] nvarchar(255) NULL, [ProductImagePath] nvarchar(255) NULL, [RecycleFee] nvarchar(255) NULL, [SaleMessageIndicator] nvarchar(255) NULL, [SaleType] nvarchar(255) NULL, [ST] nvarchar(255) NULL, [SMI] nvarchar(255) NULL, [InventoryMessageCode] nvarchar(255) NULL, [CatagoryID] nvarchar(255) NULL, [SmallImage] nvarchar(255) NULL, [BigImage] nvarchar(255) NULL, [ClearanceZone] bit DEFAULT (0) NOT NULL, [SaleCenter] bit DEFAULT (0) NOT NULL, [OnlineSpecials] bit DEFAULT (0) NOT NULL, [HotDeals] bit DEFAULT (0) NOT NULL, [InStock] bit DEFAULT (0) NOT NULL, [IsUpdating] bit NULL, [UpdateInterval] int NULL, [PriorityProduct] BIT, [LeastCount] int DEFAULT 0, [LastUpdateDatetime] datetime NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADIProduct] ([ID] bigint NOT NULL, [VENDOR_TITLE_NAME] nvarchar(255) NULL, [PART_NUM] nvarchar(255) NULL, [Cost] float NULL, [CAT_CODE] nvarchar(255) NULL, [BRAND_VALUE] nvarchar(255) NULL, [VDR_PART] nvarchar(255) NULL, [VDR_IT_DSC] nvarchar(4000) NULL, [Image_Folder] nvarchar(255) NULL, [AID_IMG1] nvarchar(255) NULL, [AID_IMG2] nvarchar(255) NULL, [CR Cost] float NULL, [MKT_MSG] nvarchar(4000) NULL, [LastUpdate] datetime NULL, [IsUpdating] bit NULL, [UpdateInterval] int NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADIInventoryExport] ([PART_NUM] nvarchar(255) NULL, [TotalInventory] int NULL, [Dallas] int NULL, [DC_AtlantaHub] int NULL, [DC_Dallas_Hub] int NULL, [DC_Elk_Grove_Hub] int NULL, [DC_Feura_Bush] int NULL, [DC_Louisville_Hub] int NULL, [DC_Reno_Hub] int NULL, [DC_Richmond_Dist_Ctr] int NULL, [Oklahama] int NULL, [RemainingBranches] int NULL, [LastUpdate] datetime NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADIInventory] ([AdiNumber] nvarchar(255) NOT NULL, [id] nvarchar(255) NULL, [dc] nvarchar(255) NULL, [region] nvarchar(255) NULL, [storeName] nvarchar(255) NULL, [address1] nvarchar(255) NULL, [address2] nvarchar(255) NULL, [address3] nvarchar(255) NULL, [country] nvarchar(255) NULL, [city] nvarchar(255) NULL, [state] nvarchar(255) NULL, [stateName] nvarchar(255) NULL, [zip] nvarchar(255) NULL, [phone] nvarchar(255) NULL, [fax] nvarchar(255) NULL, [lat] real NULL, [lon] real NULL, [inventory] nvarchar(255) NULL, [manager] nvarchar(255) NULL, [responseCode] nvarchar(255) NULL, [responseMessage] nvarchar(255) NULL, [IsHub] bit NULL, [LastUpdate] datetime NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADIChild] ([ID] bigint IDENTITY (1,1) NOT NULL, [PART_NUM] nvarchar(255) NULL, [PropertyName] nvarchar(255) NULL, [PropertyValue] nvarchar(4000) NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADICategoryExport] ([RootValue] nvarchar(255) NULL, [RootDisplayName] nvarchar(4000) NULL, [ParentValue] nvarchar(255) NULL, [ParentDisplayName] nvarchar(4000) NULL, [Value] nvarchar(255) NOT NULL, [DisplayName] nvarchar(4000) NULL, [CategoryUrl] nvarchar(4000) NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADICategory] ([ParentValue] nvarchar(255) NULL, [Value] nvarchar(255) NOT NULL, [DisplayName] nvarchar(4000) NULL, [CategoryUrl] nvarchar(4000) NULL, [ClearanceZone] bit DEFAULT (0) NOT NULL, [SaleCenter] bit DEFAULT (0) NOT NULL, [OnlineSpecials] bit DEFAULT (0) NOT NULL, [HotDeals] bit DEFAULT (0) NOT NULL, [InStock] bit DEFAULT (0) NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [ADIBrands] ([Value] nvarchar(255) NOT NULL, [DisplayName] nvarchar(255) NULL, [ClearanceZone] bit DEFAULT (0) NOT NULL, [SaleCenter] bit DEFAULT (0) NOT NULL, [OnlineSpecials] bit DEFAULT (0) NOT NULL, [HotDeals] bit DEFAULT (0) NOT NULL, [InStock] bit DEFAULT (0) NOT NULL)";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [TaskHeader] ADD CONSTRAINT [PK__TaskHeader__00000000000005B2] PRIMARY KEY ([ScheduleID])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [TaskDetail] ADD CONSTRAINT [PK__TaskDetail__00000000000002FD] PRIMARY KEY ([TaskID])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [ADIProduct1] ADD CONSTRAINT [PK__ADIProduct1__000000000000037D] PRIMARY KEY ([ID])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [ADIProduct] ADD CONSTRAINT [PK__ADIProduct__0000000000000063] PRIMARY KEY ([ID])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [ADIChild] ADD CONSTRAINT [PK__ADIChild__0000000000000228] PRIMARY KEY ([ID])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [ADIBrands] ADD CONSTRAINT [PK__ADIBrands__00000000000003B8] PRIMARY KEY ([Value])";
                RunDDL(conn, Query);
                #endregion
                
                #region [ SecLock ]
                Query = "CREATE TABLE [SecLockManufacturer] ([Code] nvarchar(100) NOT NULL, [Name] nvarchar(100) NOT NULL, [ImagePath] nvarchar(100) NOT NULL, [Url] nvarchar(100) NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [SecLockManufacturerSeries] ([ID] bigint IDENTITY (1,1) NOT NULL, [ManufacturerCode] nvarchar(100) NOT NULL, [Name] nvarchar(100) NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [SecLockCategory] ([Code] nvarchar(100) NOT NULL, [Name] nvarchar(100) NOT NULL)";
                RunDDL(conn, Query);
                Query = "CREATE TABLE [SecLockProduct] ([Code] nvarchar(100) NOT NULL, [Name] nvarchar(100) NOT NULL, [Url] nvarchar(100) NOT NULL, [ManufacturerCode] nvarchar(100) NULL, [ManufacturerName] nvarchar(100) NULL, [ManufacturerSeries] nvarchar(100) NULL, [CategoyCode] nvarchar(100) NULL, [CategoryName] nvarchar(100) NULL, [YourPrice] numeric(10,2) NULL, [ListPrice] numeric(10,2) NULL, [ImageUrl1] nvarchar(100) NULL, [ImageUrl2] nvarchar(100) NULL, [Stock] int NULL, [Description] nvarchar(4000) NULL, [TechDoc] nvarchar(4000) NULL)";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [SecLockManufacturer] ADD CONSTRAINT [PK_SecLockManufacturer] PRIMARY KEY ([Code])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [SecLockManufacturerSeries] ADD CONSTRAINT [PK_SecLockManufacturerSeries] PRIMARY KEY ([ID])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [SecLockCategory] ADD CONSTRAINT [PK_SecLockCategory] PRIMARY KEY ([Code])";
                RunDDL(conn, Query);
                Query = "ALTER TABLE [SecLockProduct] ADD CONSTRAINT [PK_SecLockProduct] PRIMARY KEY ([Code])";
                RunDDL(conn, Query);
                #endregion

                conn.Close();
                conn.Dispose();
            }

        }


        private static void RunDDL(SqlCeConnection conn, String Query)
        {
            using (SqlCeCommand cmd = new SqlCeCommand(Query, conn))
            {
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
