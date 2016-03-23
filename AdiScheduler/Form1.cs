using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebSpiderScheduler
{
    public partial class Form1 : Form
    {
        AdiScheduler scheduler = new AdiScheduler();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string k = System.DateTime.Now.DayOfWeek.ToString().Substring(0,3);
            //AdiScheduler.Process();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }
    }
}
