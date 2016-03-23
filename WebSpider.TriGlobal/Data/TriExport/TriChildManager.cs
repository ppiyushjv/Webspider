using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseConnection;
using WebSpider.Data.DatabaseManager;
using WebSpider.TriGlobal.Objects.TriExport;

namespace WebSpider.TriGlobal.Data.TriExport
{
    public class TriChildManager : DataManager
    {
        #region [Constructror]
        public TriChildManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<Tri_Child> GetData()
        {
            String Query = "SELECT * FROM TriChild";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<Tri_Child>(oDm.GetTable());
        }

        public List<Tri_Child> GetData(String AID_PART)
        {
            String Query = "SELECT * FROM TriChild WHERE PART_NUM = \"" + AID_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            return DataParser.ToList<Tri_Child>(oDm.GetTable());
        }

        public List<Tri_Child> GetData(String AID_PART, String PropertyName)
        {
            String Query = "SELECT * FROM TriChild WHERE PART_NUM = \"" + AID_PART + "\" AND PropertyName = \"" + @PropertyName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            //oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            return DataParser.ToList<Tri_Child>(oDm.GetTable());
        }

        #endregion

        #region [ Save ]
        public int Save(Tri_Child TriChild)
        {
            return SaveTriPart(TriChild.PART_NUM, TriChild.PropertyName, TriChild.PropertyValue);
        }

        private int SaveTriPart(String PART_NUM, String PropertyName, String PropertyValue)
        {
            var count = GetData(PART_NUM, PropertyName).Count();
            if (count == 0)
                return Insert(PART_NUM, PropertyName, PropertyValue);
            else
                return Update(PART_NUM, PropertyName, PropertyValue);
        }

        
        #endregion

        #region [ Insert / Update ]

        public int Insert(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "INSERT INTO TriChild (PART_NUM, PropertyName, PropertyValue)"
                + " VALUES (@PART_NUM, @PropertyName, @PropertyValue)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        public int Update(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "UPDATE TriChild SET PropertyValue = @PropertyValue"
                + " WHERE PART_NUM = \"" + PART_NUM  + "\" AND PropertyName = \"" + @PropertyName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            //oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        #endregion

        #region [ Delete ]
        public int Delete(String PART_NUM)
        {
            String Query = "DELETE FROM TriChild WHERE PART_NUM = \"" + PART_NUM + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return oDm.RunActionQuery();
        }
        public int Delete(String PART_NUM, String PropertyName)
        {
            String Query = "DELETE FROM TriChild WHERE PART_NUM = \"" + PART_NUM + "\" AND PropertyName = \"" + PropertyName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            //oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            return oDm.RunActionQuery();
        }
        #endregion
        
    }
}
