using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Core;

namespace WebSpider
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region [General]
            if (chkCaching.Checked)
            {
                try
                {
                    Convert.ToInt32(txtCacheDuration.Text);
                }
                catch {
                    ShowValidationMessage("Cache Duration is missing");
                    return;
                }
            }

            if(chkErrorLog.Checked && txtErrorFileName.Text.Length == 0)
            {
                ShowValidationMessage("Error File Name is missing");
                return;
            }

            try
            {
                Convert.ToInt32(txtRetryCount.Text);
            }
            catch {
                ShowValidationMessage("Web Request retry count is invalid or missing.");
                return;
            }

            if (txtDateFormat.Text.Length == 0)
            {
                ShowValidationMessage("Date format is missing or invalid");
                return;
            }
            if (txtcrawlDB.Text.Length == 0)
            {
                ShowValidationMessage("Crawl Mode DB Path is blank");
                return;
            }
            if (txtUpdateDB.Text.Length == 0)
            {
                ShowValidationMessage("Update Mode DB Path is blank");
                return;
            }
            if (txtPing.Text.Length == 0)
            {
                ShowValidationMessage("Ping Rate is blank");
                return;
            }
            #endregion

            #region [Mail]
            if (chkSendErrorMail.Checked)
            {
                if (txtMailTo.Text.Length == 0)
                {
                    ShowValidationMessage("Send error mail to is missing");
                    return;
                }
                if (txtMailFrom.Text.Length == 0)
                {
                    ShowValidationMessage("Send error mail from is missing");
                    return;
                }

                if (txtSmtpServer.Text.Length == 0)
                {
                    ShowValidationMessage("SMTP server is missing");
                    return;
                }
                if (txtSmtpPort.Text.Length == 0)
                {
                    ShowValidationMessage("SMTP server port is missing");
                    return;
                }
                if (txtSmtpUserName.Text.Length == 0)
                {
                    ShowValidationMessage("SMTP Username is missing");
                    return;
                }
                if (txtSmtpPassword.Text.Length == 0)
                {
                    ShowValidationMessage("SMTP Password is missing");
                    return;
                }
            }
            #endregion

            #region [ADI Global]
            if (txtLoginPage.Text.Length == 0)
            {
                ShowValidationMessage("ADIGlobal Login Page is required");
                return;
            }
            if (txtADIUsername.Text.Length == 0)
            {
                ShowValidationMessage("ADIGlobal username is required");
                return;
            }
            if (txtADIPassword.Text.Length == 0)
            {
                ShowValidationMessage("ADIGlobal password is required");
                return;
            }
            try
            {
                Convert.ToInt32(txtAdiProductUpdateInterval.Text);
            }
            catch
            {
                ShowValidationMessage("ADI Product update interval is missing or invalid");
                return;
            }
            try
            {
                Convert.ToInt32(txtAdiCatagoryUpdateInterval.Text);
            }
            catch
            {
                ShowValidationMessage("ADI Catagory update interval is missing or invalid");
                return;
            }

            #endregion

            SaveSettings();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void SaveSettings()
        {
            try
            {
                #region [GENERAL]
                Settings.SetValue("ConcurrentThreads", typeof(Int32), Convert.ToInt32(txtThreadsCount.Text));
                
                Settings.SetValue("EnableCaching", typeof(bool), chkCaching.Checked);
                Settings.SetValue("CacheDuration", typeof(Int32), Convert.ToInt32(txtCacheDuration.Text));

                Settings.SetValue("ErrorsToFile", typeof(bool), chkErrorLog.Checked);
                Settings.SetValue("ErrorFileName", typeof(String), txtErrorFileName.Text);

                Settings.SetValue("RetryCount", typeof(Int32), Convert.ToInt32(txtRetryCount.Text));
                Settings.SetValue("DateFormat", typeof(String), txtDateFormat.Text);

                var str = lstSDay.SelectedItems.Cast<Object>().Select(x => (String)x);
                Settings.SetValue("ScheduleDay", typeof(String), String.Join("," , str));                
                Settings.SetValue("ScheduleTime", typeof(int), cmbSHour.SelectedItem);
                Settings.SetValue("ScheduleEndTime", typeof(int), cmbSEndHour.SelectedItem);
                Settings.SetValue("SpiderDay", typeof(String),cmbSpiderDay.SelectedItem);               
                Settings.SetValue("SpiderTime", typeof(int), cmbSpiderHour.SelectedItem);
                Settings.SetValue("SpiderMin", typeof(int), cmbSpiderMin.SelectedItem);
                Settings.SetValue("WebSpiderDB", typeof(String), txtcrawlDB.Text);
                Settings.SetValue("FinalTable", typeof(String), txtUpdateDB.Text);
                Settings.SetValue("PingRate", typeof(int), txtPing.Text);

                Settings.SetValue("CrawlImageFolder", typeof(String), txtCrawlImageFolder.Text);
                Settings.SetValue("UpdateImageFolder", typeof(String), txtUpdateImageFolder.Text);

                #endregion

                #region [EMAIL]
                Settings.SetValue("MailErrors", typeof(bool), chkSendErrorMail.Checked);
                Settings.SetValue("ErrorMailTo", typeof(String), txtMailTo.Text);
                Settings.SetValue("ErrorMailFrom", typeof(String), txtMailFrom.Text);
                Settings.SetValue("ErrorMailSubject", typeof(String), txtErrorFileName.Text);
                Settings.SetValue("SmtpServer", typeof(String), txtSmtpServer.Text);
                Settings.SetValue("SmtpPort", typeof(Int32), Convert.ToInt32(txtSmtpPort.Text));
                Settings.SetValue("SmtpSSL", typeof(bool), chkSmtpSSL.Checked);
                Settings.SetValue("SmtpUserName", typeof(String), txtSmtpUserName.Text);
                Settings.SetValue("SmtpPassword", typeof(String), txtSmtpPassword.Text);
                #endregion

                #region [ADIGLOBAL]
                Settings.SetValue("ADILoginPage", typeof(String), txtLoginPage.Text);
                Settings.SetValue("ADIUsername", typeof(String), txtADIUsername.Text);
                Settings.SetValue("ADIPassword", typeof(String), txtADIPassword.Text);
                Settings.SetValue("ADIProductDefaultUpdateInterval", typeof(Int32), Convert.ToInt32(txtAdiProductUpdateInterval.Text));
                Settings.SetValue("AdiCategoryUpdateInterval", typeof(Int32), Convert.ToInt32(txtAdiCatagoryUpdateInterval.Text));
                Settings.SetValue("AdiImagePrefix", typeof(String), txtImagePrefix.Text);
                #endregion
                
                Settings.Save();
                this.Hide();
                this.Close();
                this.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("Error saving settings!\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            #region [GENERAL]
            txtThreadsCount.Text = Settings.GetValue("ConcurrentThreads").ToString();
            
            chkCaching.Checked = Settings.GetValue("EnableCaching");
            txtCacheDuration.Text = Settings.GetValue("CacheDuration").ToString();

            chkErrorLog.Checked = Settings.GetValue("ErrorsToFile");
            txtErrorFileName.Text = Settings.GetValue("ErrorFileName");

            txtRetryCount.Text = Settings.GetValue("RetryCount").ToString();
            txtDateFormat.Text = Settings.GetValue("DateFormat");

            chkCaching_CheckedChanged(chkCaching, new EventArgs());
            chkErrorLog_CheckedChanged(chkErrorLog, new EventArgs());

            String []str = ((String) Settings.GetValue("ScheduleDay").ToString()).Split(',');
            for (int index = 0; index < lstSDay.Items.Count; index++)
            {
                String value = (String) lstSDay.Items[index];
                if (str.Contains(value))
                    lstSDay.SelectedItems.Add(value);
            }
            cmbSHour.SelectedItem = Settings.GetValue("ScheduleTime").ToString();
            cmbSEndHour.SelectedItem = Settings.GetValue("ScheduleEndTime").ToString();

            //cmbSpiderDay.SelectedItem = Settings.GetValue("SpiderDay").ToString();
            cmbSpiderDay.SelectedItem = Settings.GetValue("SpiderDay").ToString();

            cmbSpiderHour.SelectedItem = Settings.GetValue("SpiderTime").ToString();
            cmbSpiderMin.SelectedItem = Settings.GetValue("SpiderMin").ToString();
            txtcrawlDB.Text = Settings.GetValue("WebSpiderDB").ToString();
            txtUpdateDB.Text = Settings.GetValue("FinalTable").ToString();
            txtPing.Text = Settings.GetValue("PingRate").ToString();


            txtCrawlImageFolder.Text = Settings.GetValue("CrawlImageFolder");
            txtUpdateImageFolder.Text = Settings.GetValue("UpdateImageFolder");
            #endregion

            #region [EMAIL]
            chkSendErrorMail.Checked = Settings.GetValue("MailErrors");
            txtMailTo.Text = Settings.GetValue("ErrorMailTo");
            txtMailFrom.Text = Settings.GetValue("ErrorMailFrom");
            txtMailSubject.Text = Settings.GetValue("ErrorMailSubject");
            txtSmtpServer.Text = Settings.GetValue("SmtpServer");
            txtSmtpPort.Text = Settings.GetValue("SmtpPort").ToString();
            chkSmtpSSL.Checked = Settings.GetValue("SmtpSSL");
            txtSmtpUserName.Text = Settings.GetValue("SmtpUserName");
            txtSmtpPassword.Text = Settings.GetValue("SmtpPassword");

            chkSendErrorMail_CheckedChanged(chkSendErrorMail, new EventArgs());
            #endregion

            #region [ADIGLOBAL]
            txtLoginPage.Text = Settings.GetValue("ADILoginPage");
            txtADIUsername.Text = Settings.GetValue("ADIUsername");
            txtADIPassword.Text = Settings.GetValue("ADIPassword");
            txtAdiProductUpdateInterval.Text = Settings.GetValue("ADIProductDefaultUpdateInterval").ToString();
            txtAdiCatagoryUpdateInterval.Text = Settings.GetValue("AdiCategoryUpdateInterval").ToString();
            txtImagePrefix.Text = Settings.GetValue("AdiImagePrefix");
            #endregion
        }

        private void chkCaching_CheckedChanged(object sender, EventArgs e)
        {
            txtCacheDuration.Enabled = chkCaching.Checked;
        }

        private void chkErrorLog_CheckedChanged(object sender, EventArgs e)
        {
            txtErrorFileName.Enabled = chkErrorLog.Checked;
        }

        private void chkSendErrorMail_CheckedChanged(object sender, EventArgs e)
        {
            txtMailTo.Enabled = chkSendErrorMail.Checked;
            txtMailSubject.Enabled = chkSendErrorMail.Checked;
            txtSmtpServer.Enabled = chkSendErrorMail.Checked;
            txtSmtpPort.Enabled = chkSendErrorMail.Checked;
            chkSmtpSSL.Enabled = chkSendErrorMail.Checked;
            txtSmtpUserName.Enabled = chkSendErrorMail.Checked;
            txtSmtpPassword.Enabled = chkSendErrorMail.Checked;
        }

        private void txtNumericFields_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsDigit(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void ShowValidationMessage(String MessageText)
        {
            MessageBox.Show(this, MessageText, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBrowseCrawl_Click(object sender, EventArgs e)
        {
            BrowseFile(txtcrawlDB);
        }

        private void btnBrowseUpdate_Click(object sender, EventArgs e)
        {
            BrowseFile(txtUpdateDB);
        }

        private void btnCrawlImageBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder(txtCrawlImageFolder);
        }

        private void btnUpdateImageBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder(txtUpdateImageFolder);
        }

        #region [Browse]
        private void BrowseFile(TextBox textBox)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Access 2007 Database|*.accdb|Access Database|*.mdb";
            openFileDialog1.ShowDialog();
            textBox.Text = openFileDialog1.FileName;
        }

        private void BrowseFolder(TextBox textBox)
        {
            folderBrowserDialog1.ShowDialog();
            textBox.Text = folderBrowserDialog1.SelectedPath;
        }
        #endregion
        
    }
}
