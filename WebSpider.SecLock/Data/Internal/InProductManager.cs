using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.SecLock.Objects.Internal;

namespace WebSpider.SecLock.Data.Internal
{
    public class InProductManager : DataManager
    {
        #region [Constructror]
        public InProductManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<InProduct> GetData()
        {
            String Query = "SELECT * FROM SecLockProduct WITH (NOLOCK) ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InProduct>(oDm.GetTable());
        }
        public List<InProduct> GetData(InProduct product)
        {
            String Query = "SELECT * FROM SecLockProduct WITH (NOLOCK) WHERE Code = '" + product.Code+ "' ORDER BY Name";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<InProduct>(oDm.GetTable());
        }
        #endregion

        #region [Count]
        public int Count()
        {
            String Query = "SELECT COUNT(1) FROM SecLockProduct WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion

        #region [ Save ]
        public int Save(InProduct category)
        {
            var x = GetData(category);
            if (x.Count > 0)
                return Update(category);
            else
                return Insert(category);
        }
        #endregion


        #region [ Insert ]
        private int Insert(InProduct product)
        {
            String Query = "INSERT INTO [SecLockProduct] ([Code] ,[Name] ,[Url] ,[ManufacturerCode],[ManufacturerName],[ManufacturerSeries],[CategoyCode],[CategoryName], [YourPrice] ,[ListPrice] ,[ImageUrl1],[ImageUrl2] ,[Stock] ,[Description] ,[TechDoc]) "
                + "VALUES ('" + product.Code + "' ,'" + product.Name + "' ,'" + product.Url + "' ,'"
                + product.ManufacturerCode +"', '"+ product.ManufacturerName +"', '"+ product.ManufacturerSeries +"',  '" 
                + product.CategoyCode +"', '"+ product.CategoryName +"', '"
                + product.YourPrice + "' ,'" + product.ListPrice + "' ,'" + product.ImageUrl1 + "' ,'" + product.ImageUrl2 + "', '"
                + product.Stock + "' ,'" + (String.IsNullOrEmpty(product.Description) ? String.Empty : product.Description.Replace("\'", "\'\'"))
                + "' ,'" + (String.IsNullOrEmpty(product.TechDoc) ? String.Empty : product.TechDoc.Replace("'", "''")) + "')";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Update ]
        private int  Update(InProduct product)
        {
            String Query = "UPDATE [SecLockProduct] SET [Name] = '" + product.Name
                + "', [Url] = '" + product.Url
                + "', [ManufacturerCode] = '" + product.ManufacturerCode
                + "', [ManufacturerName] = '" + product.ManufacturerName
                + "', [ManufacturerSeries] = '" + product.ManufacturerSeries
                + "', [CategoyCode] = '" + product.CategoyCode
                + "', [CategoryName] = '" + product.CategoryName
                + "', [YourPrice] = " + product.YourPrice 
                + " , [ListPrice] = " + product.ListPrice 
                + " , [ImageUrl1] = '" + product.ImageUrl1
                + "', [ImageUrl2] = '" + product.ImageUrl2
                + "', [Stock] = '" + product.Stock 
                + "', [Description] = '" + (String.IsNullOrEmpty(product.Description) ? String.Empty : product.Description.Replace("\'", "\'\'"))
                + "', [TechDoc] = '" + (String.IsNullOrEmpty(product.TechDoc) ? String.Empty : product.TechDoc.Replace("'", "''"))
                + "' WHERE [Code] = '" + product.Code + "';";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion
    }
}
