using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Data.UpdateSpider1TableAdapters;
using System.Data.OleDb;
using WebSpider.Core;
using WebSpider.AdiGlobal.Data.AdiGlobal;
using WebSpider.Objects;
using WebSpider.AdiGlobal.Objects.AdiGlobal;

namespace WebSpider
{
    public partial class ProductPriority : Form
    {
        DataTable dt = new DataTable();
        DataView dv=new DataView();
        public ProductPriority()
        {
            InitializeComponent();
        }

        private void ProductPriority_Load(object sender, EventArgs e)
        {
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);

            List<AdiProduct> adiProducts = productManager.GetData();
            gridProduct.AutoGenerateColumns = false;
            gridProduct.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dv.Table = WebSpider.Data.DatabaseManager.DataParser.BuildDataTable(adiProducts);
            gridProduct.DataSource = adiProducts;
            
        }


        private void gridProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell leastCount = null;
                DataGridViewCell priority = null;
                DataGridViewCell partNo = null;
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    leastCount = ((DataGridView)sender).Rows[e.RowIndex].Cells[3];
                    priority = ((DataGridView)sender).Rows[e.RowIndex].Cells[4];
                    partNo = ((DataGridView)sender).Rows[e.RowIndex].Cells[2];
                    ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
                    productManager.SetProductPriority(partNo.Value.ToString(), (bool)priority.Value, (int)leastCount.Value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Invalid input.");
        }

        private void txtpart_TextChanged(object sender, EventArgs e)
        {
            dv.RowFilter = "AdiNumber like '%" + txtpart.Text + "%'";
            gridProduct.DataSource = dv;   
        }


        
    }
}
