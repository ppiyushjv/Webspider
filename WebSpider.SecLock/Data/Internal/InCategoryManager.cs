using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.SecLock.Objects.Internal;

namespace WebSpider.SecLock.Data.Internal
{
    public class InCategoryManager : DataManager
    {
        #region [Constructror]
        public InCategoryManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<InCategory> GetData()
        {
            String Query = "SELECT * FROM SecLockCategory WITH (NOLOCK) ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InCategory>(oDm.GetTable());
        }
        public List<InCategory> GetData(InCategory category)
        {
            String Query = "SELECT * FROM SecLockCategory WITH (NOLOCK) WHERE Code = '" + category.Code+ "' ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InCategory>(oDm.GetTable());
        }
        #endregion

        #region [Count]
        public int Count()
        {
            String Query = "SELECT COUNT(1) FROM SecLockCategory WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion

        #region [ Save ]
        public int Save(InCategory category)
        {
            var x = GetData(category);
            if (x.Count > 0)
                return Update(category);
            else
                return Insert(category);
        }
        #endregion

        #region [ Insert ]
        private int Insert(InCategory category)
        {
            String Query = "INSERT INTO [SecLockCategory]([Code],[Name]) VALUES('"
                + category.Code + "','" + category.Name + "');";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        private int  Update(InCategory category)
        {
            String Query = "UPDATE [SecLockCategory] SET [Name] = '" + category.Name
                + "' WHERE [Code] = '" + category.Code + "'";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Delete All ]
        public void DeleteAll()
        {
            String Query = "DELETE FROM [SecLockCategory]";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.RunActionQuery();
        }
        #endregion
        
    }
}
