using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Data;

namespace WebSpider.TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //WebSpiderDBEntities x = new WebSpiderDBEntities(new SqlCeConnectionBuilder("|DataDirectory|\\App_Code\\WebSpiderDB.sdf").ToString());

            //List<ADICatagory> list = x.ADICatagories.ToList();

            //var z = x.ADICatagories.Where(m => m.ParentCategoryID != null);
            //list = z.ToList();

            //x.ADICatagories.Remove(list[0]);

            ////MessageBox.Show("Hello!");

            Test.test();
        }
    }
}
