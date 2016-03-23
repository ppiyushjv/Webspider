using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseConnection;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects;

namespace WebSpider.Data
{
    public class Test
    {
        public static void test()
        {
            //EntityConnectionStringBuilder stringBuilder = new EntityConnectionStringBuilder();
            //stringBuilder.Provider = "System.Data.SqlServerCe.4.0";
            //stringBuilder.Metadata = "res://*/WebSpiderDb.csdl|res://*/WebSpiderDb.ssdl|res://*/WebSpiderDb.msl";
            //stringBuilder.ProviderConnectionString = "data source=|DataDirectory|\\App_Code\\WebSpiderDB.sdf";

            //WebSpiderDBEntities x = new WebSpiderDBEntities(stringBuilder.ConnectionString);

            //List<ADICatagory> list = x.ADICatagories.ToList();

            //SqlCeDataConnection.sConnStr = "Data Source=E:\\GitHub\\TeamAG\\WebSpider\\WebSpider.Data\\App_Code\\WebSpiderDB.sdf;Persist Security Info=False;";

            //List<ADIBrand> list = new ADIBrandManager().GetData();
        }

    }
}
