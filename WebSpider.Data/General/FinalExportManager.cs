using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.General;

namespace WebSpider.Data.General
{
    public class FinalExportManager : DataManager
    {
        #region [ Constructor ]
        public FinalExportManager(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<FinalExport> GetData()
        {
            String Query = "SELECT * FROM FinalExport ORDER BY CreatedDate";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            return DataParser.ToList<FinalExport>(oDm.GetTable());
        }

        public FinalExport GetTop()
        {
            String Query = "SELECT TOP 1 * FROM FinalExport ORDER BY CreatedDate";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            List<FinalExport> finalExports = DataParser.ToList<FinalExport>(oDm.GetTable());
            return finalExports.Count == 0 ? null : finalExports[0];
        }

        public FinalExport GetTopBySite(String ExportSite)
        {
            String Query = "SELECT TOP 1 * FROM FinalExport WHERE ExportSite = @ExportSite ORDER BY CreatedDate";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("ExportSite", 4000, ExportSite);
            List<FinalExport> finalExports = DataParser.ToList<FinalExport>(oDm.GetTable());
            return finalExports.Count == 0 ? null : finalExports[0];
        }

        public List<FinalExport> GetBySite(String ExportSite)
        {
            String Query = "SELECT * FROM FinalExport WHERE ExportSite = @ExportSite ORDER BY CreatedDate";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("ExportSite", 4000, ExportSite);
            return DataParser.ToList<FinalExport>(oDm.GetTable());
        }
        #endregion

        #region [Insert]
        public void Insert(FinalExport fe)
        {
            Insert(fe.ExportSite, fe.ExportType, fe.ExportValue);
        }

        public int Insert(String ExportSite, String ExportType, String ExportValue)
        {
            String Query = "INSERT INTO FinalExport (ExportSite, ExportType, ExportValue, CreatedDate) "
                + "VALUES (@ExportSite, @ExportType, @ExportValue, @CreatedDate)";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("ExportSite", 4000, ExportSite);
            oDm.AddVarcharPara("ExportType", 4000, ExportType);
            oDm.AddVarcharPara("ExportValue", 4000, ExportValue);
            oDm.AddDateTimePara("CreatedDate", DateTime.Now);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [Delete]
        public int Delete(String ExportSite, String ExportType, String ExportValue, DateTime CreatedDate)
        {
            String Query = "DELETE FROM FinalExport WHERE "
                + "ExportSite = @ExportSite AND ExportType = @ExportType AND ExportValue = @ExportValue AND CreatedDate = @CreatedDate";
            SqlCeDataManager oDm = new SqlCeDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("ExportSite", 4000, ExportSite);
            oDm.AddVarcharPara("ExportType", 4000, ExportType);
            oDm.AddVarcharPara("ExportValue", 4000, ExportValue);
            oDm.AddDateTimePara("CreatedDate", CreatedDate);
            return oDm.RunActionQuery();
        }
        #endregion



        
    }
}
