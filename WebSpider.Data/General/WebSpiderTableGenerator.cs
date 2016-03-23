using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.Data.General
{
    public class WebSpiderTableGenerator : DataManager
    {
        #region [ Constructor ]
        public WebSpiderTableGenerator(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Adi Brand ]
        public String GenerateAdiBrand()
        {
            try {
                String Query = "CREATE TABLE AdiBrand ("
                    + "[Value] TEXT(255), "
                    + "DisplayName TEXT(255), "
                    + "ClearanceZone BIT, "
                    + "SaleCenter BIT, "
                    + "OnlineSpecials BIT, "
                    + "HotDeals BIT)";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'AdiBrand\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Adi Category ]
        public String GenerateAdiCategory()
        {
            try { 
                String Query = "CREATE TABLE AdiCategory ("
                    + "[Value] TEXT(255), "
                    + "DisplayName TEXT(255), "
                    + "ParentValue TEXT(255), "
                    + "CategoryUrl TEXT(255), "
                    + "ClearanceZone BIT, "
                    + "SaleCenter BIT, "
                    + "OnlineSpecials BIT, "
                    + "HotDeals BIT)";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'AdiCategory\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Adi Child ]
        public String GenerateAdiChild()
        {
            try
            {
                String Query = "CREATE TABLE ADIChild ("
                    + "ID  AUTOINCREMENT PRIMARY KEY, "
                    + "PART_NUM TEXT(255), "
                    + "PropertyName TEXT(255), "
                    + "PropertyValue MEMO)";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'ADIChild\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Adi Inventory ]
        public String GenerateAdiInventory()
        {
            try { 
                String Query = "CREATE TABLE ADIInventory ("
                    + "ID  AUTOINCREMENT PRIMARY KEY, "
                    + "PART_NUM TEXT(255), "
                    + "TotalInventory NUMBER, "
                    + "Dallas NUMBER, "
                    + "DC_AtlantaHub NUMBER, "
                    + "DC_Dallas_Hub NUMBER, "
                    + "DC_Elk_Grove_Hub NUMBER, "
                    + "DC_Feura_Bush NUMBER, "
                    + "DC_Louisville_Hub NUMBER, "
                    + "DC_Reno_Hub NUMBER, "
                    + "DC_Richmond_Dist_Ctr NUMBER, "
                    + "Oklahama NUMBER, "
                    + "RemainingBranches NUMBER, "
                    + "LastUpdate DATETIME)";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'ADIInventory\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Adi Inventory Details ]
        public String GenerateAdiInventoryDetails()
        {
            try { 
                String Query = "CREATE TABLE AdiInventoryDetails ("
                    + "AdiNumber  TEXT(255), "
                    + "id TEXT(255), "
                    + "dc TEXT(255), "
                    + "region TEXT(255), "
                    + "storeName TEXT(255), "
                    + "address1 TEXT(255), "
                    + "address2 TEXT(255), "
                    + "address3 TEXT(255), "
                    + "country TEXT(255), "
                    + "city TEXT(255), "
                    + "state TEXT(255), "
                    + "stateName TEXT(255), "
                    + "zip TEXT(255), "
                    + "phone TEXT(255), "
                    + "fax TEXT(255), "
                    + "lat SINGLE, "
                    + "lon SINGLE, "
                    + "inventory TEXT(255), "
                    + "manager TEXT(255), "
                    + "responseCode TEXT(255), "
                    + "responseMessage TEXT(255), "
                    + "IsHub BIT, "
                    + "LastUpdate TEXT(255))";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'AdiInventoryDetails\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ SecLock Category ]
        public String GenerateSecLockCategory()
        {
            try
            {
                String Query = "CREATE TABLE SecLockCategory ("
                    + "Code TEXT(255), "
                    + "Name TEXT(255))";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'SecLockCategory\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ SecLock Manufacturers ]
        public String GenerateSecLockManufacturer()
        {
            try
            {
                String Query = "CREATE TABLE SecLockManufacturer ("
                    + "Code TEXT(255), "
                    + "Name TEXT(255), "
                    + "ImagePath TEXT(255), "
                    + "Url TEXT(255))";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'SecLockManufacturer\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ SecLock Manufacturers ]
        public String GenerateSecLockManufacturerSeries()
        {
            try
            {
                String Query = "CREATE TABLE SecLockManufacturerSeries ("
                    + "ID NUMBER, "
                    + "ManufacturerCode TEXT(100), "
                    + "Name TEXT(100)) ";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'SecLockManufacturer\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Adi Final Table ]
        public String GenerateFinalTable()
        {
            try {
                String Query = "CREATE TABLE Final_Table ("
                    + "ID  AUTOINCREMENT PRIMARY KEY, "
                    + "UPC TEXT(255), "
                    + "VDR_PART TEXT(255), "
                    + "VDR_IT_DSC TEXT(255), "
                    + "Image_Folder TEXT(255), "
                    + "AID_SOURCE_ID TEXT(255), "
                    + "AID_PART TEXT(255), "
                    + "AID_COST CURRENCY, "
                    + "AID_IMG1 TEXT(255), "
                    + "AID_IMG2 TEXT(255), "
                    + "AID_VENDOR TEXT(255), "
                    + "AID_INV TEXT(255), "
                    + "AID_ClearanceZone YesNo DEFAULT No, "
                    + "AID_HotDeals YesNo DEFAULT No, "
                    + "AID_OnlineSpecials YesNo DEFAULT No, "
                    + "AID_SaleCenter YesNo DEFAULT No, "
                    + "AID_InStock YesNo DEFAULT No, "
                    //+ "ADI_LeastCount NUMBER, "
                    //+ "ADI_Priority TEXT(255), "
                    + "AID_LastUpdate TEXT(255), "
                    + "SLD_SOURCE_ID TEXT(255), "
                    + "SLD_COST CURRENCY, "
                    + "SLD_PART TEXT(255), "
                    + "SLD_IMG1 TEXT(255), "
                    + "SLD_IMG2 TEXT(255), "
                    + "SLD_VENDOR TEXT(255), "
                    + "SLD_INV TEXT(255), "
                    + "SLD_DESC MEMO, "
                    + "SLD_TECHDOC MEMO, "
                    + "SLD_LastUpdate TEXT(255))";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();

                return "Generated \'Final_Table\' Structure";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Generate ]
        public static String Generate(String FileName)
        {
            List<String> Messages = new List<String>();
            try
            {
                //String FileName = Settings.GetValue("WebSpiderDB");
                String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", FileName);
                WebSpiderTableGenerator gen = new WebSpiderTableGenerator(ConnStr);
                Messages.Add(String.Format("Using File \'{0}\'", FileName));
                Messages.Add(gen.GenerateAdiBrand());
                Messages.Add(gen.GenerateAdiCategory());
                Messages.Add(gen.GenerateFinalTable());
                Messages.Add(gen.GenerateAdiChild());
                Messages.Add(gen.GenerateAdiInventoryDetails());
                Messages.Add(gen.GenerateAdiInventory());
                Messages.Add(gen.GenerateSecLockManufacturer());
                Messages.Add(gen.GenerateSecLockManufacturerSeries());
                Messages.Add(gen.GenerateSecLockCategory());
            }
            catch (Exception ex)
            {
                Messages.Add(ex.ToString());
            }
            return String.Join("\n", Messages);
        }
        #endregion
    }
}
