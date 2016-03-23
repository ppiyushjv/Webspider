using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.TriGlobal.Objects.TriGlobal;
using WebSpider.Data.DatabaseConnection;
using System.Data;
using System.Reflection;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.TriGlobal.Data.TriGlobal
{
    public class TriCategoryExportManager : DataManager
    {
        private string ConnectionString;

        #region [ Constructor ]
        public TriCategoryExportManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion
        
        public List<TriCategoryExport> GetData()
        {
            String Query = "SELECT * FROM TriCategoryExport WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<TriCategoryExport>(oDm.GetTable());
        }

        //public int Insert(String ParentValue, String Value, String DisplayName, String CategoryUrl)
        //{

        //    String Query = "INSERT INTO TriCategory (ParentValue, Value, DisplayName, CategoryUrl) VALUES (@ParentValue, @Value, @DisplayName, @CategoryUrl)";
        //    SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
        //    oDm.AddVarcharPara("ParentValue", 255, ParentValue);
        //    oDm.AddVarcharPara("Value", 255, Value);
        //    oDm.AddVarcharPara("DisplayName", 4000, DisplayName);
        //    oDm.AddVarcharPara("CategoryUrl", 4000, CategoryUrl);
        //    return oDm.RunActionQuery();
        //}

        public int CatagoryCount()
        {
            String Query = "SELECT COUNT(1) FROM TriCategoryExport WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }

        public void ClearCatagory()
        {
            String Query = "DELETE FROM TriCategoryExport WITH (ROWLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }

        public List<TriCategoryExport> GetDataByCatagoryID(String Value)
        {
            String Query = "SELECT * FROM TriCategoryExport WITH (NOLOCK) WHERE Value = @Value";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("Value", 255, Value);
            return DataParser.ToList<TriCategoryExport>(oDm.GetTable());
        }

        public void GenerateExport()
        {
            String Query = "INSERT INTO [TriCategoryExport] ([RootValue] ,[RootDisplayName] ,[ParentValue] ,[ParentDisplayName] ,[Value] ,[DisplayName] ,[CategoryUrl]) "
                + "SELECT ROOT.[Value] ,ROOT.[DisplayName] ,PARENT.[Value] ,PARENT.[DisplayName] ,CHILD.[Value] ,CHILD.[DisplayName] ,CHILD.[CategoryUrl] "
                + "FROM [TriCategory] ROOT WITH (NOLOCK) JOIN [TriCategory] PARENT  WITH (NOLOCK) ON ROOT.[Value] = PARENT.[ParentValue] "
                + "JOIN [TriCategory] CHILD WITH (NOLOCK) ON PARENT.[Value] = CHILD.[ParentValue] "
                + "WHERE ROOT.[ParentValue] IS NULL";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }
    }
}
