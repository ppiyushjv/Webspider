using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.SecLock.Objects.External;

namespace WebSpider.SecLock.Data.External
{
    public class FinalTableManager : DataManager
    {
        #region [Constructror]
        public FinalTableManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<FinalTable> GetData()
        {
            String Query = "SELECT * FROM Final_Table WHERE SLD_PART IS NOT NULL AND SLD_PART <> ''";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        public List<FinalTable> GetData(String SLD_PART)
        {
            String Query = "SELECT * FROM Final_Table WHERE SLD_PART = \"" + SLD_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            return DataParser.ToList<FinalTable>(oDm.GetTable());
        }

        #endregion

        #region [ Count ]
        public int Count()
        {
            String Query = "SELECT * FROM Final_Table WHERE SLD_PART IS NOT NULL";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return (int)oDm.GetTable().Rows[0][0];
        }
        #endregion

        #region [ Save ]
        public int SaveProduct(FinalTable finalTable)
        {
            if (GetData(finalTable.SLD_PART).Count() == 0)
                return Insert(finalTable);
            else
                return Update(finalTable);
        }
        #endregion

        #region [ Update ]
        private int Update(FinalTable finalTable)
        {

            String Query = "UPDATE [Final_Table] SET [SLD_SOURCE_ID] = '" + finalTable.SLD_SOURCE_ID
                + "', [SLD_COST] = "+ finalTable.SLD_COST 
                + ", [SLD_IMG1] = '"+ finalTable.SLD_IMG1 
                + "', [SLD_IMG2] = '"+finalTable.SLD_IMG2
                + "', [SLD_VENDOR] = '"+finalTable.SLD_VENDOR
                + "', [SLD_INV] = '"+finalTable.SLD_INV
                + "', [SLD_LastUpdate] = '"+ finalTable.SLD_LastUpdate
                + "', [SLD_DESC] = '" + finalTable.SLD_DESC
                + "', [SLD_TECHDOC] = '" + finalTable.SLD_TECHDOC
                + "' WHERE [SLD_PART] = '" + finalTable.SLD_PART 
                + "'";
            OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
            return oDm.RunActionQuery();
        }
        #endregion

        #region [ Insert ]
        private int Insert(FinalTable finalTable)
        {
            try
            {
                String Query = "INSERT INTO [Final_Table] ([SLD_SOURCE_ID], [SLD_COST], [SLD_PART], [SLD_IMG1], [SLD_IMG2], [SLD_VENDOR], [SLD_INV], [SLD_DESC], [SLD_TECHDOC], [SLD_LastUpdate])"
                    + "VALUES ('" + finalTable.SLD_SOURCE_ID + "'," + finalTable.SLD_COST + " , '" + finalTable.SLD_PART + "', '"
                    + finalTable.SLD_IMG1 + "', '" + finalTable.SLD_IMG2 + "', '" + finalTable.SLD_VENDOR + "', '"
                    + finalTable.SLD_INV + "', '" + finalTable.SLD_DESC + "', '" + finalTable.SLD_TECHDOC + "', '" + finalTable.SLD_LastUpdate + "')";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                return oDm.RunActionQuery();
            }
            catch
            {
                String Query = "INSERT INTO [Final_Table] ([SLD_SOURCE_ID], [SLD_COST], [SLD_PART], [SLD_IMG1], [SLD_IMG2], [SLD_VENDOR], [SLD_INV], [SLD_LastUpdate])"
                    + " VALUES ('" + finalTable.SLD_SOURCE_ID + "'," + finalTable.SLD_COST + " , '" + finalTable.SLD_PART + "', '"
                    + finalTable.SLD_IMG1 + "', '" + finalTable.SLD_IMG2 + "', '" + finalTable.SLD_VENDOR + "', '"
                    + finalTable.SLD_INV + "', '" + finalTable.SLD_LastUpdate + "')";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                return oDm.RunActionQuery();
            }
        }
        #endregion       
    }
}