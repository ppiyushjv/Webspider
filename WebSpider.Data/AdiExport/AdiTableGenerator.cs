using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.Data.AdiExport
{
    public class AdiTableGenerator : DataManager
    {
        #region [ Constructor ]
        public AdiTableGenerator(String ConnectionString)
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
            try { 
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
                    + "AID_LastUpdate TEXT(255))";
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

        #region [ Adi Final Table Update ]
        public String GenerateFinalTableUpdate()
        {
            try
            {
                String Query = String.Empty;
                OleDbDataManager oDm;

                Query = "CREATE TABLE Final_Table ("
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
                    + "AID_LastUpdate TEXT(255))";
                oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated Final_Table Structure";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
