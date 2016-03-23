namespace WebSpider
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtThreadsCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkErrorLog = new System.Windows.Forms.CheckBox();
            this.txtErrorFileName = new System.Windows.Forms.TextBox();
            this.chkSendErrorMail = new System.Windows.Forms.CheckBox();
            this.txtMailTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.chkSmtpSSL = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSmtpUserName = new System.Windows.Forms.TextBox();
            this.txtSmtpPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkCaching = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCacheDuration = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtADIUsername = new System.Windows.Forms.TextBox();
            this.txtADIPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMailSubject = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMailFrom = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtPing = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
            this.btnCrawlImageBrowse = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.txtCrawlImageFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseUpdate = new System.Windows.Forms.Button();
            this.btnBrowseCrawl = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtUpdateDB = new System.Windows.Forms.TextBox();
            this.txtcrawlDB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSpiderDay = new System.Windows.Forms.ComboBox();
            this.cmbSpiderMin = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbSpiderHour = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.Stockalert = new System.Windows.Forms.GroupBox();
            this.lstSDay = new System.Windows.Forms.ListBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cmbSEndHour = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbSHour = new System.Windows.Forms.ComboBox();
            this.txtDateFormat = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtRetryCount = new System.Windows.Forms.TextBox();
            this.tabMail = new System.Windows.Forms.TabPage();
            this.tabAdiGlobal = new System.Windows.Forms.TabPage();
            this.txtImagePrefix = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtLoginPage = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtAdiCatagoryUpdateInterval = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAdiProductUpdateInterval = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtUpdateImageFolder = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btnUpdateImageBrowse = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBoxDatabase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Stockalert.SuspendLayout();
            this.tabMail.SuspendLayout();
            this.tabAdiGlobal.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Concurrent Threads";
            // 
            // txtThreadsCount
            // 
            this.txtThreadsCount.Location = new System.Drawing.Point(114, 6);
            this.txtThreadsCount.Name = "txtThreadsCount";
            this.txtThreadsCount.Size = new System.Drawing.Size(36, 20);
            this.txtThreadsCount.TabIndex = 1;
            this.txtThreadsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThreadsCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericFields_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "File Name";
            // 
            // chkErrorLog
            // 
            this.chkErrorLog.AutoSize = true;
            this.chkErrorLog.Location = new System.Drawing.Point(10, 66);
            this.chkErrorLog.Name = "chkErrorLog";
            this.chkErrorLog.Size = new System.Drawing.Size(108, 17);
            this.chkErrorLog.TabIndex = 7;
            this.chkErrorLog.Text = "Write errors to file";
            this.chkErrorLog.UseVisualStyleBackColor = true;
            this.chkErrorLog.CheckedChanged += new System.EventHandler(this.chkErrorLog_CheckedChanged);
            // 
            // txtErrorFileName
            // 
            this.txtErrorFileName.Location = new System.Drawing.Point(203, 65);
            this.txtErrorFileName.Name = "txtErrorFileName";
            this.txtErrorFileName.Size = new System.Drawing.Size(220, 20);
            this.txtErrorFileName.TabIndex = 8;
            // 
            // chkSendErrorMail
            // 
            this.chkSendErrorMail.AutoSize = true;
            this.chkSendErrorMail.Location = new System.Drawing.Point(6, 6);
            this.chkSendErrorMail.Name = "chkSendErrorMail";
            this.chkSendErrorMail.Size = new System.Drawing.Size(121, 17);
            this.chkSendErrorMail.TabIndex = 9;
            this.chkSendErrorMail.Text = "Send errors as email";
            this.chkSendErrorMail.UseVisualStyleBackColor = true;
            this.chkSendErrorMail.CheckedChanged += new System.EventHandler(this.chkSendErrorMail_CheckedChanged);
            // 
            // txtMailTo
            // 
            this.txtMailTo.Location = new System.Drawing.Point(126, 30);
            this.txtMailTo.Multiline = true;
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Size = new System.Drawing.Size(293, 58);
            this.txtMailTo.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Send mail to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Seperate by comma(,)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "SMTP Server";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Location = new System.Drawing.Point(126, 167);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(293, 20);
            this.txtSmtpServer.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Server Port";
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Location = new System.Drawing.Point(126, 193);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(57, 20);
            this.txtSmtpPort.TabIndex = 13;
            this.txtSmtpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericFields_KeyPress);
            // 
            // chkSmtpSSL
            // 
            this.chkSmtpSSL.AutoSize = true;
            this.chkSmtpSSL.Location = new System.Drawing.Point(337, 195);
            this.chkSmtpSSL.Name = "chkSmtpSSL";
            this.chkSmtpSSL.Size = new System.Drawing.Size(82, 17);
            this.chkSmtpSSL.TabIndex = 14;
            this.chkSmtpSSL.Text = "Enable SSL";
            this.chkSmtpSSL.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Username";
            // 
            // txtSmtpUserName
            // 
            this.txtSmtpUserName.Location = new System.Drawing.Point(126, 220);
            this.txtSmtpUserName.Name = "txtSmtpUserName";
            this.txtSmtpUserName.Size = new System.Drawing.Size(293, 20);
            this.txtSmtpUserName.TabIndex = 15;
            // 
            // txtSmtpPassword
            // 
            this.txtSmtpPassword.Location = new System.Drawing.Point(126, 247);
            this.txtSmtpPassword.Name = "txtSmtpPassword";
            this.txtSmtpPassword.PasswordChar = '●';
            this.txtSmtpPassword.Size = new System.Drawing.Size(293, 20);
            this.txtSmtpPassword.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Password";
            // 
            // chkCaching
            // 
            this.chkCaching.AutoSize = true;
            this.chkCaching.Location = new System.Drawing.Point(10, 38);
            this.chkCaching.Name = "chkCaching";
            this.chkCaching.Size = new System.Drawing.Size(124, 17);
            this.chkCaching.TabIndex = 3;
            this.chkCaching.Text = "Enable data caching";
            this.chkCaching.UseVisualStyleBackColor = true;
            this.chkCaching.CheckedChanged += new System.EventHandler(this.chkCaching_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(201, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Cache Duration";
            // 
            // txtCacheDuration
            // 
            this.txtCacheDuration.Location = new System.Drawing.Point(288, 35);
            this.txtCacheDuration.Name = "txtCacheDuration";
            this.txtCacheDuration.Size = new System.Drawing.Size(55, 20);
            this.txtCacheDuration.TabIndex = 4;
            this.txtCacheDuration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericFields_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(349, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "minutes";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(368, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(287, 375);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Site Username";
            // 
            // txtADIUsername
            // 
            this.txtADIUsername.Location = new System.Drawing.Point(130, 32);
            this.txtADIUsername.Name = "txtADIUsername";
            this.txtADIUsername.Size = new System.Drawing.Size(293, 20);
            this.txtADIUsername.TabIndex = 5;
            // 
            // txtADIPassword
            // 
            this.txtADIPassword.Location = new System.Drawing.Point(130, 58);
            this.txtADIPassword.Name = "txtADIPassword";
            this.txtADIPassword.PasswordChar = '●';
            this.txtADIPassword.Size = new System.Drawing.Size(293, 20);
            this.txtADIPassword.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Site Password";
            // 
            // txtMailSubject
            // 
            this.txtMailSubject.Location = new System.Drawing.Point(126, 139);
            this.txtMailSubject.Name = "txtMailSubject";
            this.txtMailSubject.Size = new System.Drawing.Size(293, 20);
            this.txtMailSubject.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 142);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Email Subject";
            // 
            // txtMailFrom
            // 
            this.txtMailFrom.Location = new System.Drawing.Point(126, 113);
            this.txtMailFrom.Name = "txtMailFrom";
            this.txtMailFrom.Size = new System.Drawing.Size(293, 20);
            this.txtMailFrom.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 116);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Mail From";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabMail);
            this.tabControl1.Controls.Add(this.tabAdiGlobal);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(437, 358);
            this.tabControl1.TabIndex = 21;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.txtPing);
            this.tabGeneral.Controls.Add(this.label29);
            this.tabGeneral.Controls.Add(this.groupBoxDatabase);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.Stockalert);
            this.tabGeneral.Controls.Add(this.txtDateFormat);
            this.tabGeneral.Controls.Add(this.label21);
            this.tabGeneral.Controls.Add(this.label20);
            this.tabGeneral.Controls.Add(this.txtRetryCount);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.txtThreadsCount);
            this.tabGeneral.Controls.Add(this.chkCaching);
            this.tabGeneral.Controls.Add(this.label10);
            this.tabGeneral.Controls.Add(this.txtCacheDuration);
            this.tabGeneral.Controls.Add(this.label11);
            this.tabGeneral.Controls.Add(this.txtErrorFileName);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.chkErrorLog);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(429, 332);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // txtPing
            // 
            this.txtPing.Location = new System.Drawing.Point(361, 6);
            this.txtPing.Name = "txtPing";
            this.txtPing.Size = new System.Drawing.Size(56, 20);
            this.txtPing.TabIndex = 25;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(305, 9);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(50, 13);
            this.label29.TabIndex = 24;
            this.label29.Text = "Ping/Min";
            // 
            // groupBoxDatabase
            // 
            this.groupBoxDatabase.Controls.Add(this.btnCrawlImageBrowse);
            this.groupBoxDatabase.Controls.Add(this.label32);
            this.groupBoxDatabase.Controls.Add(this.txtCrawlImageFolder);
            this.groupBoxDatabase.Controls.Add(this.btnBrowseUpdate);
            this.groupBoxDatabase.Controls.Add(this.btnBrowseCrawl);
            this.groupBoxDatabase.Controls.Add(this.label28);
            this.groupBoxDatabase.Controls.Add(this.label27);
            this.groupBoxDatabase.Controls.Add(this.txtUpdateDB);
            this.groupBoxDatabase.Controls.Add(this.txtcrawlDB);
            this.groupBoxDatabase.Location = new System.Drawing.Point(12, 206);
            this.groupBoxDatabase.Name = "groupBoxDatabase";
            this.groupBoxDatabase.Size = new System.Drawing.Size(411, 122);
            this.groupBoxDatabase.TabIndex = 23;
            this.groupBoxDatabase.TabStop = false;
            this.groupBoxDatabase.Text = "Database Path";
            // 
            // btnCrawlImageBrowse
            // 
            this.btnCrawlImageBrowse.Location = new System.Drawing.Point(330, 64);
            this.btnCrawlImageBrowse.Name = "btnCrawlImageBrowse";
            this.btnCrawlImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnCrawlImageBrowse.TabIndex = 8;
            this.btnCrawlImageBrowse.Text = "Browse";
            this.btnCrawlImageBrowse.UseVisualStyleBackColor = true;
            this.btnCrawlImageBrowse.Click += new System.EventHandler(this.btnCrawlImageBrowse_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(16, 69);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(97, 13);
            this.label32.TabIndex = 7;
            this.label32.Text = "Crawl Image Folder";
            // 
            // txtCrawlImageFolder
            // 
            this.txtCrawlImageFolder.Location = new System.Drawing.Point(118, 66);
            this.txtCrawlImageFolder.Name = "txtCrawlImageFolder";
            this.txtCrawlImageFolder.Size = new System.Drawing.Size(202, 20);
            this.txtCrawlImageFolder.TabIndex = 6;
            // 
            // btnBrowseUpdate
            // 
            this.btnBrowseUpdate.Location = new System.Drawing.Point(330, 37);
            this.btnBrowseUpdate.Name = "btnBrowseUpdate";
            this.btnBrowseUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseUpdate.TabIndex = 5;
            this.btnBrowseUpdate.Text = "Browse";
            this.btnBrowseUpdate.UseVisualStyleBackColor = true;
            this.btnBrowseUpdate.Click += new System.EventHandler(this.btnBrowseUpdate_Click);
            // 
            // btnBrowseCrawl
            // 
            this.btnBrowseCrawl.Location = new System.Drawing.Point(330, 10);
            this.btnBrowseCrawl.Name = "btnBrowseCrawl";
            this.btnBrowseCrawl.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCrawl.TabIndex = 4;
            this.btnBrowseCrawl.Text = "Browse";
            this.btnBrowseCrawl.UseVisualStyleBackColor = true;
            this.btnBrowseCrawl.Click += new System.EventHandler(this.btnBrowseCrawl_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(22, 42);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(90, 13);
            this.label28.TabIndex = 3;
            this.label28.Text = "Update Mode DB";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(31, 15);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(81, 13);
            this.label27.TabIndex = 2;
            this.label27.Text = "Crawl Mode DB";
            // 
            // txtUpdateDB
            // 
            this.txtUpdateDB.Location = new System.Drawing.Point(118, 39);
            this.txtUpdateDB.Name = "txtUpdateDB";
            this.txtUpdateDB.Size = new System.Drawing.Size(202, 20);
            this.txtUpdateDB.TabIndex = 1;
            // 
            // txtcrawlDB
            // 
            this.txtcrawlDB.Location = new System.Drawing.Point(118, 12);
            this.txtcrawlDB.Name = "txtcrawlDB";
            this.txtcrawlDB.Size = new System.Drawing.Size(202, 20);
            this.txtcrawlDB.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSpiderDay);
            this.groupBox1.Controls.Add(this.cmbSpiderMin);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.cmbSpiderHour);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Location = new System.Drawing.Point(10, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 44);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spider Scheduler";
            // 
            // cmbSpiderDay
            // 
            this.cmbSpiderDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpiderDay.FormattingEnabled = true;
            this.cmbSpiderDay.Items.AddRange(new object[] {
            "ALL",
            "MON",
            "TUE",
            "WED",
            "THU",
            "FRI",
            "SAT",
            "SUN"});
            this.cmbSpiderDay.Location = new System.Drawing.Point(70, 16);
            this.cmbSpiderDay.Name = "cmbSpiderDay";
            this.cmbSpiderDay.Size = new System.Drawing.Size(63, 21);
            this.cmbSpiderDay.TabIndex = 23;
            // 
            // cmbSpiderMin
            // 
            this.cmbSpiderMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpiderMin.FormattingEnabled = true;
            this.cmbSpiderMin.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cmbSpiderMin.Location = new System.Drawing.Point(324, 16);
            this.cmbSpiderMin.Name = "cmbSpiderMin";
            this.cmbSpiderMin.Size = new System.Drawing.Size(63, 21);
            this.cmbSpiderMin.TabIndex = 22;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(294, 20);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(24, 13);
            this.label26.TabIndex = 21;
            this.label26.Text = "Min";
            // 
            // cmbSpiderHour
            // 
            this.cmbSpiderHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpiderHour.FormattingEnabled = true;
            this.cmbSpiderHour.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.cmbSpiderHour.Location = new System.Drawing.Point(202, 16);
            this.cmbSpiderHour.Name = "cmbSpiderHour";
            this.cmbSpiderHour.Size = new System.Drawing.Size(63, 21);
            this.cmbSpiderHour.TabIndex = 20;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(29, 19);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(26, 13);
            this.label25.TabIndex = 17;
            this.label25.Text = "Day";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(166, 19);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(30, 13);
            this.label24.TabIndex = 19;
            this.label24.Text = "Hour";
            // 
            // Stockalert
            // 
            this.Stockalert.Controls.Add(this.lstSDay);
            this.Stockalert.Controls.Add(this.label30);
            this.Stockalert.Controls.Add(this.cmbSEndHour);
            this.Stockalert.Controls.Add(this.label22);
            this.Stockalert.Controls.Add(this.label23);
            this.Stockalert.Controls.Add(this.cmbSHour);
            this.Stockalert.Location = new System.Drawing.Point(10, 162);
            this.Stockalert.Name = "Stockalert";
            this.Stockalert.Size = new System.Drawing.Size(413, 44);
            this.Stockalert.TabIndex = 21;
            this.Stockalert.TabStop = false;
            this.Stockalert.Text = "Stock Alert Scheduler";
            // 
            // lstSDay
            // 
            this.lstSDay.FormattingEnabled = true;
            this.lstSDay.Items.AddRange(new object[] {
            "MON",
            "TUE",
            "WED",
            "THU",
            "FRI",
            "SAT",
            "SUN"});
            this.lstSDay.Location = new System.Drawing.Point(61, 14);
            this.lstSDay.Name = "lstSDay";
            this.lstSDay.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstSDay.Size = new System.Drawing.Size(62, 30);
            this.lstSDay.TabIndex = 20;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(270, 19);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 13);
            this.label30.TabIndex = 17;
            this.label30.Text = "End Hour";
            // 
            // cmbSEndHour
            // 
            this.cmbSEndHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSEndHour.FormattingEnabled = true;
            this.cmbSEndHour.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.cmbSEndHour.Location = new System.Drawing.Point(324, 15);
            this.cmbSEndHour.Name = "cmbSEndHour";
            this.cmbSEndHour.Size = new System.Drawing.Size(63, 21);
            this.cmbSEndHour.TabIndex = 18;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(29, 19);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(26, 13);
            this.label22.TabIndex = 13;
            this.label22.Text = "Day";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(141, 18);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 13);
            this.label23.TabIndex = 15;
            this.label23.Text = "Start Hour";
            // 
            // cmbSHour
            // 
            this.cmbSHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSHour.FormattingEnabled = true;
            this.cmbSHour.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.cmbSHour.Location = new System.Drawing.Point(202, 15);
            this.cmbSHour.Name = "cmbSHour";
            this.cmbSHour.Size = new System.Drawing.Size(63, 21);
            this.cmbSHour.TabIndex = 16;
            // 
            // txtDateFormat
            // 
            this.txtDateFormat.Location = new System.Drawing.Point(271, 92);
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Size = new System.Drawing.Size(152, 20);
            this.txtDateFormat.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(204, 95);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 13);
            this.label21.TabIndex = 11;
            this.label21.Text = "Date Format";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 95);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(137, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "Web Requests Retry Count";
            // 
            // txtRetryCount
            // 
            this.txtRetryCount.Location = new System.Drawing.Point(149, 92);
            this.txtRetryCount.Name = "txtRetryCount";
            this.txtRetryCount.Size = new System.Drawing.Size(49, 20);
            this.txtRetryCount.TabIndex = 9;
            this.txtRetryCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericFields_KeyPress);
            // 
            // tabMail
            // 
            this.tabMail.Controls.Add(this.chkSendErrorMail);
            this.tabMail.Controls.Add(this.label15);
            this.tabMail.Controls.Add(this.txtMailTo);
            this.tabMail.Controls.Add(this.txtMailFrom);
            this.tabMail.Controls.Add(this.label3);
            this.tabMail.Controls.Add(this.label14);
            this.tabMail.Controls.Add(this.label4);
            this.tabMail.Controls.Add(this.txtMailSubject);
            this.tabMail.Controls.Add(this.label5);
            this.tabMail.Controls.Add(this.txtSmtpServer);
            this.tabMail.Controls.Add(this.label6);
            this.tabMail.Controls.Add(this.txtSmtpPort);
            this.tabMail.Controls.Add(this.chkSmtpSSL);
            this.tabMail.Controls.Add(this.label7);
            this.tabMail.Controls.Add(this.txtSmtpUserName);
            this.tabMail.Controls.Add(this.label8);
            this.tabMail.Controls.Add(this.txtSmtpPassword);
            this.tabMail.Location = new System.Drawing.Point(4, 22);
            this.tabMail.Name = "tabMail";
            this.tabMail.Padding = new System.Windows.Forms.Padding(3);
            this.tabMail.Size = new System.Drawing.Size(429, 332);
            this.tabMail.TabIndex = 1;
            this.tabMail.Text = "Email";
            this.tabMail.UseVisualStyleBackColor = true;
            // 
            // tabAdiGlobal
            // 
            this.tabAdiGlobal.Controls.Add(this.txtImagePrefix);
            this.tabAdiGlobal.Controls.Add(this.label9);
            this.tabAdiGlobal.Controls.Add(this.label31);
            this.tabAdiGlobal.Controls.Add(this.txtLoginPage);
            this.tabAdiGlobal.Controls.Add(this.label18);
            this.tabAdiGlobal.Controls.Add(this.label19);
            this.tabAdiGlobal.Controls.Add(this.txtAdiCatagoryUpdateInterval);
            this.tabAdiGlobal.Controls.Add(this.label17);
            this.tabAdiGlobal.Controls.Add(this.label16);
            this.tabAdiGlobal.Controls.Add(this.txtAdiProductUpdateInterval);
            this.tabAdiGlobal.Controls.Add(this.txtADIPassword);
            this.tabAdiGlobal.Controls.Add(this.label13);
            this.tabAdiGlobal.Controls.Add(this.label12);
            this.tabAdiGlobal.Controls.Add(this.txtADIUsername);
            this.tabAdiGlobal.Location = new System.Drawing.Point(4, 22);
            this.tabAdiGlobal.Name = "tabAdiGlobal";
            this.tabAdiGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdiGlobal.Size = new System.Drawing.Size(429, 332);
            this.tabAdiGlobal.TabIndex = 2;
            this.tabAdiGlobal.Text = "Adi Global";
            this.tabAdiGlobal.UseVisualStyleBackColor = true;
            // 
            // txtImagePrefix
            // 
            this.txtImagePrefix.Location = new System.Drawing.Point(130, 137);
            this.txtImagePrefix.Name = "txtImagePrefix";
            this.txtImagePrefix.Size = new System.Drawing.Size(293, 20);
            this.txtImagePrefix.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Image Prefix";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(7, 9);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(61, 13);
            this.label31.TabIndex = 13;
            this.label31.Text = "Login Page";
            // 
            // txtLoginPage
            // 
            this.txtLoginPage.Location = new System.Drawing.Point(130, 6);
            this.txtLoginPage.Name = "txtLoginPage";
            this.txtLoginPage.Size = new System.Drawing.Size(293, 20);
            this.txtLoginPage.TabIndex = 14;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(380, 111);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 13);
            this.label18.TabIndex = 12;
            this.label18.Text = "minutes";
            this.label18.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(122, 13);
            this.label19.TabIndex = 11;
            this.label19.Text = "Catagory update interval";
            this.label19.Visible = false;
            // 
            // txtAdiCatagoryUpdateInterval
            // 
            this.txtAdiCatagoryUpdateInterval.Location = new System.Drawing.Point(130, 111);
            this.txtAdiCatagoryUpdateInterval.Name = "txtAdiCatagoryUpdateInterval";
            this.txtAdiCatagoryUpdateInterval.Size = new System.Drawing.Size(244, 20);
            this.txtAdiCatagoryUpdateInterval.TabIndex = 10;
            this.txtAdiCatagoryUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdiCatagoryUpdateInterval.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(380, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "minutes";
            this.label17.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(114, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Default update interval";
            this.label16.Visible = false;
            // 
            // txtAdiProductUpdateInterval
            // 
            this.txtAdiProductUpdateInterval.Location = new System.Drawing.Point(130, 85);
            this.txtAdiProductUpdateInterval.Name = "txtAdiProductUpdateInterval";
            this.txtAdiProductUpdateInterval.Size = new System.Drawing.Size(244, 20);
            this.txtAdiProductUpdateInterval.TabIndex = 7;
            this.txtAdiProductUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdiProductUpdateInterval.Visible = false;
            this.txtAdiProductUpdateInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericFields_KeyPress);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtUpdateImageFolder
            // 
            this.txtUpdateImageFolder.Location = new System.Drawing.Point(145, 333);
            this.txtUpdateImageFolder.Name = "txtUpdateImageFolder";
            this.txtUpdateImageFolder.Size = new System.Drawing.Size(202, 20);
            this.txtUpdateImageFolder.TabIndex = 22;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(36, 336);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(106, 13);
            this.label33.TabIndex = 23;
            this.label33.Text = "Update Image Folder";
            // 
            // btnUpdateImageBrowse
            // 
            this.btnUpdateImageBrowse.Location = new System.Drawing.Point(357, 331);
            this.btnUpdateImageBrowse.Name = "btnUpdateImageBrowse";
            this.btnUpdateImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateImageBrowse.TabIndex = 24;
            this.btnUpdateImageBrowse.Text = "Browse";
            this.btnUpdateImageBrowse.UseVisualStyleBackColor = true;
            this.btnUpdateImageBrowse.Click += new System.EventHandler(this.btnUpdateImageBrowse_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 404);
            this.Controls.Add(this.btnUpdateImageBrowse);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.txtUpdateImageFolder);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupBoxDatabase.ResumeLayout(false);
            this.groupBoxDatabase.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Stockalert.ResumeLayout(false);
            this.Stockalert.PerformLayout();
            this.tabMail.ResumeLayout(false);
            this.tabMail.PerformLayout();
            this.tabAdiGlobal.ResumeLayout(false);
            this.tabAdiGlobal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThreadsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkErrorLog;
        private System.Windows.Forms.TextBox txtErrorFileName;
        private System.Windows.Forms.CheckBox chkSendErrorMail;
        private System.Windows.Forms.TextBox txtMailTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.CheckBox chkSmtpSSL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSmtpUserName;
        private System.Windows.Forms.TextBox txtSmtpPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkCaching;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCacheDuration;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtADIUsername;
        private System.Windows.Forms.TextBox txtADIPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMailSubject;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMailFrom;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabMail;
        private System.Windows.Forms.TabPage tabAdiGlobal;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAdiProductUpdateInterval;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtAdiCatagoryUpdateInterval;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtRetryCount;
        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbSHour;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSpiderHour;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox Stockalert;
        private System.Windows.Forms.ComboBox cmbSpiderMin;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBoxDatabase;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtUpdateDB;
        private System.Windows.Forms.TextBox txtcrawlDB;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtPing;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cmbSEndHour;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtLoginPage;
        private System.Windows.Forms.Button btnBrowseUpdate;
        private System.Windows.Forms.Button btnBrowseCrawl;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCrawlImageBrowse;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtCrawlImageFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cmbSpiderDay;
        private System.Windows.Forms.ListBox lstSDay;
        private System.Windows.Forms.TextBox txtImagePrefix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUpdateImageFolder;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button btnUpdateImageBrowse;

    }
}