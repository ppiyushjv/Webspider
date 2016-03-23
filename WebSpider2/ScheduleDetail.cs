using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Data.General;
using WebSpider.Objects;
using WebSpider.Objects.General;

namespace WebSpider
{
    public partial class ScheduleDetail : Form
    {
        TaskHeader taskHeader;
        
        public ScheduleDetail()
        {
            InitializeComponent();
            dtScheduleDate.Value = DateTime.Now;
        }

        public ScheduleDetail(Int64 ScheduleID)
        {
            InitializeComponent();
            taskHeader = new TaskHeader();
            taskHeader.ScheduleID = ScheduleID;
        }

        #region [ Save ]
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (taskHeader == null)
                taskHeader = new TaskHeader();

            taskHeader.TaskName = txtTaskName.Text.Trim();
            taskHeader.ScheduleFrom = dtScheduleDate.Value;
            taskHeader.Site = Constants.SiteName.ADIGLOBAL;
            taskHeader.TaskRepeat = chkRepeat.Checked;
            taskHeader.TaskRepeatInterval = Convert.ToInt32(numRepeatCount.Value);
            taskHeader.TaskRepeatUnit = cmbRepeatType.Text;
            taskHeader.Enabled = chkEnabled.Checked;
            taskHeader.TaskDescription = txtDescription.Text.Trim();

            try
            {
                TaskHeaderManager mgr = new TaskHeaderManager(Constants.ConnectionString);
                var x = mgr.Save(taskHeader);
            }
            catch (Exception ex) { }
        }
        #endregion

        #region [ Cancel ]
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void chkRepeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkRepeat = sender as CheckBox;
            numRepeatCount.Enabled = chkRepeat.Checked;
            cmbRepeatType.Enabled = chkRepeat.Checked;
        }

        private void ScheduleDetail_Load(object sender, EventArgs e)
        {
            if (taskHeader == null) return;

            try
            {
                TaskHeaderManager mgr = new TaskHeaderManager(Constants.ConnectionString);
                List<TaskHeader> headers = mgr.GetData(taskHeader.ScheduleID);
                if (headers.Count == 1)
                {
                    txtTaskName.Text = headers[0].TaskName;
                    dtScheduleDate.Value = headers[0].ScheduleFrom;
                    //taskHeader.Site = Constants.SiteName.ADIGLOBAL;
                    chkRepeat.Checked = headers[0].TaskRepeat;
                    numRepeatCount.Value = headers[0].TaskRepeatInterval;
                    cmbRepeatType.Text = headers[0].TaskRepeatUnit;
                    chkEnabled.Checked = headers[0].Enabled;
                    txtDescription.Text = headers[0].TaskDescription.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
