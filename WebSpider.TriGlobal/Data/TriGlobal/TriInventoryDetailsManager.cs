using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.TriGlobal.Objects.TriGlobal;

namespace WebSpider.TriGlobal.Data.TriGlobal
{
    public class TriInventoryDetailsManager : DataManager
    {
        #region [ Constructor ]
        public TriInventoryDetailsManager(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion
        
        public List<TriInventoryDetails> GetData()
        {
            String Query = "SELECT * FROM TriInventory";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return DataParser.ToList<TriInventoryDetails>(oDm.GetTable());
        }

        public List<TriInventoryDetails> GetDataByPartNumber(String TriNumber)
        {
            String Query = "SELECT * FROM TriInventory WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);

            return DataParser.ToList<TriInventoryDetails>(oDm.GetTable());
        }

        public DataTable GetDataTableByPartNumber(String TriNumber)
        {
            String Query = "SELECT * FROM TriInventory WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);

            return oDm.GetTable();
        }


        public int Insert(String TriNumber, String id, String dc, String region, String storeName, String address1, String address2, String address3
            , String country, String city, String state, String stateName, String zip, String phone, String fax, float? lat, float? lon, String inventory
            , String manager, String responseCode, String responseMessage, Boolean IsHub)
        {
            String Query = "INSERT INTO TriInventory (TriNumber, id ,dc ,region ,storeName ,address1 ,address2 ,address3 ,country ,city ,state ,stateName ,zip ,phone ,fax ,lat ,lon ,inventory ,manager ,responseCode ,responseMessage ,IsHub, LastUpdate) "
                + "VALUES (@TriNumber, @id ,@dc ,@region ,@storeName ,@address1 ,@address2 ,@address3 ,@country ,@city ,@state ,@stateName ,@zip ,@phone ,@fax ,@lat ,@lon ,@inventory ,@manager ,@responseCode ,@responseMessage ,@IsHub, @LastUpdate)";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddVarcharPara("id", 4000, id);
            oDm.AddVarcharPara("dc", 4000, dc);
            oDm.AddVarcharPara("region", 4000, region);
            oDm.AddVarcharPara("storeName", 4000, storeName);
            oDm.AddVarcharPara("address1", 4000, address1);
            oDm.AddVarcharPara("address2", 4000, address2);
            oDm.AddVarcharPara("address3", 4000, address3);
            oDm.AddVarcharPara("country", 4000, country);
            oDm.AddVarcharPara("city", 4000, city);
            oDm.AddVarcharPara("state", 4000, state);
            oDm.AddVarcharPara("stateName", 4000, stateName);
            oDm.AddVarcharPara("zip", 4000, zip);
            oDm.AddVarcharPara("phone", 4000, phone);
            oDm.AddVarcharPara("fax", 4000, fax);
            oDm.AddFloatPara("lat", lat);
            oDm.AddFloatPara("lon", lon);
            oDm.AddVarcharPara("inventory", 4000, inventory);
            oDm.AddVarcharPara("manager", 4000, manager);
            oDm.AddVarcharPara("responseCode", 4000, responseCode);
            oDm.AddVarcharPara("responseMessage", 4000, responseMessage);
            oDm.AddBoolPara("IsHub", IsHub);
            oDm.AddDateTimePara("LastUpdate", DateTime.Now);
            return oDm.RunActionQuery();
        }

        public int Update(String TriNumber, String id, String dc, String region, String storeName, String address1, String address2, String address3, 
            String country, String city, String state, String stateName, String zip, String phone, String fax, float? lat, float? lon, String inventory, 
            String manager, String responseCode, String responseMessage, Boolean IsHub)
        {
            String Query = "UPDATE TriInventory  SET dc = @dc ,region = @region ,storeName = @storeName ,address1 = @address1 ,address2 = @address2 ,address3 = @address3 "
                + ",country = @country ,city = @city ,state = @state ,stateName = @stateName ,zip = @zip ,phone = @phone ,fax = @fax ,lat = @lat ,lon = @lon "
                + ",inventory = @inventory ,manager = @manager ,responseCode = @responseCode ,responseMessage = @responseMessage ,IsHub = @IsHub "
                + ",LastUpdate = @LastUpdate WHERE TriNumber = @TriNumber AND id = @id";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            oDm.AddVarcharPara("id", 4000, id);
            oDm.AddVarcharPara("dc", 4000, dc);
            oDm.AddVarcharPara("region", 4000, region);
            oDm.AddVarcharPara("storeName", 4000, storeName);
            oDm.AddVarcharPara("address1", 4000, address1);
            oDm.AddVarcharPara("address2", 4000, address2);
            oDm.AddVarcharPara("address3", 4000, address3);
            oDm.AddVarcharPara("country", 4000, country);
            oDm.AddVarcharPara("city", 4000, city);
            oDm.AddVarcharPara("state", 4000, state);
            oDm.AddVarcharPara("stateName", 4000, stateName);
            oDm.AddVarcharPara("zip", 4000, zip);
            oDm.AddVarcharPara("phone", 4000, phone);
            oDm.AddVarcharPara("fax", 4000, fax);
            oDm.AddFloatPara("lat", lat);
            oDm.AddFloatPara("lon", lon);
            oDm.AddVarcharPara("inventory", 4000, inventory);
            oDm.AddVarcharPara("manager", 4000, manager);
            oDm.AddVarcharPara("responseCode", 4000, responseCode);
            oDm.AddVarcharPara("responseMessage", 4000, responseMessage);
            oDm.AddBoolPara("IsHub", IsHub);
            oDm.AddDateTimePara("LastUpdate", DateTime.Now);
            return oDm.RunActionQuery();
        }

        public int DeleteByPart(String TriNumber)
        {
            String Query = "DELETE FROM TriInventory WHERE TriNumber = @TriNumber";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("TriNumber", 4000, TriNumber);
            return oDm.RunActionQuery();
        }

        public int DeleteAll()
        {
            String Query = "DELETE FROM TriInventory";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
    }
}
