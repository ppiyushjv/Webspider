using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Objects.AdiGlobal;
using WebSpider.Data.DatabaseConnection;
using System.Data;
using System.Reflection;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.Data.AdiGlobal
{
    public class ADICategoryExportManager : DataManager
    {
        private string ConnectionString;

        #region [ Constructor ]
        public ADICategoryExportManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion
        
        public List<ADICategoryExport> GetData()
        {
            String Query = "SELECT * FROM ADICategoryExport WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<ADICategoryExport>(oDm.GetTable());
        }

        //public int Insert(String ParentValue, String Value, String DisplayName, String CategoryUrl)
        //{

        //    String Query = "INSERT INTO ADICategory (ParentValue, Value, DisplayName, CategoryUrl) VALUES (@ParentValue, @Value, @DisplayName, @CategoryUrl)";
        //    SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
        //    oDm.AddVarcharPara("ParentValue", 255, ParentValue);
        //    oDm.AddVarcharPara("Value", 255, Value);
        //    oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
        //    oDm.AddVarcharPara("CategoryUrl", 4000, CategoryUrl);
        //    return oDm.RunActionQuery();
        //}

        public int CatagoryCount()
        {
            String Query = "SELECT COUNT(1) FROM ADICategoryExport WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }

        public void ClearCatagory()
        {
            String Query = "DELETE FROM ADICategoryExport WITH (ROWLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }

        public List<ADICategoryExport> GetDataByCatagoryID(String Value)
        {
            String Query = "SELECT * FROM ADICategoryExport WITH (NOLOCK) WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 255, Value);
            return DataParser.ToList<ADICategoryExport>(oDm.GetTable());
        }

        public void GenerateExport()
        {
            String Query = "INSERT INTO [ADICategoryExport] ([RootValue] ,[RootDisplayName] ,[ParentValue] ,[ParentDisplayName] ,[Value] ,[DisplayName] ,[CategoryUrl]) "
                + "SELECT ROOT.[Value] ,ROOT.[DisplayName] ,PARENT.[Value] ,PARENT.[DisplayName] ,CHILD.[Value] ,CHILD.[DisplayName] ,CHILD.[CategoryUrl] "
                + "FROM [ADICategory] ROOT WITH (NOLOCK) JOIN [ADICategory] PARENT  WITH (NOLOCK) ON ROOT.[Value] = PARENT.[ParentValue] "
                + "JOIN [ADICategory] CHILD WITH (NOLOCK) ON PARENT.[Value] = CHILD.[ParentValue] "
                + "WHERE ROOT.[ParentValue] IS NULL";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }
    }
}
