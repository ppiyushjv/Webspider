using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.AdiExport;

namespace WebSpider.Data.AdiExport
{
    public class AdiInventoryDetailsManager :DataManager
    {
        #region [Constructror]
        public AdiInventoryDetailsManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<ADI_InventoryDetails> GetData()
        {
            String Query = "SELECT * FROM AdiInventoryDetails";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<ADI_InventoryDetails>(oDm.GetTable());
        }

        public List<ADI_InventoryDetails> GetData(String AdiNumber)
        {
            String Query = "SELECT * FROM AdiInventoryDetails WHERE AdiNumber = \"" + AdiNumber + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AdiNumber", 4000, AdiNumber);
            return DataParser.ToList<ADI_InventoryDetails>(oDm.GetTable());
        }

        public List<ADI_InventoryDetails> GetData(String AdiNumber, String id, String dc, String region, String storeName)
        {
            String Query = "SELECT * FROM AdiInventoryDetails WHERE AdiNumber = \"" + AdiNumber
                + "\" and id = \"" + id + "\" and dc = \"" + dc + "\" and region = \"" + region + "\" and storeName = \"" + storeName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AdiNumber", 4000, AdiNumber);
            //oDm.AddVarcharPara("id", 4000, id);
            //oDm.AddVarcharPara("dc", 4000, dc);
            //oDm.AddVarcharPara("region", 4000, region);
            //oDm.AddVarcharPara("storename", 4000, storeName);
            return DataParser.ToList<ADI_InventoryDetails>(oDm.GetTable());
        }
        #endregion

        #region [ Save ]
        public int Save(String AdiNumber, String id, String dc, String region, String storeName, String address1, String address2, String address3
            , String country, String city, String state, String stateName, String zip, String phone, String fax, float? lat, float? lon, String inventory
            , String manager, String responseCode, String responseMessage, Boolean IsHub, DateTime LastUpdate)
        {
            if (GetData(AdiNumber, id, dc, region, storeName).Count == 0)
                return Insert(AdiNumber, id, dc, region, storeName, address1, address2, address3, country, city, state, stateName, zip, phone, fax, lat, lon, inventory, manager, responseCode, responseMessage, IsHub, LastUpdate);
            else
                return Update(AdiNumber, id, dc, region, storeName, address1, address2, address3, country, city, state, stateName, zip, phone, fax, lat, lon, inventory, manager, responseCode, responseMessage, IsHub, LastUpdate);
        }
        #endregion

        #region [ Insert ]
        public int Insert(String AdiNumber, String id, String dc, String region, String storeName, String address1, String address2, String address3
            , String country, String city, String state, String stateName, String zip, String phone, String fax, float? lat, float? lon, String inventory
            , String manager, String responseCode, String responseMessage, Boolean IsHub, DateTime LastUpdate)
        {
            String Query = "INSERT INTO AdiInventoryDetails (AdiNumber, id ,dc ,region ,storeName ,address1 ,address2 ,address3 ,country ,city ,state ,stateName ,zip ,phone ,fax ,lat ,lon ,inventory ,manager ,responseCode ,responseMessage ,IsHub, LastUpdate) "
                + "VALUES (@AdiNumber, @id ,@dc ,@region ,@storeName ,@address1 ,@address2 ,@address3 ,@country ,@city ,@state ,@stateName ,@zip ,@phone ,@fax ,@lat ,@lon ,@inventory ,@manager ,@responseCode ,@responseMessage ,@IsHub, @LastUpdate)";

            //String Query = "INSERT INTO AdiInventoryDetails (AdiNumber, id ,dc ,region ,storeName ,address1 ,address2 ,address3 ,country ,city ,state ,stateName ,zip ,phone ,fax,inventory ,manager ,responseCode ,responseMessage ,IsHub, LastUpdate) "
            //   + "VALUES (@AdiNumber, @id ,@dc ,@region ,@storeName ,@address1 ,@address2 ,@address3 ,@country ,@city ,@state ,@stateName ,@zip ,@phone ,@fax ,@inventory ,@manager ,@responseCode ,@responseMessage ,@IsHub, @LastUpdate)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("AdiNumber", 4000, AdiNumber);
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
            oDm.AddDateTimePara("LastUpdate", LastUpdate);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        public int Update(String AdiNumber, String id, String dc, String region, String storeName, String address1, String address2, String address3,
            String country, String city, String state, String stateName, String zip, String phone, String fax, float? lat, float? lon, String inventory,
            String manager, String responseCode, String responseMessage, Boolean IsHub, DateTime LastUpdate)
        {
            String Query = "UPDATE AdiInventoryDetails  SET dc = @dc ,region = @region ,storeName = @storeName ,address1 = @address1 ,address2 = @address2 ,address3 = @address3 "
                + ",country = @country ,city = @city ,state = @state ,stateName = @stateName ,zip = @zip ,phone = @phone ,fax = @fax ,lat = @lat ,lon = @lon "
                + ",inventory = @inventory ,manager = @manager ,responseCode = @responseCode ,responseMessage = @responseMessage ,IsHub = @IsHub "
                + ",LastUpdate = @LastUpdate WHERE AdiNumber = @AdiNumber AND id = \"" + id + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("AdiNumber", 4000, AdiNumber);
            //oDm.AddVarcharPara("id", 4000, id);
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
            oDm.AddDateTimePara("LastUpdate", LastUpdate);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Delete ]
        public int Delete(String AdiNumber)
        {
            String Query = "DELETE FROM AdiInventoryDetails WHERE AdiNumber = @AdiNumber";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("AdiNumber", 4000, AdiNumber);
            return oDm.RunActionQuery();
        }

        public int DeleteAll()
        {
            String Query = "DELETE FROM AdiInventoryDetails";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion
    }
}
