using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseConnection;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.AdiExport;

namespace WebSpider.Data.AdiExport
{
    public class AdiChildManager : DataManager
    {
        #region [Constructror]
        public AdiChildManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        #endregion

        #region [ Get Data ]
        public List<ADI_Child> GetData()
        {
            String Query = "SELECT * FROM ADIChild";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            return DataParser.ToList<ADI_Child>(oDm.GetTable());
        }

        public List<ADI_Child> GetData(String AID_PART)
        {
            String Query = "SELECT * FROM ADIChild WHERE PART_NUM = \"" + AID_PART + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            return DataParser.ToList<ADI_Child>(oDm.GetTable());
        }

        public List<ADI_Child> GetData(String AID_PART, String PropertyName)
        {
            String Query = "SELECT * FROM ADIChild WHERE PART_NUM = \"" + AID_PART + "\" AND PropertyName = \"" + @PropertyName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("AID_PART", 4000, AID_PART);
            //oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            return DataParser.ToList<ADI_Child>(oDm.GetTable());
        }

        #endregion

        #region [ Save ]
        public int Save(ADI_Child adiChild)
        {
            return SaveAdiPart(adiChild.PART_NUM, adiChild.PropertyName, adiChild.PropertyValue);
        }

        private int SaveAdiPart(String PART_NUM, String PropertyName, String PropertyValue)
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
            String Query = "INSERT INTO AdiChild (PART_NUM, PropertyName, PropertyValue)"
                + " VALUES (@PART_NUM, @PropertyName, @PropertyValue)";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        public int Update(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "UPDATE AdiChild SET PropertyValue = @PropertyValue"
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
            String Query = "DELETE FROM AdiChild WHERE PART_NUM = \"" + PART_NUM + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return oDm.RunActionQuery();
        }
        public int Delete(String PART_NUM, String PropertyName)
        {
            String Query = "DELETE FROM AdiChild WHERE PART_NUM = \"" + PART_NUM + "\" AND PropertyName = \"" + PropertyName + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            //oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            return oDm.RunActionQuery();
        }
        #endregion
        
    }
}
