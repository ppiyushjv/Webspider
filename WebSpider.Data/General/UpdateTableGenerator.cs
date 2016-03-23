using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSpider.Data.DatabaseManager;

namespace WebSpider.Data.General
{
    public class UpdateSpiderTableGenerator : DataManager
    {
        #region [ Constructor ]
        public UpdateSpiderTableGenerator(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion
        #region [ Adi Final Table Update ]
        public String GenerateFinalTableUpdate()
        {
            try
            {
                String Query = String.Empty;
                OleDbDataManager oDm;

                Query = "CREATE TABLE Final_Table ("
                    + "ID  AUTOINCREMENT PRIMARY KEY, "
                    + "UPC TEXT(255), "
                    + "VDR_PART TEXT(255), "
                    + "VDR_IT_DSC TEXT(255), "
                    + "Image_Folder TEXT(255), "
                    + "AID_SOURCE_ID TEXT(255), "
                    + "AID_PART TEXT(255), "
                    + "AID_COST CURRENCY, "
                    + "AID_IMG1 TEXT(255), "
                    + "AID_IMG2 TEXT(255), "
                    + "AID_VENDOR TEXT(255), "
                    + "AID_INV TEXT(255), "
                    + "AID_LastUpdate TEXT(255), "
                    + "SLD_SOURCE_ID TEXT(255), "
                    + "SLD_COST CURRENCY, "
                    + "SLD_PART TEXT(255), "
                    + "SLD_IMG1 TEXT(255), "
                    + "SLD_IMG2 TEXT(255), "
                    + "SLD_VENDOR TEXT(255), "
                    + "SLD_INV TEXT(255), "
                    + "SLD_DESC MEMO, "
                    + "SLD_TECHDOC MEMO, "
                    + "SLD_LastUpdate TEXT(255)) ";
                oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated Final_Table Structure";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region [ Adi Child ]
        public String GenerateAdiChild()
        {
            try
            {
                String Query = "CREATE TABLE ADIChild ("
                    + "ID  AUTOINCREMENT PRIMARY KEY, "
                    + "PART_NUM TEXT(255), "
                    + "PropertyName TEXT(255), "
                    + "PropertyValue MEMO)";
                OleDbDataManager oDm = new OleDbDataManager(this.ConnectionString, Query, true);
                oDm.RunActionQuery();
                return "Generated \'ADIChild\' Structure";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion


        #region [ Generate ]
        public static String Generate(String FileName)
        {
            List<String> Messages = new List<String>();
            try
            {
                String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", FileName);
                UpdateSpiderTableGenerator gen = new UpdateSpiderTableGenerator(ConnStr);
                Messages.Add(String.Format("Using File \'{0}\'", FileName));
                Messages.Add(gen.GenerateFinalTableUpdate());
                Messages.Add(gen.GenerateAdiChild());
            }
            catch (Exception ex)
            {
                Messages.Add(ex.ToString());
            }
            return String.Join("\n", Messages);
        }
        #endregion
        
    }
}
