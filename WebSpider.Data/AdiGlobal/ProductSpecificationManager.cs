using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;
using WebSpider.Objects.AdiGlobal;

namespace WebSpider.Data.AdiGlobal
{
    public class ADIProductSpecificationManager : DataManager
    {
        private string ConnectionString;

        public ADIProductSpecificationManager(string ConnectionString)
        {
            // TODO: Complete member initialization
            this.ConnectionString = ConnectionString;
        }
        public List<ADIChild> GetData()
        {
            String Query = "SELECT * FROM ADIChild WITH (NOLOCK)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            return DataParser.ToList<ADIChild>(oDm.GetTable());
        }

        public int Insert(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "INSERT INTO [ADIChild] WITH (ROWLOCK) ([PART_NUM],[PropertyName],[PropertyValue])VALUES(@PART_NUM, @PropertyName, @PropertyValue)";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        public List<ADIChild> GetDataByPartNumber(String PART_NUM)
        {
            String Query = "SELECT * FROM ADIChild WITH (NOLOCK) WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return DataParser.ToList<ADIChild>(oDm.GetTable());
        }
        public DataTable GetDataTableByPartNumber(String PART_NUM)
        {
            String Query = "SELECT * FROM ADIChild WITH (NOLOCK) WHERE PART_NUM = @PART_NUM";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            return oDm.GetTable();
        }

        public String GetValue(String PART_NUM, String PropertyName)
        {
            String Query = "SELECT ID, PART_NUM, PropertyName, PropertyValue FROM ADIChild WITH (NOLOCK) WHERE PART_NUM = @PART_NUM AND PropertyName = @PropertyName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            List<ADIChild> childs = DataParser.ToList<ADIChild>(oDm.GetTable());
            if (childs.Count > 0)
                return childs[0].PropertyValue;
            else
                return null;
        }

        public int UpdateValue(String PART_NUM, String PropertyName, String PropertyValue)
        {
            String Query = "UPDATE ADIChild WITH (ROWLOCK) SET PropertyValue = @PropertyValue WHERE PART_NUM = @PART_NUM AND PropertyName = @PropertyName";
            SqlCeDataManager oDm = new SqlCeDataManager(this.ConnectionString, Query, true);
            oDm.AddVarcharPara("PART_NUM", 4000, PART_NUM);
            oDm.AddVarcharPara("PropertyName", 4000, PropertyName);
            oDm.AddVarcharPara("PropertyValue", 4000, PropertyValue);
            return oDm.RunActionQuery();
        }

        public int Delete(string PART_NUM)
        {
            String Query = "DELETE FROM AdiChild WHERE PART_NUM = \"" + PART_NUM + "\"";
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
