namespace WebSpider
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "-1",
            "Current Task",
            "Current Running Task not scheduled",
            "",
            "",
            "NO",
            "NO",
            "YES",
            "",
            ""}, -1);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.toolBarTasks = new System.Windows.Forms.ToolBar();
            this.toolBarButtonContinue = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPause = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonStop = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDeleteAll = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tabAdiGlobal = new System.Windows.Forms.TabControl();
            this.tabPageAdiCategory = new System.Windows.Forms.TabPage();
            this.treeCatagory = new System.Windows.Forms.TreeView();
            this.tabPageAdiBrand = new System.Windows.Forms.TabPage();
            this.treeViewBrands = new System.Windows.Forms.TreeView();
            this.tabPageAdiProducts = new System.Windows.Forms.TabPage();
            this.chkAdiProductsCheckAll = new System.Windows.Forms.CheckBox();
            this.treeViewAdiProducts = new System.Windows.Forms.TreeView();
            this.listViewThreads = new System.Windows.Forms.ListView();
            this.columnHeaderTaskID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskNameValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderIgnitoMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDownloadImages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderWebSite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnStart = new System.Windows.Forms.Button();
            this.chkAdiDownloadImage = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusActiveThreads = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusActiveThreadCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSeperator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusQueued = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusQueuesThreadsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusSeperator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusExport = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarSyncTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timerSpider = new System.Windows.Forms.Timer(this.components);
            this.btnTimer = new System.Windows.Forms.Button();
            this.timerPingRate = new System.Windows.Forms.Timer(this.components);
            this.chkAdiIncognito = new System.Windows.Forms.CheckBox();
            this.timerTasks = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageTasks = new System.Windows.Forms.TabPage();
            this.tabPageAdiGlobal = new System.Windows.Forms.TabPage();
            this.cmbAdiMode = new System.Windows.Forms.ComboBox();
            this.toolBarAdiGlobal = new System.Windows.Forms.ToolBar();
            this.toolBarButtonAdiPriority = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonAdiReload = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonAdiScheduler = new System.Windows.Forms.ToolBarButton();
            this.tabPageSecLock = new System.Windows.Forms.TabPage();
            this.chkSeclockIncognito = new System.Windows.Forms.CheckBox();
            this.chkSeclockDownloadImages = new System.Windows.Forms.CheckBox();
            this.toolBarSecLock = new System.Windows.Forms.ToolBar();
            this.toolBarButtonSecLockRefresh = new System.Windows.Forms.ToolBarButton();
            this.tabControlSecLock = new System.Windows.Forms.TabControl();
            this.tabPageSeclockManufacturers = new System.Windows.Forms.TabPage();
            this.chkSecLockManufanufacturersSelectAll = new System.Windows.Forms.CheckBox();
            this.tvSecLockManufacturers = new System.Windows.Forms.TreeView();
            this.tabPageSecLockCategories = new System.Windows.Forms.TabPage();
            this.chkSecLockCategoriessSelectAll = new System.Windows.Forms.CheckBox();
            this.tvSecLockCategories = new System.Windows.Forms.TreeView();
            this.tabPageSecLockProducts = new System.Windows.Forms.TabPage();
            this.chkSecLockProductsSelectAll = new System.Windows.Forms.CheckBox();
            this.tvsecLockProducts = new System.Windows.Forms.TreeView();
            this.tabPageSecLockCSV = new System.Windows.Forms.TabPage();
            this.btnSecLockCSVStart = new System.Windows.Forms.Button();
            this.chkSeclockCSVDownloadImages = new System.Windows.Forms.CheckBox();
            this.chkSeclockCSVIncognito = new System.Windows.Forms.CheckBox();
            this.chkSecLockCSVProductSelectAll = new System.Windows.Forms.CheckBox();
            this.tvSecLockCSVProduct = new System.Windows.Forms.TreeView();
            this.tabPageTri = new System.Windows.Forms.TabPage();
            this.toolBarButtonTriReload = new System.Windows.Forms.Button();
            this.label48 = new System.Windows.Forms.Label();
            this.cmbTriMode = new System.Windows.Forms.ComboBox();
            this.chkTRIownloadImages = new System.Windows.Forms.CheckBox();
            this.chkTriIncognito = new System.Windows.Forms.CheckBox();
            this.tabControlTri = new System.Windows.Forms.TabControl();
            this.tabTriManufacturers = new System.Windows.Forms.TabPage();
            this.chkTriManufanufacturersSelectAll = new System.Windows.Forms.CheckBox();
            this.tvTriManufacturers = new System.Windows.Forms.TreeView();
            this.tabTriMainCategory = new System.Windows.Forms.TabPage();
            this.tabTriProduct = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.btnSettingsCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.btnCleanDb = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDocFolderBrowse = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.txtDocFolder = new System.Windows.Forms.TextBox();
            this.btnUpdateImageBrowse = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.btnCrawlImageBrowse = new System.Windows.Forms.Button();
            this.txtCrawlImageFolder = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtUpdateImageFolder = new System.Windows.Forms.TextBox();
            this.txtPing = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
            this.btnGenerateAdiUpdateSchema = new System.Windows.Forms.Button();
            this.btnGenerateAdiCrawlSchema = new System.Windows.Forms.Button();
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtThreadsCount = new System.Windows.Forms.TextBox();
            this.chkCaching = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCacheDuration = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtErrorFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkErrorLog = new System.Windows.Forms.CheckBox();
            this.tabMail = new System.Windows.Forms.TabPage();
            this.chkSendErrorMail = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMailTo = new System.Windows.Forms.TextBox();
            this.txtMailFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMailSubject = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.chkSmtpSSL = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSmtpUserName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSmtpPassword = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtAdiGlobalImagePrefix = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtAdiGlobalLoginPage = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtAdiGlobalCatagoryUpdateInterval = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAdiGlobalProductUpdateInterval = new System.Windows.Forms.TextBox();
            this.txtAdiGlobalPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtAdiGlobalUsername = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSecLockImagePrefix = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtSecLockLoginPage = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txtSecLockCategoryUpdateInterval = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtSecLockUpdateInterval = new System.Windows.Forms.TextBox();
            this.txtSecLockPassword = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txtSecLockUserName = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtTriedImagePrefix = new System.Windows.Forms.TextBox();
            this.txtTriedPassword = new System.Windows.Forms.TextBox();
            this.txtTriedUserName = new System.Windows.Forms.TextBox();
            this.txtTriedLoginPage = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.contextMenuStripTaskEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemIgnitoMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDownloadImages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.listViewScheduleTasks = new System.Windows.Forms.ListView();
            this.columnHeaderTaskHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeadertaskHeaderTaskDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderSite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderScheduleFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderRepeat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderRepeatInterval = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderLastRun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTaskHeaderNextRun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label34 = new System.Windows.Forms.Label();
            this.btnNewSchedule = new System.Windows.Forms.Button();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabAdiGlobal.SuspendLayout();
            this.tabPageAdiCategory.SuspendLayout();
            this.tabPageAdiBrand.SuspendLayout();
            this.tabPageAdiProducts.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageTasks.SuspendLayout();
            this.tabPageAdiGlobal.SuspendLayout();
            this.tabPageSecLock.SuspendLayout();
            this.tabControlSecLock.SuspendLayout();
            this.tabPageSeclockManufacturers.SuspendLayout();
            this.tabPageSecLockCategories.SuspendLayout();
            this.tabPageSecLockProducts.SuspendLayout();
            this.tabPageSecLockCSV.SuspendLayout();
            this.tabPageTri.SuspendLayout();
            this.tabControlTri.SuspendLayout();
            this.tabTriManufacturers.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxDatabase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Stockalert.SuspendLayout();
            this.tabMail.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStripTaskEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            this.imageList2.Images.SetKeyName(5, "Priority.png");
            this.imageList2.Images.SetKeyName(6, "delete.png");
            this.imageList2.Images.SetKeyName(7, "pause.png");
            this.imageList2.Images.SetKeyName(8, "Play.png");
            this.imageList2.Images.SetKeyName(9, "reload.png");
            this.imageList2.Images.SetKeyName(10, "scheduler.png");
            this.imageList2.Images.SetKeyName(11, "settings.png");
            this.imageList2.Images.SetKeyName(12, "Stop.png");
            // 
            // toolBarTasks
            // 
            this.toolBarTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBarTasks.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarTasks.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonContinue,
            this.toolBarButtonPause,
            this.toolBarButtonStop,
            this.toolBarButton1,
            this.toolBarButtonDeleteAll,
            this.toolBarButton2});
            this.toolBarTasks.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBarTasks.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBarTasks.DropDownArrows = true;
            this.toolBarTasks.ImageList = this.imageList2;
            this.toolBarTasks.Location = new System.Drawing.Point(6, 6);
            this.toolBarTasks.Name = "toolBarTasks";
            this.toolBarTasks.ShowToolTips = true;
            this.toolBarTasks.Size = new System.Drawing.Size(900, 28);
            this.toolBarTasks.TabIndex = 12;
            this.toolBarTasks.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarTasks_ButtonClick);
            // 
            // toolBarButtonContinue
            // 
            this.toolBarButtonContinue.ImageIndex = 8;
            this.toolBarButtonContinue.Name = "toolBarButtonContinue";
            this.toolBarButtonContinue.ToolTipText = "Coninue parsing process";
            // 
            // toolBarButtonPause
            // 
            this.toolBarButtonPause.ImageIndex = 7;
            this.toolBarButtonPause.Name = "toolBarButtonPause";
            this.toolBarButtonPause.ToolTipText = "Pause parsing process";
            // 
            // toolBarButtonStop
            // 
            this.toolBarButtonStop.ImageIndex = 12;
            this.toolBarButtonStop.Name = "toolBarButtonStop";
            this.toolBarButtonStop.ToolTipText = "Stop parsing process";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonDeleteAll
            // 
            this.toolBarButtonDeleteAll.ImageIndex = 6;
            this.toolBarButtonDeleteAll.Name = "toolBarButtonDeleteAll";
            this.toolBarButtonDeleteAll.ToolTipText = "Delete all results";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tabAdiGlobal
            // 
            this.tabAdiGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabAdiGlobal.Controls.Add(this.tabPageAdiCategory);
            this.tabAdiGlobal.Controls.Add(this.tabPageAdiBrand);
            this.tabAdiGlobal.Controls.Add(this.tabPageAdiProducts);
            this.tabAdiGlobal.Location = new System.Drawing.Point(6, 40);
            this.tabAdiGlobal.Name = "tabAdiGlobal";
            this.tabAdiGlobal.SelectedIndex = 0;
            this.tabAdiGlobal.Size = new System.Drawing.Size(939, 346);
            this.tabAdiGlobal.TabIndex = 13;
            // 
            // tabPageAdiCategory
            // 
            this.tabPageAdiCategory.Controls.Add(this.treeCatagory);
            this.tabPageAdiCategory.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdiCategory.Name = "tabPageAdiCategory";
            this.tabPageAdiCategory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdiCategory.Size = new System.Drawing.Size(931, 320);
            this.tabPageAdiCategory.TabIndex = 0;
            this.tabPageAdiCategory.Text = "Category";
            this.tabPageAdiCategory.UseVisualStyleBackColor = true;
            // 
            // treeCatagory
            // 
            this.treeCatagory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCatagory.CheckBoxes = true;
            this.treeCatagory.Location = new System.Drawing.Point(3, 3);
            this.treeCatagory.Name = "treeCatagory";
            this.treeCatagory.Size = new System.Drawing.Size(922, 314);
            this.treeCatagory.TabIndex = 0;
            this.treeCatagory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCatagory_AfterCheck);
            // 
            // tabPageAdiBrand
            // 
            this.tabPageAdiBrand.Controls.Add(this.treeViewBrands);
            this.tabPageAdiBrand.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdiBrand.Name = "tabPageAdiBrand";
            this.tabPageAdiBrand.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdiBrand.Size = new System.Drawing.Size(931, 320);
            this.tabPageAdiBrand.TabIndex = 1;
            this.tabPageAdiBrand.Text = "Brand";
            this.tabPageAdiBrand.UseVisualStyleBackColor = true;
            // 
            // treeViewBrands
            // 
            this.treeViewBrands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewBrands.CheckBoxes = true;
            this.treeViewBrands.Location = new System.Drawing.Point(0, 0);
            this.treeViewBrands.Name = "treeViewBrands";
            this.treeViewBrands.Size = new System.Drawing.Size(931, 320);
            this.treeViewBrands.TabIndex = 0;
            this.treeViewBrands.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewBrands_AfterCheck);
            // 
            // tabPageAdiProducts
            // 
            this.tabPageAdiProducts.Controls.Add(this.chkAdiProductsCheckAll);
            this.tabPageAdiProducts.Controls.Add(this.treeViewAdiProducts);
            this.tabPageAdiProducts.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdiProducts.Name = "tabPageAdiProducts";
            this.tabPageAdiProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdiProducts.Size = new System.Drawing.Size(931, 320);
            this.tabPageAdiProducts.TabIndex = 3;
            this.tabPageAdiProducts.Text = "Products";
            this.tabPageAdiProducts.UseVisualStyleBackColor = true;
            // 
            // chkAdiProductsCheckAll
            // 
            this.chkAdiProductsCheckAll.AutoSize = true;
            this.chkAdiProductsCheckAll.Location = new System.Drawing.Point(834, 15);
            this.chkAdiProductsCheckAll.Name = "chkAdiProductsCheckAll";
            this.chkAdiProductsCheckAll.Size = new System.Drawing.Size(71, 17);
            this.chkAdiProductsCheckAll.TabIndex = 1;
            this.chkAdiProductsCheckAll.Text = "Check All";
            this.chkAdiProductsCheckAll.UseVisualStyleBackColor = true;
            this.chkAdiProductsCheckAll.CheckedChanged += new System.EventHandler(this.chkAdiProductsCheckAll_CheckedChanged);
            // 
            // treeViewAdiProducts
            // 
            this.treeViewAdiProducts.CheckBoxes = true;
            this.treeViewAdiProducts.Location = new System.Drawing.Point(4, 4);
            this.treeViewAdiProducts.Name = "treeViewAdiProducts";
            this.treeViewAdiProducts.Size = new System.Drawing.Size(924, 295);
            this.treeViewAdiProducts.TabIndex = 0;
            this.treeViewAdiProducts.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAdiProducts_AfterCheck);
            // 
            // listViewThreads
            // 
            this.listViewThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewThreads.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listViewThreads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTaskID,
            this.columnHeaderTaskNameValue,
            this.columnHeaderTaskName,
            this.columnHeaderTaskStatus,
            this.columnHeaderTaskType,
            this.columnHeaderTaskMode,
            this.columnHeaderIgnitoMode,
            this.columnHeaderDownloadImages,
            this.columnHeaderWebSite});
            this.listViewThreads.FullRowSelect = true;
            this.listViewThreads.GridLines = true;
            this.listViewThreads.HideSelection = false;
            this.listViewThreads.Location = new System.Drawing.Point(6, 40);
            this.listViewThreads.Name = "listViewThreads";
            this.listViewThreads.Size = new System.Drawing.Size(944, 325);
            this.listViewThreads.TabIndex = 14;
            this.listViewThreads.UseCompatibleStateImageBehavior = false;
            this.listViewThreads.View = System.Windows.Forms.View.Details;
            this.listViewThreads.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewThreads_KeyDown);
            this.listViewThreads.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewThreads_MouseClick);
            // 
            // columnHeaderTaskID
            // 
            this.columnHeaderTaskID.Text = "TaskID";
            this.columnHeaderTaskID.Width = 0;
            // 
            // columnHeaderTaskNameValue
            // 
            this.columnHeaderTaskNameValue.Text = "TaskNameValue";
            // 
            // columnHeaderTaskName
            // 
            this.columnHeaderTaskName.Text = "TaskName";
            this.columnHeaderTaskName.Width = 200;
            // 
            // columnHeaderTaskStatus
            // 
            this.columnHeaderTaskStatus.Text = "Status";
            this.columnHeaderTaskStatus.Width = 100;
            // 
            // columnHeaderTaskType
            // 
            this.columnHeaderTaskType.Text = "Type";
            // 
            // columnHeaderTaskMode
            // 
            this.columnHeaderTaskMode.Text = "Mode";
            // 
            // columnHeaderIgnitoMode
            // 
            this.columnHeaderIgnitoMode.Text = "Ignito";
            // 
            // columnHeaderDownloadImages
            // 
            this.columnHeaderDownloadImages.Text = "Images";
            // 
            // columnHeaderWebSite
            // 
            this.columnHeaderWebSite.Text = "Site";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnStart.Location = new System.Drawing.Point(804, 11);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(141, 23);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkAdiDownloadImage
            // 
            this.chkAdiDownloadImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAdiDownloadImage.AutoSize = true;
            this.chkAdiDownloadImage.Location = new System.Drawing.Point(755, 15);
            this.chkAdiDownloadImage.Name = "chkAdiDownloadImage";
            this.chkAdiDownloadImage.Size = new System.Drawing.Size(151, 17);
            this.chkAdiDownloadImage.TabIndex = 19;
            this.chkAdiDownloadImage.Text = "Download Product Images";
            this.chkAdiDownloadImage.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusActiveThreads,
            this.toolStripStatusActiveThreadCount,
            this.toolStripStatusLabelSeperator1,
            this.toolStripStatusQueued,
            this.toolStripStatusQueuesThreadsCount,
            this.toolStripStatusSeperator2,
            this.toolStripStatusExport});
            this.statusStrip1.Location = new System.Drawing.Point(0, 545);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(961, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusActiveThreads
            // 
            this.toolStripStatusActiveThreads.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusActiveThreads.Name = "toolStripStatusActiveThreads";
            this.toolStripStatusActiveThreads.Size = new System.Drawing.Size(85, 17);
            this.toolStripStatusActiveThreads.Text = "Active Threads";
            // 
            // toolStripStatusActiveThreadCount
            // 
            this.toolStripStatusActiveThreadCount.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusActiveThreadCount.Name = "toolStripStatusActiveThreadCount";
            this.toolStripStatusActiveThreadCount.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusActiveThreadCount.Text = "0";
            // 
            // toolStripStatusLabelSeperator1
            // 
            this.toolStripStatusLabelSeperator1.Name = "toolStripStatusLabelSeperator1";
            this.toolStripStatusLabelSeperator1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabelSeperator1.Text = "|";
            // 
            // toolStripStatusQueued
            // 
            this.toolStripStatusQueued.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusQueued.Name = "toolStripStatusQueued";
            this.toolStripStatusQueued.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusQueued.Text = "Queues Threads";
            // 
            // toolStripStatusQueuesThreadsCount
            // 
            this.toolStripStatusQueuesThreadsCount.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusQueuesThreadsCount.Name = "toolStripStatusQueuesThreadsCount";
            this.toolStripStatusQueuesThreadsCount.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusQueuesThreadsCount.Text = "0";
            // 
            // toolStripStatusSeperator2
            // 
            this.toolStripStatusSeperator2.Name = "toolStripStatusSeperator2";
            this.toolStripStatusSeperator2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusSeperator2.Text = "|";
            // 
            // toolStripStatusExport
            // 
            this.toolStripStatusExport.Name = "toolStripStatusExport";
            this.toolStripStatusExport.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusBarSyncTimer
            // 
            this.StatusBarSyncTimer.Enabled = true;
            this.StatusBarSyncTimer.Tick += new System.EventHandler(this.StatusBarSyncTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(457, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Mode";
            // 
            // timerSpider
            // 
            this.timerSpider.Interval = 60000;
            this.timerSpider.Tick += new System.EventHandler(this.timerSpider_Tick);
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(723, 11);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 23);
            this.btnTimer.TabIndex = 24;
            this.btnTimer.Text = "Timer Start";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Visible = false;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // timerPingRate
            // 
            this.timerPingRate.Enabled = true;
            this.timerPingRate.Interval = 60000;
            this.timerPingRate.Tick += new System.EventHandler(this.timerPingRate_Tick);
            // 
            // chkAdiIncognito
            // 
            this.chkAdiIncognito.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAdiIncognito.AutoSize = true;
            this.chkAdiIncognito.Location = new System.Drawing.Point(637, 15);
            this.chkAdiIncognito.Name = "chkAdiIncognito";
            this.chkAdiIncognito.Size = new System.Drawing.Size(99, 17);
            this.chkAdiIncognito.TabIndex = 25;
            this.chkAdiIncognito.Text = "Incognito mode";
            this.chkAdiIncognito.UseVisualStyleBackColor = true;
            // 
            // timerTasks
            // 
            this.timerTasks.Interval = 5000;
            this.timerTasks.Tick += new System.EventHandler(this.timerTasks_Tick);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageTasks);
            this.tabControlMain.Controls.Add(this.tabPageAdiGlobal);
            this.tabControlMain.Controls.Add(this.tabPageSecLock);
            this.tabControlMain.Controls.Add(this.tabPageSecLockCSV);
            this.tabControlMain.Controls.Add(this.tabPageTri);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Location = new System.Drawing.Point(0, 1);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(961, 418);
            this.tabControlMain.TabIndex = 27;
            this.tabControlMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlMain_Selected);
            // 
            // tabPageTasks
            // 
            this.tabPageTasks.Controls.Add(this.listViewThreads);
            this.tabPageTasks.Controls.Add(this.btnTimer);
            this.tabPageTasks.Controls.Add(this.btnStart);
            this.tabPageTasks.Controls.Add(this.toolBarTasks);
            this.tabPageTasks.Location = new System.Drawing.Point(4, 22);
            this.tabPageTasks.Name = "tabPageTasks";
            this.tabPageTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTasks.Size = new System.Drawing.Size(953, 392);
            this.tabPageTasks.TabIndex = 1;
            this.tabPageTasks.Text = "Tasks";
            this.tabPageTasks.UseVisualStyleBackColor = true;
            // 
            // tabPageAdiGlobal
            // 
            this.tabPageAdiGlobal.Controls.Add(this.cmbAdiMode);
            this.tabPageAdiGlobal.Controls.Add(this.tabAdiGlobal);
            this.tabPageAdiGlobal.Controls.Add(this.chkAdiIncognito);
            this.tabPageAdiGlobal.Controls.Add(this.label1);
            this.tabPageAdiGlobal.Controls.Add(this.chkAdiDownloadImage);
            this.tabPageAdiGlobal.Controls.Add(this.toolBarAdiGlobal);
            this.tabPageAdiGlobal.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdiGlobal.Name = "tabPageAdiGlobal";
            this.tabPageAdiGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdiGlobal.Size = new System.Drawing.Size(953, 392);
            this.tabPageAdiGlobal.TabIndex = 0;
            this.tabPageAdiGlobal.Text = "AdiGlobal.us";
            this.tabPageAdiGlobal.UseVisualStyleBackColor = true;
            // 
            // cmbAdiMode
            // 
            this.cmbAdiMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdiMode.FormattingEnabled = true;
            this.cmbAdiMode.Items.AddRange(new object[] {
            "Crawl",
            "Update",
            "Clearance Zone",
            "Hot Deals",
            "Online Specials",
            "Sale Center",
            "In Stock"});
            this.cmbAdiMode.Location = new System.Drawing.Point(497, 13);
            this.cmbAdiMode.Name = "cmbAdiMode";
            this.cmbAdiMode.Size = new System.Drawing.Size(121, 21);
            this.cmbAdiMode.TabIndex = 27;
            this.cmbAdiMode.SelectedIndexChanged += new System.EventHandler(this.cmbAdiMode_SelectedIndexChanged);
            // 
            // toolBarAdiGlobal
            // 
            this.toolBarAdiGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBarAdiGlobal.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarAdiGlobal.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonAdiPriority,
            this.toolBarButtonAdiReload,
            this.toolBarButtonAdiScheduler});
            this.toolBarAdiGlobal.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBarAdiGlobal.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBarAdiGlobal.DropDownArrows = true;
            this.toolBarAdiGlobal.ImageList = this.imageList2;
            this.toolBarAdiGlobal.Location = new System.Drawing.Point(6, 6);
            this.toolBarAdiGlobal.Name = "toolBarAdiGlobal";
            this.toolBarAdiGlobal.ShowToolTips = true;
            this.toolBarAdiGlobal.Size = new System.Drawing.Size(900, 28);
            this.toolBarAdiGlobal.TabIndex = 28;
            this.toolBarAdiGlobal.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarAdiGlobal_ButtonClick);
            // 
            // toolBarButtonAdiPriority
            // 
            this.toolBarButtonAdiPriority.ImageIndex = 5;
            this.toolBarButtonAdiPriority.Name = "toolBarButtonAdiPriority";
            this.toolBarButtonAdiPriority.ToolTipText = "Priority";
            // 
            // toolBarButtonAdiReload
            // 
            this.toolBarButtonAdiReload.ImageIndex = 9;
            this.toolBarButtonAdiReload.Name = "toolBarButtonAdiReload";
            this.toolBarButtonAdiReload.ToolTipText = "Reload";
            // 
            // toolBarButtonAdiScheduler
            // 
            this.toolBarButtonAdiScheduler.ImageIndex = 10;
            this.toolBarButtonAdiScheduler.Name = "toolBarButtonAdiScheduler";
            this.toolBarButtonAdiScheduler.ToolTipText = "Scheduler";
            // 
            // tabPageSecLock
            // 
            this.tabPageSecLock.Controls.Add(this.chkSeclockIncognito);
            this.tabPageSecLock.Controls.Add(this.chkSeclockDownloadImages);
            this.tabPageSecLock.Controls.Add(this.toolBarSecLock);
            this.tabPageSecLock.Controls.Add(this.tabControlSecLock);
            this.tabPageSecLock.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecLock.Name = "tabPageSecLock";
            this.tabPageSecLock.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSecLock.Size = new System.Drawing.Size(953, 392);
            this.tabPageSecLock.TabIndex = 3;
            this.tabPageSecLock.Text = "SecLock.com";
            this.tabPageSecLock.UseVisualStyleBackColor = true;
            // 
            // chkSeclockIncognito
            // 
            this.chkSeclockIncognito.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSeclockIncognito.AutoSize = true;
            this.chkSeclockIncognito.Location = new System.Drawing.Point(641, 17);
            this.chkSeclockIncognito.Name = "chkSeclockIncognito";
            this.chkSeclockIncognito.Size = new System.Drawing.Size(99, 17);
            this.chkSeclockIncognito.TabIndex = 31;
            this.chkSeclockIncognito.Text = "Incognito mode";
            this.chkSeclockIncognito.UseVisualStyleBackColor = true;
            this.chkSeclockIncognito.Visible = false;
            // 
            // chkSeclockDownloadImages
            // 
            this.chkSeclockDownloadImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSeclockDownloadImages.AutoSize = true;
            this.chkSeclockDownloadImages.Location = new System.Drawing.Point(759, 17);
            this.chkSeclockDownloadImages.Name = "chkSeclockDownloadImages";
            this.chkSeclockDownloadImages.Size = new System.Drawing.Size(151, 17);
            this.chkSeclockDownloadImages.TabIndex = 30;
            this.chkSeclockDownloadImages.Text = "Download Product Images";
            this.chkSeclockDownloadImages.UseVisualStyleBackColor = true;
            // 
            // toolBarSecLock
            // 
            this.toolBarSecLock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBarSecLock.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarSecLock.AutoSize = false;
            this.toolBarSecLock.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonSecLockRefresh});
            this.toolBarSecLock.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBarSecLock.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBarSecLock.DropDownArrows = true;
            this.toolBarSecLock.ImageList = this.imageList2;
            this.toolBarSecLock.Location = new System.Drawing.Point(10, 6);
            this.toolBarSecLock.Name = "toolBarSecLock";
            this.toolBarSecLock.ShowToolTips = true;
            this.toolBarSecLock.Size = new System.Drawing.Size(943, 28);
            this.toolBarSecLock.TabIndex = 29;
            this.toolBarSecLock.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarSecLock_ButtonClick);
            // 
            // toolBarButtonSecLockRefresh
            // 
            this.toolBarButtonSecLockRefresh.ImageIndex = 9;
            this.toolBarButtonSecLockRefresh.Name = "toolBarButtonSecLockRefresh";
            this.toolBarButtonSecLockRefresh.ToolTipText = "Refresh";
            // 
            // tabControlSecLock
            // 
            this.tabControlSecLock.Controls.Add(this.tabPageSeclockManufacturers);
            this.tabControlSecLock.Controls.Add(this.tabPageSecLockCategories);
            this.tabControlSecLock.Controls.Add(this.tabPageSecLockProducts);
            this.tabControlSecLock.Location = new System.Drawing.Point(6, 40);
            this.tabControlSecLock.Name = "tabControlSecLock";
            this.tabControlSecLock.SelectedIndex = 0;
            this.tabControlSecLock.Size = new System.Drawing.Size(939, 346);
            this.tabControlSecLock.TabIndex = 1;
            // 
            // tabPageSeclockManufacturers
            // 
            this.tabPageSeclockManufacturers.Controls.Add(this.chkSecLockManufanufacturersSelectAll);
            this.tabPageSeclockManufacturers.Controls.Add(this.tvSecLockManufacturers);
            this.tabPageSeclockManufacturers.Location = new System.Drawing.Point(4, 22);
            this.tabPageSeclockManufacturers.Name = "tabPageSeclockManufacturers";
            this.tabPageSeclockManufacturers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSeclockManufacturers.Size = new System.Drawing.Size(931, 320);
            this.tabPageSeclockManufacturers.TabIndex = 0;
            this.tabPageSeclockManufacturers.Text = "Manufacturers";
            this.tabPageSeclockManufacturers.UseVisualStyleBackColor = true;
            // 
            // chkSecLockManufanufacturersSelectAll
            // 
            this.chkSecLockManufanufacturersSelectAll.AutoSize = true;
            this.chkSecLockManufanufacturersSelectAll.Location = new System.Drawing.Point(830, 15);
            this.chkSecLockManufanufacturersSelectAll.Name = "chkSecLockManufanufacturersSelectAll";
            this.chkSecLockManufanufacturersSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSecLockManufanufacturersSelectAll.TabIndex = 1;
            this.chkSecLockManufanufacturersSelectAll.Text = "Select All";
            this.chkSecLockManufanufacturersSelectAll.UseVisualStyleBackColor = true;
            this.chkSecLockManufanufacturersSelectAll.CheckedChanged += new System.EventHandler(this.chkSecLockManufanufacturersSelectAll_CheckedChanged);
            // 
            // tvSecLockManufacturers
            // 
            this.tvSecLockManufacturers.CheckBoxes = true;
            this.tvSecLockManufacturers.Location = new System.Drawing.Point(6, 6);
            this.tvSecLockManufacturers.Name = "tvSecLockManufacturers";
            this.tvSecLockManufacturers.Size = new System.Drawing.Size(919, 308);
            this.tvSecLockManufacturers.TabIndex = 0;
            this.tvSecLockManufacturers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSecLockManufacturers_AfterCheck);
            // 
            // tabPageSecLockCategories
            // 
            this.tabPageSecLockCategories.Controls.Add(this.chkSecLockCategoriessSelectAll);
            this.tabPageSecLockCategories.Controls.Add(this.tvSecLockCategories);
            this.tabPageSecLockCategories.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecLockCategories.Name = "tabPageSecLockCategories";
            this.tabPageSecLockCategories.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSecLockCategories.Size = new System.Drawing.Size(931, 320);
            this.tabPageSecLockCategories.TabIndex = 1;
            this.tabPageSecLockCategories.Text = "Categories";
            this.tabPageSecLockCategories.UseVisualStyleBackColor = true;
            // 
            // chkSecLockCategoriessSelectAll
            // 
            this.chkSecLockCategoriessSelectAll.AutoSize = true;
            this.chkSecLockCategoriessSelectAll.Location = new System.Drawing.Point(830, 15);
            this.chkSecLockCategoriessSelectAll.Name = "chkSecLockCategoriessSelectAll";
            this.chkSecLockCategoriessSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSecLockCategoriessSelectAll.TabIndex = 2;
            this.chkSecLockCategoriessSelectAll.Text = "Select All";
            this.chkSecLockCategoriessSelectAll.UseVisualStyleBackColor = true;
            this.chkSecLockCategoriessSelectAll.CheckedChanged += new System.EventHandler(this.chkSecLockCategoriessSelectAll_CheckedChanged);
            // 
            // tvSecLockCategories
            // 
            this.tvSecLockCategories.CheckBoxes = true;
            this.tvSecLockCategories.Location = new System.Drawing.Point(7, 7);
            this.tvSecLockCategories.Name = "tvSecLockCategories";
            this.tvSecLockCategories.Size = new System.Drawing.Size(918, 307);
            this.tvSecLockCategories.TabIndex = 0;
            this.tvSecLockCategories.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSecLockCategories_AfterCheck);
            // 
            // tabPageSecLockProducts
            // 
            this.tabPageSecLockProducts.Controls.Add(this.chkSecLockProductsSelectAll);
            this.tabPageSecLockProducts.Controls.Add(this.tvsecLockProducts);
            this.tabPageSecLockProducts.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecLockProducts.Name = "tabPageSecLockProducts";
            this.tabPageSecLockProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSecLockProducts.Size = new System.Drawing.Size(931, 320);
            this.tabPageSecLockProducts.TabIndex = 2;
            this.tabPageSecLockProducts.Text = "Products";
            this.tabPageSecLockProducts.UseVisualStyleBackColor = true;
            // 
            // chkSecLockProductsSelectAll
            // 
            this.chkSecLockProductsSelectAll.AutoSize = true;
            this.chkSecLockProductsSelectAll.Location = new System.Drawing.Point(830, 15);
            this.chkSecLockProductsSelectAll.Name = "chkSecLockProductsSelectAll";
            this.chkSecLockProductsSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSecLockProductsSelectAll.TabIndex = 2;
            this.chkSecLockProductsSelectAll.Text = "Select All";
            this.chkSecLockProductsSelectAll.UseVisualStyleBackColor = true;
            this.chkSecLockProductsSelectAll.CheckedChanged += new System.EventHandler(this.chkSecLockProductsSelectAll_CheckedChanged);
            // 
            // tvsecLockProducts
            // 
            this.tvsecLockProducts.CheckBoxes = true;
            this.tvsecLockProducts.Location = new System.Drawing.Point(6, 7);
            this.tvsecLockProducts.Name = "tvsecLockProducts";
            this.tvsecLockProducts.Size = new System.Drawing.Size(918, 307);
            this.tvsecLockProducts.TabIndex = 1;
            this.tvsecLockProducts.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvsecLockProducts_AfterCheck);
            // 
            // tabPageSecLockCSV
            // 
            this.tabPageSecLockCSV.Controls.Add(this.btnSecLockCSVStart);
            this.tabPageSecLockCSV.Controls.Add(this.chkSeclockCSVDownloadImages);
            this.tabPageSecLockCSV.Controls.Add(this.chkSeclockCSVIncognito);
            this.tabPageSecLockCSV.Controls.Add(this.chkSecLockCSVProductSelectAll);
            this.tabPageSecLockCSV.Controls.Add(this.tvSecLockCSVProduct);
            this.tabPageSecLockCSV.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecLockCSV.Name = "tabPageSecLockCSV";
            this.tabPageSecLockCSV.Size = new System.Drawing.Size(953, 392);
            this.tabPageSecLockCSV.TabIndex = 4;
            this.tabPageSecLockCSV.Text = "SecLock CSV";
            this.tabPageSecLockCSV.UseVisualStyleBackColor = true;
            // 
            // btnSecLockCSVStart
            // 
            this.btnSecLockCSVStart.ForeColor = System.Drawing.Color.Red;
            this.btnSecLockCSVStart.Location = new System.Drawing.Point(463, 10);
            this.btnSecLockCSVStart.Name = "btnSecLockCSVStart";
            this.btnSecLockCSVStart.Size = new System.Drawing.Size(75, 23);
            this.btnSecLockCSVStart.TabIndex = 4;
            this.btnSecLockCSVStart.Text = "Start";
            this.btnSecLockCSVStart.UseVisualStyleBackColor = true;
            this.btnSecLockCSVStart.Click += new System.EventHandler(this.btnSecLockCSVStart_Click);
            // 
            // chkSeclockCSVDownloadImages
            // 
            this.chkSeclockCSVDownloadImages.AutoSize = true;
            this.chkSeclockCSVDownloadImages.Location = new System.Drawing.Point(784, 8);
            this.chkSeclockCSVDownloadImages.Name = "chkSeclockCSVDownloadImages";
            this.chkSeclockCSVDownloadImages.Size = new System.Drawing.Size(151, 17);
            this.chkSeclockCSVDownloadImages.TabIndex = 3;
            this.chkSeclockCSVDownloadImages.Text = "Download Product Images";
            this.chkSeclockCSVDownloadImages.UseVisualStyleBackColor = true;
            // 
            // chkSeclockCSVIncognito
            // 
            this.chkSeclockCSVIncognito.AutoSize = true;
            this.chkSeclockCSVIncognito.Location = new System.Drawing.Point(662, 8);
            this.chkSeclockCSVIncognito.Name = "chkSeclockCSVIncognito";
            this.chkSeclockCSVIncognito.Size = new System.Drawing.Size(99, 17);
            this.chkSeclockCSVIncognito.TabIndex = 2;
            this.chkSeclockCSVIncognito.Text = "Incognito mode";
            this.chkSeclockCSVIncognito.UseVisualStyleBackColor = true;
            // 
            // chkSecLockCSVProductSelectAll
            // 
            this.chkSecLockCSVProductSelectAll.AutoSize = true;
            this.chkSecLockCSVProductSelectAll.Location = new System.Drawing.Point(865, 55);
            this.chkSecLockCSVProductSelectAll.Name = "chkSecLockCSVProductSelectAll";
            this.chkSecLockCSVProductSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSecLockCSVProductSelectAll.TabIndex = 1;
            this.chkSecLockCSVProductSelectAll.Text = "Select All";
            this.chkSecLockCSVProductSelectAll.UseVisualStyleBackColor = true;
            this.chkSecLockCSVProductSelectAll.CheckedChanged += new System.EventHandler(this.chkSecLockCSVProductSelectAll_CheckedChanged);
            // 
            // tvSecLockCSVProduct
            // 
            this.tvSecLockCSVProduct.CheckBoxes = true;
            this.tvSecLockCSVProduct.Location = new System.Drawing.Point(3, 39);
            this.tvSecLockCSVProduct.Name = "tvSecLockCSVProduct";
            this.tvSecLockCSVProduct.Size = new System.Drawing.Size(947, 350);
            this.tvSecLockCSVProduct.TabIndex = 0;
            this.tvSecLockCSVProduct.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvsecLockCSVProducts_AfterCheck);
            // 
            // tabPageTri
            // 
            this.tabPageTri.Controls.Add(this.toolBarButtonTriReload);
            this.tabPageTri.Controls.Add(this.label48);
            this.tabPageTri.Controls.Add(this.cmbTriMode);
            this.tabPageTri.Controls.Add(this.chkTRIownloadImages);
            this.tabPageTri.Controls.Add(this.chkTriIncognito);
            this.tabPageTri.Controls.Add(this.tabControlTri);
            this.tabPageTri.Location = new System.Drawing.Point(4, 22);
            this.tabPageTri.Name = "tabPageTri";
            this.tabPageTri.Size = new System.Drawing.Size(953, 392);
            this.tabPageTri.TabIndex = 5;
            this.tabPageTri.Text = "Tri-ed.com";
            this.tabPageTri.UseVisualStyleBackColor = true;
            // 
            // toolBarButtonTriReload
            // 
            this.toolBarButtonTriReload.Location = new System.Drawing.Point(3, 5);
            this.toolBarButtonTriReload.Name = "toolBarButtonTriReload";
            this.toolBarButtonTriReload.Size = new System.Drawing.Size(75, 23);
            this.toolBarButtonTriReload.TabIndex = 6;
            this.toolBarButtonTriReload.Text = "Reload All";
            this.toolBarButtonTriReload.UseVisualStyleBackColor = true;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(449, 11);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(34, 13);
            this.label48.TabIndex = 5;
            this.label48.Text = "Mode";
            // 
            // cmbTriMode
            // 
            this.cmbTriMode.FormattingEnabled = true;
            this.cmbTriMode.Items.AddRange(new object[] {
            "Crawl",
            "Update"});
            this.cmbTriMode.Location = new System.Drawing.Point(503, 7);
            this.cmbTriMode.Name = "cmbTriMode";
            this.cmbTriMode.Size = new System.Drawing.Size(121, 21);
            this.cmbTriMode.TabIndex = 4;
            // 
            // chkTRIownloadImages
            // 
            this.chkTRIownloadImages.AutoSize = true;
            this.chkTRIownloadImages.Location = new System.Drawing.Point(778, 9);
            this.chkTRIownloadImages.Name = "chkTRIownloadImages";
            this.chkTRIownloadImages.Size = new System.Drawing.Size(151, 17);
            this.chkTRIownloadImages.TabIndex = 3;
            this.chkTRIownloadImages.Text = "Download Product Images";
            this.chkTRIownloadImages.UseVisualStyleBackColor = true;
            // 
            // chkTriIncognito
            // 
            this.chkTriIncognito.AutoSize = true;
            this.chkTriIncognito.Location = new System.Drawing.Point(662, 9);
            this.chkTriIncognito.Name = "chkTriIncognito";
            this.chkTriIncognito.Size = new System.Drawing.Size(99, 17);
            this.chkTriIncognito.TabIndex = 2;
            this.chkTriIncognito.Text = "Incognito mode";
            this.chkTriIncognito.UseVisualStyleBackColor = true;
            // 
            // tabControlTri
            // 
            this.tabControlTri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTri.Controls.Add(this.tabTriManufacturers);
            this.tabControlTri.Controls.Add(this.tabTriMainCategory);
            this.tabControlTri.Controls.Add(this.tabTriProduct);
            this.tabControlTri.Enabled = false;
            this.tabControlTri.Location = new System.Drawing.Point(3, 32);
            this.tabControlTri.Name = "tabControlTri";
            this.tabControlTri.SelectedIndex = 0;
            this.tabControlTri.Size = new System.Drawing.Size(947, 357);
            this.tabControlTri.TabIndex = 0;
            // 
            // tabTriManufacturers
            // 
            this.tabTriManufacturers.Controls.Add(this.chkTriManufanufacturersSelectAll);
            this.tabTriManufacturers.Controls.Add(this.tvTriManufacturers);
            this.tabTriManufacturers.Location = new System.Drawing.Point(4, 22);
            this.tabTriManufacturers.Name = "tabTriManufacturers";
            this.tabTriManufacturers.Padding = new System.Windows.Forms.Padding(3);
            this.tabTriManufacturers.Size = new System.Drawing.Size(939, 331);
            this.tabTriManufacturers.TabIndex = 1;
            this.tabTriManufacturers.Text = "Manufacture";
            this.tabTriManufacturers.UseVisualStyleBackColor = true;
            // 
            // chkTriManufanufacturersSelectAll
            // 
            this.chkTriManufanufacturersSelectAll.AutoSize = true;
            this.chkTriManufanufacturersSelectAll.Location = new System.Drawing.Point(846, 18);
            this.chkTriManufanufacturersSelectAll.Name = "chkTriManufanufacturersSelectAll";
            this.chkTriManufanufacturersSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkTriManufanufacturersSelectAll.TabIndex = 1;
            this.chkTriManufanufacturersSelectAll.Text = "Select All";
            this.chkTriManufanufacturersSelectAll.UseVisualStyleBackColor = true;
            // 
            // tvTriManufacturers
            // 
            this.tvTriManufacturers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvTriManufacturers.Location = new System.Drawing.Point(6, 6);
            this.tvTriManufacturers.Name = "tvTriManufacturers";
            this.tvTriManufacturers.Size = new System.Drawing.Size(919, 308);
            this.tvTriManufacturers.TabIndex = 0;
            // 
            // tabTriMainCategory
            // 
            this.tabTriMainCategory.Location = new System.Drawing.Point(4, 22);
            this.tabTriMainCategory.Name = "tabTriMainCategory";
            this.tabTriMainCategory.Size = new System.Drawing.Size(939, 331);
            this.tabTriMainCategory.TabIndex = 2;
            this.tabTriMainCategory.Text = "Category";
            this.tabTriMainCategory.UseVisualStyleBackColor = true;
            // 
            // tabTriProduct
            // 
            this.tabTriProduct.Location = new System.Drawing.Point(4, 22);
            this.tabTriProduct.Name = "tabTriProduct";
            this.tabTriProduct.Size = new System.Drawing.Size(939, 331);
            this.tabTriProduct.TabIndex = 5;
            this.tabTriProduct.Text = "Products";
            this.tabTriProduct.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.btnSettingsSave);
            this.tabPageSettings.Controls.Add(this.btnSettingsCancel);
            this.tabPageSettings.Controls.Add(this.tabControl1);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(953, 392);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.Location = new System.Drawing.Point(785, 342);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsSave.TabIndex = 23;
            this.btnSettingsSave.Text = "Save";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            this.btnSettingsSave.Click += new System.EventHandler(this.btnSettingsSave_Click);
            // 
            // btnSettingsCancel
            // 
            this.btnSettingsCancel.Location = new System.Drawing.Point(866, 342);
            this.btnSettingsCancel.Name = "btnSettingsCancel";
            this.btnSettingsCancel.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsCancel.TabIndex = 24;
            this.btnSettingsCancel.Text = "Cancel";
            this.btnSettingsCancel.UseVisualStyleBackColor = true;
            this.btnSettingsCancel.Click += new System.EventHandler(this.btnSettingsCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabMail);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(937, 330);
            this.tabControl1.TabIndex = 22;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.btnCleanDb);
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.txtPing);
            this.tabGeneral.Controls.Add(this.label29);
            this.tabGeneral.Controls.Add(this.groupBoxDatabase);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.Stockalert);
            this.tabGeneral.Controls.Add(this.txtDateFormat);
            this.tabGeneral.Controls.Add(this.label21);
            this.tabGeneral.Controls.Add(this.label20);
            this.tabGeneral.Controls.Add(this.txtRetryCount);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.txtThreadsCount);
            this.tabGeneral.Controls.Add(this.chkCaching);
            this.tabGeneral.Controls.Add(this.label10);
            this.tabGeneral.Controls.Add(this.txtCacheDuration);
            this.tabGeneral.Controls.Add(this.label11);
            this.tabGeneral.Controls.Add(this.txtErrorFileName);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.chkErrorLog);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(929, 304);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // btnCleanDb
            // 
            this.btnCleanDb.BackColor = System.Drawing.Color.Red;
            this.btnCleanDb.ForeColor = System.Drawing.Color.Black;
            this.btnCleanDb.Location = new System.Drawing.Point(489, 240);
            this.btnCleanDb.Name = "btnCleanDb";
            this.btnCleanDb.Size = new System.Drawing.Size(413, 23);
            this.btnCleanDb.TabIndex = 27;
            this.btnCleanDb.Text = "Clean Internal Data";
            this.btnCleanDb.UseVisualStyleBackColor = false;
            this.btnCleanDb.Click += new System.EventHandler(this.btnCleanDb_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDocFolderBrowse);
            this.groupBox2.Controls.Add(this.label44);
            this.groupBox2.Controls.Add(this.txtDocFolder);
            this.groupBox2.Controls.Add(this.btnUpdateImageBrowse);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.btnCrawlImageBrowse);
            this.groupBox2.Controls.Add(this.txtCrawlImageFolder);
            this.groupBox2.Controls.Add(this.label42);
            this.groupBox2.Controls.Add(this.txtUpdateImageFolder);
            this.groupBox2.Location = new System.Drawing.Point(489, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 103);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download Folders";
            // 
            // btnDocFolderBrowse
            // 
            this.btnDocFolderBrowse.Location = new System.Drawing.Point(329, 70);
            this.btnDocFolderBrowse.Name = "btnDocFolderBrowse";
            this.btnDocFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnDocFolderBrowse.TabIndex = 31;
            this.btnDocFolderBrowse.Text = "Browse";
            this.btnDocFolderBrowse.UseVisualStyleBackColor = true;
            this.btnDocFolderBrowse.Click += new System.EventHandler(this.btnDocFolderBrowse_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(20, 75);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(93, 13);
            this.label44.TabIndex = 30;
            this.label44.Text = "Documents Folder";
            // 
            // txtDocFolder
            // 
            this.txtDocFolder.Location = new System.Drawing.Point(117, 72);
            this.txtDocFolder.Name = "txtDocFolder";
            this.txtDocFolder.Size = new System.Drawing.Size(202, 20);
            this.txtDocFolder.TabIndex = 29;
            // 
            // btnUpdateImageBrowse
            // 
            this.btnUpdateImageBrowse.Location = new System.Drawing.Point(329, 43);
            this.btnUpdateImageBrowse.Name = "btnUpdateImageBrowse";
            this.btnUpdateImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateImageBrowse.TabIndex = 28;
            this.btnUpdateImageBrowse.Text = "Browse";
            this.btnUpdateImageBrowse.UseVisualStyleBackColor = true;
            this.btnUpdateImageBrowse.Click += new System.EventHandler(this.btnUpdateImageBrowse_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(15, 22);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(97, 13);
            this.label32.TabIndex = 7;
            this.label32.Text = "Crawl Image Folder";
            // 
            // btnCrawlImageBrowse
            // 
            this.btnCrawlImageBrowse.Location = new System.Drawing.Point(329, 17);
            this.btnCrawlImageBrowse.Name = "btnCrawlImageBrowse";
            this.btnCrawlImageBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnCrawlImageBrowse.TabIndex = 8;
            this.btnCrawlImageBrowse.Text = "Browse";
            this.btnCrawlImageBrowse.UseVisualStyleBackColor = true;
            this.btnCrawlImageBrowse.Click += new System.EventHandler(this.btnCrawlImageBrowse_Click);
            // 
            // txtCrawlImageFolder
            // 
            this.txtCrawlImageFolder.Location = new System.Drawing.Point(117, 19);
            this.txtCrawlImageFolder.Name = "txtCrawlImageFolder";
            this.txtCrawlImageFolder.Size = new System.Drawing.Size(202, 20);
            this.txtCrawlImageFolder.TabIndex = 6;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(8, 48);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(106, 13);
            this.label42.TabIndex = 27;
            this.label42.Text = "Update Image Folder";
            // 
            // txtUpdateImageFolder
            // 
            this.txtUpdateImageFolder.Location = new System.Drawing.Point(117, 45);
            this.txtUpdateImageFolder.Name = "txtUpdateImageFolder";
            this.txtUpdateImageFolder.Size = new System.Drawing.Size(202, 20);
            this.txtUpdateImageFolder.TabIndex = 26;
            // 
            // txtPing
            // 
            this.txtPing.Location = new System.Drawing.Point(361, 6);
            this.txtPing.Name = "txtPing";
            this.txtPing.Size = new System.Drawing.Size(56, 20);
            this.txtPing.TabIndex = 25;
            this.txtPing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericFields_KeyPress);
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
            this.groupBoxDatabase.Controls.Add(this.btnGenerateAdiUpdateSchema);
            this.groupBoxDatabase.Controls.Add(this.btnGenerateAdiCrawlSchema);
            this.groupBoxDatabase.Controls.Add(this.btnBrowseUpdate);
            this.groupBoxDatabase.Controls.Add(this.btnBrowseCrawl);
            this.groupBoxDatabase.Controls.Add(this.label28);
            this.groupBoxDatabase.Controls.Add(this.label27);
            this.groupBoxDatabase.Controls.Add(this.txtUpdateDB);
            this.groupBoxDatabase.Controls.Add(this.txtcrawlDB);
            this.groupBoxDatabase.Location = new System.Drawing.Point(489, 9);
            this.groupBoxDatabase.Name = "groupBoxDatabase";
            this.groupBoxDatabase.Size = new System.Drawing.Size(413, 110);
            this.groupBoxDatabase.TabIndex = 23;
            this.groupBoxDatabase.TabStop = false;
            this.groupBoxDatabase.Text = "Database Path";
            // 
            // btnGenerateAdiUpdateSchema
            // 
            this.btnGenerateAdiUpdateSchema.Location = new System.Drawing.Point(220, 74);
            this.btnGenerateAdiUpdateSchema.Name = "btnGenerateAdiUpdateSchema";
            this.btnGenerateAdiUpdateSchema.Size = new System.Drawing.Size(162, 23);
            this.btnGenerateAdiUpdateSchema.TabIndex = 7;
            this.btnGenerateAdiUpdateSchema.Text = "Generate Update Tables";
            this.btnGenerateAdiUpdateSchema.UseVisualStyleBackColor = true;
            this.btnGenerateAdiUpdateSchema.Click += new System.EventHandler(this.btnGenerateAdiUpdateSchema_Click);
            // 
            // btnGenerateAdiCrawlSchema
            // 
            this.btnGenerateAdiCrawlSchema.Location = new System.Drawing.Point(25, 74);
            this.btnGenerateAdiCrawlSchema.Name = "btnGenerateAdiCrawlSchema";
            this.btnGenerateAdiCrawlSchema.Size = new System.Drawing.Size(162, 23);
            this.btnGenerateAdiCrawlSchema.TabIndex = 6;
            this.btnGenerateAdiCrawlSchema.Text = "Generate Crawl Tables";
            this.btnGenerateAdiCrawlSchema.UseVisualStyleBackColor = true;
            this.btnGenerateAdiCrawlSchema.Click += new System.EventHandler(this.btnGenerateAdiCrawlSchema_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(10, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 44);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spider Scheduler";
            this.groupBox1.Visible = false;
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
            this.Stockalert.Location = new System.Drawing.Point(10, 126);
            this.Stockalert.Name = "Stockalert";
            this.Stockalert.Size = new System.Drawing.Size(413, 78);
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
            this.lstSDay.Size = new System.Drawing.Size(62, 43);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Concurrent Threads";
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
            // txtErrorFileName
            // 
            this.txtErrorFileName.Location = new System.Drawing.Point(203, 65);
            this.txtErrorFileName.Name = "txtErrorFileName";
            this.txtErrorFileName.Size = new System.Drawing.Size(220, 20);
            this.txtErrorFileName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "File Name";
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
            // tabMail
            // 
            this.tabMail.Controls.Add(this.chkSendErrorMail);
            this.tabMail.Controls.Add(this.label15);
            this.tabMail.Controls.Add(this.txtMailTo);
            this.tabMail.Controls.Add(this.txtMailFrom);
            this.tabMail.Controls.Add(this.label4);
            this.tabMail.Controls.Add(this.label14);
            this.tabMail.Controls.Add(this.label5);
            this.tabMail.Controls.Add(this.txtMailSubject);
            this.tabMail.Controls.Add(this.label6);
            this.tabMail.Controls.Add(this.txtSmtpServer);
            this.tabMail.Controls.Add(this.label7);
            this.tabMail.Controls.Add(this.txtSmtpPort);
            this.tabMail.Controls.Add(this.chkSmtpSSL);
            this.tabMail.Controls.Add(this.label8);
            this.tabMail.Controls.Add(this.txtSmtpUserName);
            this.tabMail.Controls.Add(this.label9);
            this.tabMail.Controls.Add(this.txtSmtpPassword);
            this.tabMail.Location = new System.Drawing.Point(4, 22);
            this.tabMail.Name = "tabMail";
            this.tabMail.Padding = new System.Windows.Forms.Padding(3);
            this.tabMail.Size = new System.Drawing.Size(929, 304);
            this.tabMail.TabIndex = 1;
            this.tabMail.Text = "Email";
            this.tabMail.UseVisualStyleBackColor = true;
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
            this.chkSendErrorMail.CheckStateChanged += new System.EventHandler(this.chkSendErrorMail_CheckedChanged);
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
            // txtMailTo
            // 
            this.txtMailTo.Location = new System.Drawing.Point(126, 30);
            this.txtMailTo.Multiline = true;
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Size = new System.Drawing.Size(293, 58);
            this.txtMailTo.TabIndex = 10;
            // 
            // txtMailFrom
            // 
            this.txtMailFrom.Location = new System.Drawing.Point(126, 113);
            this.txtMailFrom.Name = "txtMailFrom";
            this.txtMailFrom.Size = new System.Drawing.Size(293, 20);
            this.txtMailFrom.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Send mail to";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Seperate by comma(,)";
            // 
            // txtMailSubject
            // 
            this.txtMailSubject.Location = new System.Drawing.Point(126, 139);
            this.txtMailSubject.Name = "txtMailSubject";
            this.txtMailSubject.Size = new System.Drawing.Size(293, 20);
            this.txtMailSubject.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "SMTP Server";
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Location = new System.Drawing.Point(126, 167);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(293, 20);
            this.txtSmtpServer.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Server Port";
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Location = new System.Drawing.Point(126, 193);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(57, 20);
            this.txtSmtpPort.TabIndex = 13;
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Username";
            // 
            // txtSmtpUserName
            // 
            this.txtSmtpUserName.Location = new System.Drawing.Point(126, 220);
            this.txtSmtpUserName.Name = "txtSmtpUserName";
            this.txtSmtpUserName.Size = new System.Drawing.Size(293, 20);
            this.txtSmtpUserName.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Password";
            // 
            // txtSmtpPassword
            // 
            this.txtSmtpPassword.Location = new System.Drawing.Point(126, 247);
            this.txtSmtpPassword.Name = "txtSmtpPassword";
            this.txtSmtpPassword.PasswordChar = '●';
            this.txtSmtpPassword.Size = new System.Drawing.Size(293, 20);
            this.txtSmtpPassword.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtAdiGlobalImagePrefix);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.txtAdiGlobalLoginPage);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.txtAdiGlobalCatagoryUpdateInterval);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtAdiGlobalProductUpdateInterval);
            this.tabPage1.Controls.Add(this.txtAdiGlobalPassword);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.txtAdiGlobalUsername);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(929, 304);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Adi Global";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtAdiGlobalImagePrefix
            // 
            this.txtAdiGlobalImagePrefix.Location = new System.Drawing.Point(138, 141);
            this.txtAdiGlobalImagePrefix.Name = "txtAdiGlobalImagePrefix";
            this.txtAdiGlobalImagePrefix.Size = new System.Drawing.Size(293, 20);
            this.txtAdiGlobalImagePrefix.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Image Prefix";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(15, 13);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(61, 13);
            this.label31.TabIndex = 41;
            this.label31.Text = "Login Page";
            // 
            // txtAdiGlobalLoginPage
            // 
            this.txtAdiGlobalLoginPage.Location = new System.Drawing.Point(138, 10);
            this.txtAdiGlobalLoginPage.Name = "txtAdiGlobalLoginPage";
            this.txtAdiGlobalLoginPage.Size = new System.Drawing.Size(293, 20);
            this.txtAdiGlobalLoginPage.TabIndex = 42;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(388, 115);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 13);
            this.label18.TabIndex = 40;
            this.label18.Text = "minutes";
            this.label18.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 118);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(122, 13);
            this.label19.TabIndex = 39;
            this.label19.Text = "Catagory update interval";
            this.label19.Visible = false;
            // 
            // txtAdiGlobalCatagoryUpdateInterval
            // 
            this.txtAdiGlobalCatagoryUpdateInterval.Location = new System.Drawing.Point(138, 115);
            this.txtAdiGlobalCatagoryUpdateInterval.Name = "txtAdiGlobalCatagoryUpdateInterval";
            this.txtAdiGlobalCatagoryUpdateInterval.Size = new System.Drawing.Size(244, 20);
            this.txtAdiGlobalCatagoryUpdateInterval.TabIndex = 38;
            this.txtAdiGlobalCatagoryUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdiGlobalCatagoryUpdateInterval.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(388, 89);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "minutes";
            this.label17.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(114, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Default update interval";
            this.label16.Visible = false;
            // 
            // txtAdiGlobalProductUpdateInterval
            // 
            this.txtAdiGlobalProductUpdateInterval.Location = new System.Drawing.Point(138, 89);
            this.txtAdiGlobalProductUpdateInterval.Name = "txtAdiGlobalProductUpdateInterval";
            this.txtAdiGlobalProductUpdateInterval.Size = new System.Drawing.Size(244, 20);
            this.txtAdiGlobalProductUpdateInterval.TabIndex = 35;
            this.txtAdiGlobalProductUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdiGlobalProductUpdateInterval.Visible = false;
            // 
            // txtAdiGlobalPassword
            // 
            this.txtAdiGlobalPassword.Location = new System.Drawing.Point(138, 62);
            this.txtAdiGlobalPassword.Name = "txtAdiGlobalPassword";
            this.txtAdiGlobalPassword.PasswordChar = '●';
            this.txtAdiGlobalPassword.Size = new System.Drawing.Size(293, 20);
            this.txtAdiGlobalPassword.TabIndex = 34;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Site Password";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(15, 39);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(76, 13);
            this.label33.TabIndex = 32;
            this.label33.Text = "Site Username";
            // 
            // txtAdiGlobalUsername
            // 
            this.txtAdiGlobalUsername.Location = new System.Drawing.Point(138, 36);
            this.txtAdiGlobalUsername.Name = "txtAdiGlobalUsername";
            this.txtAdiGlobalUsername.Size = new System.Drawing.Size(293, 20);
            this.txtAdiGlobalUsername.TabIndex = 33;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSecLockImagePrefix);
            this.tabPage2.Controls.Add(this.label35);
            this.tabPage2.Controls.Add(this.label36);
            this.tabPage2.Controls.Add(this.txtSecLockLoginPage);
            this.tabPage2.Controls.Add(this.label37);
            this.tabPage2.Controls.Add(this.label38);
            this.tabPage2.Controls.Add(this.txtSecLockCategoryUpdateInterval);
            this.tabPage2.Controls.Add(this.label39);
            this.tabPage2.Controls.Add(this.label40);
            this.tabPage2.Controls.Add(this.txtSecLockUpdateInterval);
            this.tabPage2.Controls.Add(this.txtSecLockPassword);
            this.tabPage2.Controls.Add(this.label41);
            this.tabPage2.Controls.Add(this.label43);
            this.tabPage2.Controls.Add(this.txtSecLockUserName);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(929, 304);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "SecLock";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtSecLockImagePrefix
            // 
            this.txtSecLockImagePrefix.Location = new System.Drawing.Point(137, 141);
            this.txtSecLockImagePrefix.Name = "txtSecLockImagePrefix";
            this.txtSecLockImagePrefix.Size = new System.Drawing.Size(293, 20);
            this.txtSecLockImagePrefix.TabIndex = 58;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(14, 144);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(65, 13);
            this.label35.TabIndex = 57;
            this.label35.Text = "Image Prefix";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(14, 13);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(61, 13);
            this.label36.TabIndex = 55;
            this.label36.Text = "Login Page";
            // 
            // txtSecLockLoginPage
            // 
            this.txtSecLockLoginPage.Location = new System.Drawing.Point(137, 10);
            this.txtSecLockLoginPage.Name = "txtSecLockLoginPage";
            this.txtSecLockLoginPage.Size = new System.Drawing.Size(293, 20);
            this.txtSecLockLoginPage.TabIndex = 56;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(387, 115);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(43, 13);
            this.label37.TabIndex = 54;
            this.label37.Text = "minutes";
            this.label37.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(14, 118);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(122, 13);
            this.label38.TabIndex = 53;
            this.label38.Text = "Catagory update interval";
            this.label38.Visible = false;
            // 
            // txtSecLockCategoryUpdateInterval
            // 
            this.txtSecLockCategoryUpdateInterval.Location = new System.Drawing.Point(137, 115);
            this.txtSecLockCategoryUpdateInterval.Name = "txtSecLockCategoryUpdateInterval";
            this.txtSecLockCategoryUpdateInterval.Size = new System.Drawing.Size(244, 20);
            this.txtSecLockCategoryUpdateInterval.TabIndex = 52;
            this.txtSecLockCategoryUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSecLockCategoryUpdateInterval.Visible = false;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(387, 89);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(43, 13);
            this.label39.TabIndex = 51;
            this.label39.Text = "minutes";
            this.label39.Visible = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(14, 92);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(114, 13);
            this.label40.TabIndex = 50;
            this.label40.Text = "Default update interval";
            this.label40.Visible = false;
            // 
            // txtSecLockUpdateInterval
            // 
            this.txtSecLockUpdateInterval.Location = new System.Drawing.Point(137, 89);
            this.txtSecLockUpdateInterval.Name = "txtSecLockUpdateInterval";
            this.txtSecLockUpdateInterval.Size = new System.Drawing.Size(244, 20);
            this.txtSecLockUpdateInterval.TabIndex = 49;
            this.txtSecLockUpdateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSecLockUpdateInterval.Visible = false;
            // 
            // txtSecLockPassword
            // 
            this.txtSecLockPassword.Location = new System.Drawing.Point(137, 62);
            this.txtSecLockPassword.Name = "txtSecLockPassword";
            this.txtSecLockPassword.PasswordChar = '●';
            this.txtSecLockPassword.Size = new System.Drawing.Size(293, 20);
            this.txtSecLockPassword.TabIndex = 48;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(14, 65);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(74, 13);
            this.label41.TabIndex = 45;
            this.label41.Text = "Site Password";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(14, 39);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(76, 13);
            this.label43.TabIndex = 46;
            this.label43.Text = "Site Username";
            // 
            // txtSecLockUserName
            // 
            this.txtSecLockUserName.Location = new System.Drawing.Point(137, 36);
            this.txtSecLockUserName.Name = "txtSecLockUserName";
            this.txtSecLockUserName.Size = new System.Drawing.Size(293, 20);
            this.txtSecLockUserName.TabIndex = 47;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.label49);
            this.tabPage3.Controls.Add(this.txtTriedImagePrefix);
            this.tabPage3.Controls.Add(this.txtTriedPassword);
            this.tabPage3.Controls.Add(this.txtTriedUserName);
            this.tabPage3.Controls.Add(this.txtTriedLoginPage);
            this.tabPage3.Controls.Add(this.label50);
            this.tabPage3.Controls.Add(this.label47);
            this.tabPage3.Controls.Add(this.label46);
            this.tabPage3.Controls.Add(this.label45);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(929, 304);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "Tri-ed.com";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtTriedImagePrefix
            // 
            this.txtTriedImagePrefix.Location = new System.Drawing.Point(137, 103);
            this.txtTriedImagePrefix.Name = "txtTriedImagePrefix";
            this.txtTriedImagePrefix.Size = new System.Drawing.Size(293, 20);
            this.txtTriedImagePrefix.TabIndex = 11;
            // 
            // txtTriedPassword
            // 
            this.txtTriedPassword.Location = new System.Drawing.Point(137, 72);
            this.txtTriedPassword.Name = "txtTriedPassword";
            this.txtTriedPassword.Size = new System.Drawing.Size(293, 20);
            this.txtTriedPassword.TabIndex = 8;
            // 
            // txtTriedUserName
            // 
            this.txtTriedUserName.Location = new System.Drawing.Point(137, 40);
            this.txtTriedUserName.Name = "txtTriedUserName";
            this.txtTriedUserName.Size = new System.Drawing.Size(293, 20);
            this.txtTriedUserName.TabIndex = 7;
            // 
            // txtTriedLoginPage
            // 
            this.txtTriedLoginPage.Location = new System.Drawing.Point(137, 10);
            this.txtTriedLoginPage.Name = "txtTriedLoginPage";
            this.txtTriedLoginPage.Size = new System.Drawing.Size(293, 20);
            this.txtTriedLoginPage.TabIndex = 6;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(14, 103);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(65, 13);
            this.label50.TabIndex = 5;
            this.label50.Text = "Image Prefix";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(14, 75);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(74, 13);
            this.label47.TabIndex = 2;
            this.label47.Text = "Site Password";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(14, 43);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(76, 13);
            this.label46.TabIndex = 1;
            this.label46.Text = "Site Username";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(14, 13);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(61, 13);
            this.label45.TabIndex = 0;
            this.label45.Text = "Login Page";
            // 
            // contextMenuStripTaskEdit
            // 
            this.contextMenuStripTaskEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemIgnitoMode,
            this.toolStripMenuItemDownloadImages,
            this.toolStripSeparator1,
            this.toolStripMenuItemRefresh,
            this.toolStripSeparator2,
            this.toolStripMenuItemDelete});
            this.contextMenuStripTaskEdit.Name = "contextMenuStripTaskEdit";
            this.contextMenuStripTaskEdit.Size = new System.Drawing.Size(170, 104);
            // 
            // toolStripMenuItemIgnitoMode
            // 
            this.toolStripMenuItemIgnitoMode.Name = "toolStripMenuItemIgnitoMode";
            this.toolStripMenuItemIgnitoMode.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemIgnitoMode.Text = "Ignito Mode";
            this.toolStripMenuItemIgnitoMode.Click += new System.EventHandler(this.toolStripMenuItemIgnitoMode_Click);
            // 
            // toolStripMenuItemDownloadImages
            // 
            this.toolStripMenuItemDownloadImages.Name = "toolStripMenuItemDownloadImages";
            this.toolStripMenuItemDownloadImages.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemDownloadImages.Text = "Download Images";
            this.toolStripMenuItemDownloadImages.Click += new System.EventHandler(this.toolStripMenuItemDownloadImages_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemRefresh.Text = "Refresh";
            this.toolStripMenuItemRefresh.Click += new System.EventHandler(this.toolStripMenuItemRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItemDelete.Text = "Delete";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listViewScheduleTasks
            // 
            this.listViewScheduleTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewScheduleTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTaskHeaderID,
            this.columnHeaderTaskHeaderName,
            this.columnHeadertaskHeaderTaskDescription,
            this.columnHeaderTaskHeaderSite,
            this.columnHeaderTaskHeaderScheduleFrom,
            this.columnHeaderTaskHeaderRepeat,
            this.columnHeaderTaskHeaderRepeatInterval,
            this.columnHeaderTaskHeaderEnabled,
            this.columnHeaderTaskHeaderLastRun,
            this.columnHeaderTaskHeaderNextRun});
            this.listViewScheduleTasks.FullRowSelect = true;
            this.listViewScheduleTasks.GridLines = true;
            this.listViewScheduleTasks.HideSelection = false;
            listViewItem3.Tag = "";
            this.listViewScheduleTasks.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.listViewScheduleTasks.Location = new System.Drawing.Point(0, 445);
            this.listViewScheduleTasks.MultiSelect = false;
            this.listViewScheduleTasks.Name = "listViewScheduleTasks";
            this.listViewScheduleTasks.Size = new System.Drawing.Size(961, 97);
            this.listViewScheduleTasks.TabIndex = 28;
            this.listViewScheduleTasks.UseCompatibleStateImageBehavior = false;
            this.listViewScheduleTasks.View = System.Windows.Forms.View.Details;
            this.listViewScheduleTasks.SelectedIndexChanged += new System.EventHandler(this.listViewScheduleTasks_SelectedIndexChanged);
            this.listViewScheduleTasks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewScheduleTasks_MouseClick);
            this.listViewScheduleTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewScheduleTasks_MouseDoubleClick);
            // 
            // columnHeaderTaskHeaderID
            // 
            this.columnHeaderTaskHeaderID.Text = "ScheduleID";
            this.columnHeaderTaskHeaderID.Width = 0;
            // 
            // columnHeaderTaskHeaderName
            // 
            this.columnHeaderTaskHeaderName.Text = "TaskName";
            // 
            // columnHeadertaskHeaderTaskDescription
            // 
            this.columnHeadertaskHeaderTaskDescription.Text = "Description";
            this.columnHeadertaskHeaderTaskDescription.Width = 180;
            // 
            // columnHeaderTaskHeaderSite
            // 
            this.columnHeaderTaskHeaderSite.Text = "Site";
            // 
            // columnHeaderTaskHeaderScheduleFrom
            // 
            this.columnHeaderTaskHeaderScheduleFrom.Text = "ScheduleFrom";
            // 
            // columnHeaderTaskHeaderRepeat
            // 
            this.columnHeaderTaskHeaderRepeat.Text = "Repeat";
            // 
            // columnHeaderTaskHeaderRepeatInterval
            // 
            this.columnHeaderTaskHeaderRepeatInterval.Text = "Repeat Interval";
            // 
            // columnHeaderTaskHeaderEnabled
            // 
            this.columnHeaderTaskHeaderEnabled.Text = "Enabled";
            // 
            // columnHeaderTaskHeaderLastRun
            // 
            this.columnHeaderTaskHeaderLastRun.Text = "Last Run";
            // 
            // columnHeaderTaskHeaderNextRun
            // 
            this.columnHeaderTaskHeaderNextRun.Text = "Next Run";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(1, 429);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(90, 13);
            this.label34.TabIndex = 29;
            this.label34.Text = "Scheduled Tasks";
            // 
            // btnNewSchedule
            // 
            this.btnNewSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewSchedule.Location = new System.Drawing.Point(843, 425);
            this.btnNewSchedule.Name = "btnNewSchedule";
            this.btnNewSchedule.Size = new System.Drawing.Size(54, 20);
            this.btnNewSchedule.TabIndex = 30;
            this.btnNewSchedule.Text = "New";
            this.btnNewSchedule.UseVisualStyleBackColor = true;
            this.btnNewSchedule.Click += new System.EventHandler(this.btnNewSchedule_Click);
            // 
            // btnDeleteSchedule
            // 
            this.btnDeleteSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteSchedule.Location = new System.Drawing.Point(903, 425);
            this.btnDeleteSchedule.Name = "btnDeleteSchedule";
            this.btnDeleteSchedule.Size = new System.Drawing.Size(54, 20);
            this.btnDeleteSchedule.TabIndex = 32;
            this.btnDeleteSchedule.Text = "Delete";
            this.btnDeleteSchedule.UseVisualStyleBackColor = true;
            this.btnDeleteSchedule.Click += new System.EventHandler(this.btnDeleteSchedule_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(18, 139);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(46, 13);
            this.label49.TabIndex = 12;
            this.label49.Text = "CSV DB";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 135);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(293, 20);
            this.textBox1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(439, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 19);
            this.button1.TabIndex = 14;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 567);
            this.Controls.Add(this.btnDeleteSchedule);
            this.Controls.Add(this.btnNewSchedule);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.listViewScheduleTasks);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web Spider";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabAdiGlobal.ResumeLayout(false);
            this.tabPageAdiCategory.ResumeLayout(false);
            this.tabPageAdiBrand.ResumeLayout(false);
            this.tabPageAdiProducts.ResumeLayout(false);
            this.tabPageAdiProducts.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageTasks.ResumeLayout(false);
            this.tabPageTasks.PerformLayout();
            this.tabPageAdiGlobal.ResumeLayout(false);
            this.tabPageAdiGlobal.PerformLayout();
            this.tabPageSecLock.ResumeLayout(false);
            this.tabPageSecLock.PerformLayout();
            this.tabControlSecLock.ResumeLayout(false);
            this.tabPageSeclockManufacturers.ResumeLayout(false);
            this.tabPageSeclockManufacturers.PerformLayout();
            this.tabPageSecLockCategories.ResumeLayout(false);
            this.tabPageSecLockCategories.PerformLayout();
            this.tabPageSecLockProducts.ResumeLayout(false);
            this.tabPageSecLockProducts.PerformLayout();
            this.tabPageSecLockCSV.ResumeLayout(false);
            this.tabPageSecLockCSV.PerformLayout();
            this.tabPageTri.ResumeLayout(false);
            this.tabPageTri.PerformLayout();
            this.tabControlTri.ResumeLayout(false);
            this.tabTriManufacturers.ResumeLayout(false);
            this.tabTriManufacturers.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxDatabase.ResumeLayout(false);
            this.groupBoxDatabase.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Stockalert.ResumeLayout(false);
            this.Stockalert.PerformLayout();
            this.tabMail.ResumeLayout(false);
            this.tabMail.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStripTaskEdit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonGo_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolBar toolBarTasks;
        private System.Windows.Forms.ToolBarButton toolBarButtonContinue;
        private System.Windows.Forms.ToolBarButton toolBarButtonPause;
        private System.Windows.Forms.ToolBarButton toolBarButtonStop;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButtonDeleteAll;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        //private System.Windows.Forms.ToolBarButton toolBarButtonSettings;
        private System.Windows.Forms.TabControl tabAdiGlobal;
        private System.Windows.Forms.TabPage tabPageAdiCategory;
        private System.Windows.Forms.TabPage tabPageAdiBrand;
        private System.Windows.Forms.TreeView treeCatagory;
        private System.Windows.Forms.ListView listViewThreads;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskID;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskNameValue;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskName;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkAdiDownloadImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusActiveThreads;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusActiveThreadCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSeperator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusQueued;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusQueuesThreadsCount;
        private System.Windows.Forms.Timer StatusBarSyncTimer;
        private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.TreeView treeViewBrands;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskType;
        private System.Windows.Forms.Timer timerSpider;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Timer timerPingRate;
        private System.Windows.Forms.CheckBox chkAdiIncognito;
        private System.Windows.Forms.ColumnHeader columnHeaderWebSite;
        private System.Windows.Forms.Timer timerTasks;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageAdiGlobal;
        private System.Windows.Forms.TabPage tabPageTasks;
        private System.Windows.Forms.ComboBox cmbAdiMode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTaskEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIgnitoMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDownloadImages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ColumnHeader columnHeaderIgnitoMode;
        private System.Windows.Forms.ColumnHeader columnHeaderDownloadImages;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskMode;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.ToolBar toolBarAdiGlobal;
        private System.Windows.Forms.ToolBarButton toolBarButtonAdiSettings;
        private System.Windows.Forms.ToolBarButton toolBarButton11;
        private System.Windows.Forms.ToolBarButton toolBarAdiPriority;
        private System.Windows.Forms.ToolBarButton toolBarButtonAdiReload;
        private System.Windows.Forms.ToolBarButton toolBarButtonAdiScheduler;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TextBox txtPing;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBoxDatabase;
        private System.Windows.Forms.Button btnCrawlImageBrowse;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtCrawlImageFolder;
        private System.Windows.Forms.Button btnBrowseUpdate;
        private System.Windows.Forms.Button btnBrowseCrawl;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtUpdateDB;
        private System.Windows.Forms.TextBox txtcrawlDB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSpiderDay;
        private System.Windows.Forms.ComboBox cmbSpiderMin;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbSpiderHour;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox Stockalert;
        private System.Windows.Forms.ListBox lstSDay;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cmbSEndHour;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbSHour;
        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtRetryCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtThreadsCount;
        private System.Windows.Forms.CheckBox chkCaching;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCacheDuration;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtErrorFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkErrorLog;
        private System.Windows.Forms.TabPage tabMail;
        private System.Windows.Forms.CheckBox chkSendErrorMail;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMailTo;
        private System.Windows.Forms.TextBox txtMailFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMailSubject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.CheckBox chkSmtpSSL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSmtpUserName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSmtpPassword;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnUpdateImageBrowse;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtUpdateImageFolder;
        private System.Windows.Forms.Button btnSettingsSave;
        private System.Windows.Forms.Button btnSettingsCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtAdiGlobalImagePrefix;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtAdiGlobalLoginPage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtAdiGlobalCatagoryUpdateInterval;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAdiGlobalProductUpdateInterval;
        private System.Windows.Forms.TextBox txtAdiGlobalPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtAdiGlobalUsername;
        private System.Windows.Forms.TabPage tabPageAdiProducts;
        private System.Windows.Forms.TreeView treeViewAdiProducts;
        private System.Windows.Forms.CheckBox chkAdiProductsCheckAll;
        private System.Windows.Forms.ToolBarButton toolBarButtonAdiPriority;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerateAdiUpdateSchema;
        private System.Windows.Forms.Button btnGenerateAdiCrawlSchema;
        private System.Windows.Forms.ListView listViewScheduleTasks;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeadertaskHeaderTaskDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderSite;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderScheduleFrom;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderRepeat;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderRepeatInterval;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderEnabled;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderLastRun;
        private System.Windows.Forms.ColumnHeader columnHeaderTaskHeaderNextRun;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btnNewSchedule;
        private System.Windows.Forms.Button btnDeleteSchedule;
        private System.Windows.Forms.Button btnCleanDb;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSeperator2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusExport;
        private System.Windows.Forms.TabPage tabPageSecLock;
        private System.Windows.Forms.TreeView tvSecLockManufacturers;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtSecLockImagePrefix;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtSecLockLoginPage;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtSecLockCategoryUpdateInterval;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txtSecLockUpdateInterval;
        private System.Windows.Forms.TextBox txtSecLockPassword;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txtSecLockUserName;
        private System.Windows.Forms.TabControl tabControlSecLock;
        private System.Windows.Forms.TabPage tabPageSeclockManufacturers;
        private System.Windows.Forms.TabPage tabPageSecLockCategories;
        private System.Windows.Forms.TreeView tvSecLockCategories;
        private System.Windows.Forms.ToolBar toolBarSecLock;
        private System.Windows.Forms.ToolBarButton toolBarButtonSecLockRefresh;
        private System.Windows.Forms.TabPage tabPageSecLockProducts;
        private System.Windows.Forms.TreeView tvsecLockProducts;
        private System.Windows.Forms.CheckBox chkSeclockIncognito;
        private System.Windows.Forms.CheckBox chkSeclockDownloadImages;
        private System.Windows.Forms.CheckBox chkSecLockManufanufacturersSelectAll;
        private System.Windows.Forms.CheckBox chkSecLockCategoriessSelectAll;
        private System.Windows.Forms.CheckBox chkSecLockProductsSelectAll;
        private System.Windows.Forms.Button btnDocFolderBrowse;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtDocFolder;
        private System.Windows.Forms.TabPage tabPageSecLockCSV;
        private System.Windows.Forms.CheckBox chkSecLockCSVProductSelectAll;
        private System.Windows.Forms.TreeView tvSecLockCSVProduct;
        private System.Windows.Forms.CheckBox chkSeclockCSVDownloadImages;
        private System.Windows.Forms.CheckBox chkSeclockCSVIncognito;
        private System.Windows.Forms.Button btnSecLockCSVStart;
        private System.Windows.Forms.TabPage tabPageTri;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtTriedImagePrefix;
        private System.Windows.Forms.TextBox txtTriedPassword;
        private System.Windows.Forms.TextBox txtTriedUserName;
        private System.Windows.Forms.TextBox txtTriedLoginPage;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.CheckBox chkTRIownloadImages;
        private System.Windows.Forms.CheckBox chkTriIncognito;
        private System.Windows.Forms.Button toolBarButtonTriReload;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ComboBox cmbTriMode;
        private System.Windows.Forms.TabControl tabControlTri;
        private System.Windows.Forms.TabPage tabTriManufacturers;
        private System.Windows.Forms.CheckBox chkTriManufanufacturersSelectAll;
        private System.Windows.Forms.TreeView tvTriManufacturers;
        private System.Windows.Forms.TabPage tabTriMainCategory;
        private System.Windows.Forms.TabPage tabTriProduct;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label49;
    }
}

