using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.TriGlobal.Objects.TriGlobal;

namespace WebSpider.TriGlobal.Data.TriGlobal
{
    public class TriProductSpecificationManager : DataManager
    {
        private string ConnectionString;

        public TriProductSpecificationManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        public List<TriChild> GetData()
        {
            String Query = "SELECT * FROM TriChild WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<TriChild>(oDm.GetTable());
        }

        public int Insert(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "INSERT INTO [TriChild] WITH (ROWLOCK) ([PART_NUM],[PropertyName],[PropertyValue])VALUES(@PART_NUM, @PropertyName, @PropertyValue)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        public List<TriChild> GetDataByPartNumber(String PART_NUM)
        {
            String Query = "SELECT * FROM TriChild WITH (NOLOCK) WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return DataParser.ToList<TriChild>(oDm.GetTable());
        }
        public DataTable GetDataTableByPartNumber(String PART_NUM)
        {
            String Query = "SELECT * FROM TriChild WITH (NOLOCK) WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return oDm.GetTable();
        }

        public String GetValue(String PART_NUM, String PropertyName)
        {
            String Query = "SELECT ID, PART_NUM, PropertyName, PropertyValue FROM TriChild WITH (NOLOCK) WHERE PART_NUM = @PART_NUM AND PropertyName = @PropertyName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            List<TriChild> childs = DataParser.ToList<TriChild>(oDm.GetTable());
            if (childs.Count > 0)
                return childs[0].PropertyValue;
            else
                return null;
        }

        public int UpdateValue(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "UPDATE TriChild WITH (ROWLOCK) SET PropertyValue = @PropertyValue WHERE PART_NUM = @PART_NUM AND PropertyName = @PropertyName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        public int Delete(string PART_NUM)
        {
            String Query = "DELETE FROM TriChild WHERE PART_NUM = \"" + PART_NUM + "\"";
            OleDbDataManager oDm = new OleDbDataManager(ConnectionString, Query, true);
            //oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return oDm.RunActionQuery();
        }

        public int Save(String PART_NUM, String PropertyName, String PropertyValue)
        {
            if (GetValue(PART_NUM, PropertyName) == null)
                return Insert(PART_NUM, PropertyName, PropertyValue);
            else
                return UpdateValue(PART_NUM, PropertyName, PropertyValue);
        }
    }
}
