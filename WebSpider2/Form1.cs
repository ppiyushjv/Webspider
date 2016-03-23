using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using WebSpider.Core;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Collections.Specialized;
using System.Web;
using WebSpider.Data.WebSpiderDb1TableAdapters;
using Newtonsoft.Json;
using WebSpider.Objects;
using WebSpider.AdiGlobal.Objects.AdiGlobal;
using WebSpider.Data.General;
using WebSpider.Objects.General;
using WebSpider.AdiGlobal.Objects.AdiExport;

namespace WebSpider
{
    public partial class Form1 : Form
    {

        #region [Properties]
        Browser browser = new Browser();       
        int pingCount = 1;        
        #endregion
        
        #region [ Form ]

        #region [ Constructor ]
        public Form1()
        {
            InitializeComponent();
            InitTasks();
            InitExport();
            InitScheduler();
        }
        #endregion
        
        #region [Form Load]
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime buildDate = ApplicationInformation.CompileDate;
            this.Text = String.Format("WebSpider v{0} Build {1:MMM dd, yyyy}", Application.ProductVersion, buildDate);
            LoadSettings();
            ReloadTaskDetails();
        }


        #endregion

        #region [Form Closing]
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SpiderThread.ThreadCount != 0)
            {
                DialogResult result = MessageBox.Show(this, "Cancel active operations?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    //Utility.ApplicationLog("Cancelling all active tasks");
                    SpiderThread.CancelAll();
                    exportTimer.Stop();
                    //Utility.ApplicationLog("All tasks cancelled");
                }
                else
                    e.Cancel = true;
                //Utility.ApplicationLog("Closing WebSpider");
            }
            else if (AdiSpider.PendingExports())
            {
                DialogResult result = MessageBox.Show(this, "Products are currently being saved. Do you really want to terminate application without completing product saving?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    //Utility.ApplicationLog("Cancelling all active tasks");
                    SpiderThread.CancelAll();
                    exportTimer.Stop();
                    //Utility.ApplicationLog("All tasks cancelled");
                }
                else
                    e.Cancel = true;
            }
        }
        #endregion

        #region [Status Bar]
        private void StatusBarSyncTimer_Tick(object sender, EventArgs e)
        {
            toolStripStatusActiveThreadCount.Text = SpiderThread.ActiveThreadCount.ToString();
            toolStripStatusQueuesThreadsCount.Text = SpiderThread.QueuedThreadCount.ToString();
            if (SpiderThread.ActiveThreadCount == 0 && SpiderThread.QueuedThreadCount == 0)
            {
                btnStart.Text = "Start";
                btnStart.Enabled = true;
                cmbAdiMode.Enabled = true;
                chkAdiDownloadImage.Enabled = true;
                btnTimer.Enabled = true;
                if (cmbAdiMode.Text != "Update")
                {
                    treeCatagory.Enabled = true;
                    chkAdiIncognito.Enabled = true;
                }

                BackgroundWorker statusBarWorker = new BackgroundWorker();
                statusBarWorker.DoWork += statusBarWorker_DoWork;
                statusBarWorker.RunWorkerCompleted += statusBarWorker_RunWorkerCompleted;
                statusBarWorker.RunWorkerAsync();
            }
        }

        #region [ Data Exporter ]
        private void exportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void exportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [Email Worker ]
        private void statusBarWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusBarSyncTimer.Enabled = true;
        }

        private void statusBarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StatusBarSyncTimer.Enabled = false;
            String FileName = String.Format("{0}\\{1}", Application.StartupPath, Constants.EmailErrorFile);
            if (File.Exists(FileName))
            {
                try { Utility.SendErrorMail(FileName); }
                catch { }

                try { File.Delete(FileName); }
                catch { }
            }
        }
        #endregion
        
        #endregion

        #region [Delegates]
        private delegate void ShowFormDelegate(Form form);
        private void ShowForm(Form form)
        {
            if (this.InvokeRequired)
            {
                ShowFormDelegate d = new ShowFormDelegate(ShowForm);
                this.Invoke(d, form);
            }
            else
            {
                form.Show(this);
            }
        }

        private delegate void CloseFormDelegate(Form form);
        private void CloseForm(Form form)
        {
            if (this.InvokeRequired)
            {
                CloseFormDelegate d = new CloseFormDelegate(CloseForm);
                this.Invoke(d, form);
            }
            else
            {
                form.Close();
            }
        }
            
        public delegate void ClearTreeNodesDelegate(TreeView treeView);
        private void ClearTreeNodes(TreeView treeView)
        {
            if (this.InvokeRequired)
            {
                ClearTreeNodesDelegate d = new ClearTreeNodesDelegate(ClearTreeNodes);
                treeView.Invoke(d, treeView);
            }
            else
            {
                treeView.Nodes.Clear();
            }
        }

        public delegate void AddTreeRootNodeDelegate(TreeView treeView, TreeNode tNode);
        private void AddTreeRootNode(TreeView treeView, TreeNode tNode)
        {
            if (this.InvokeRequired)
            {
                AddTreeRootNodeDelegate d = new AddTreeRootNodeDelegate(AddTreeRootNode);
                treeView.Invoke(d, treeView, tNode);
            }
            else
            {
                treeView.Nodes.Add(tNode);
            }
        }

        public delegate void DeleteTreeRootNodeDelegate(TreeView treeView, TreeNode tNode);
        private void DeleteTreeRootNode(TreeView treeView, TreeNode tNode)
        {
            if (this.InvokeRequired)
            {
                DeleteTreeRootNodeDelegate d = new DeleteTreeRootNodeDelegate(DeleteTreeRootNode);
                treeView.Invoke(d, treeView, tNode);
            }
            else
            {
                treeView.Nodes.Remove(tNode);
            }
        }

        public delegate void AddTreeSubNodeDelegate(TreeNode tNode, TreeNode newNode);
        private void AddTreeSubNode(TreeNode tNode, TreeNode newNode)
        {
            if (this.InvokeRequired)
            {
                AddTreeSubNodeDelegate d = new AddTreeSubNodeDelegate(AddTreeSubNode);
                treeCatagory.Invoke(d, tNode, newNode);
            }
            else
            {
                tNode.Nodes.Add(newNode);
            }
        }

        private delegate void CheckNodeDelegate(TreeNode tNode, Boolean Checked);
        private void CheckNode(TreeNode tNode, Boolean Checked)
        {
            if (treeCatagory.InvokeRequired)
            {
                CheckNodeDelegate d = new CheckNodeDelegate(CheckNode);
                treeCatagory.Invoke(d, tNode, Checked);
            }
            else
                tNode.Checked = Checked;
        }

        private delegate void ChangeButtonTextDelegate(Button button, String Text);
        private void ChangeButtonText(Button button, String Text)
        {
            if (this.InvokeRequired)
            {
                ChangeButtonTextDelegate d = new ChangeButtonTextDelegate(ChangeButtonText);
                this.Invoke(d, button, Text);
            }
            else
                button.Text = Text;
        }

        private delegate void ChangeControlStateDelegate(Control control, Boolean Enabled);
        private void ChangeControlState(Control control, Boolean Enabled)
        {
            if (this.InvokeRequired)
            {
                ChangeControlStateDelegate d = new ChangeControlStateDelegate(ChangeControlState);
                this.Invoke(d, control, Enabled);
            }
            else
                control.Enabled = Enabled;
        }

        private delegate void ChangeProductListViewStatusTextDelegate(ListViewItem lvItem, String Text);
        private void ChangeProductListViewStatusText(ListViewItem lvItem, String Text)
        {
            if (this.InvokeRequired)
            {
                ChangeProductListViewStatusTextDelegate d = new ChangeProductListViewStatusTextDelegate(ChangeProductListViewStatusText);
                this.Invoke(d, lvItem, Text);
            }
            else
                lvItem.SubItems[2].Text = Text;
        }
        #endregion

        #region [ Tab Selection ]
        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            switch(e.TabPage.Name){
                case "tabPageAdiGlobal":
                    if (String.IsNullOrEmpty(cmbAdiMode.Text))
                        cmbAdiMode.SelectedIndex = 0;
                    break;
                case "tabPageSecLock":
                    if (tvSecLockManufacturers.Nodes.Count == 0)
                        LoadSecLockManufactueres();
                    if (tvSecLockCategories.Nodes.Count == 0)
                        LoadSecLockCategories();
                    if (tvsecLockProducts.Nodes.Count == 0)
                        LoadSecLockUpdateProducts();
                    break;
                case "tabPageSecLockCSV":
                    if (tvSecLockCSVProduct.Nodes.Count == 0)
                    {
                        LoadSecLockCSVUpdateProducts();
                        chkSecLockCSVProductSelectAll.Checked = true;
                    }
                    break;
                case "tabPageTri":
                    if (String.IsNullOrEmpty(cmbTriMode.Text))
                        cmbTriMode.SelectedIndex = 0;
                    break;
            }
        }
        #endregion

        #endregion

        #region [ Tasks ]

        private void InitTasks()
        {
            TaskDetailManager tdMgr = new TaskDetailManager(Constants.ConnectionString);
            tdMgr.DeleteByHeaderId(-1);
        }

        #region [Toolbar Events]
        private void toolBarTasks_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Name)
            {
                case "toolBarButtonPause":
                    //Utility.ApplicationLog("Pausing all tasks");
                    SpiderThread.PauseAll();
                    foreach (ListViewItem item in listViewThreads.Items)
                    {
                        if (item.SubItems[2].Text == Constants.PROCESSING_TEXT)
                            item.SubItems[2].Text = Constants.PAUSED_TEXT;
                    }
                    break;
                case "toolBarButtonContinue":
                    //Utility.ApplicationLog("Resuming active all tasks");
                    SpiderThread.ResumeAll();
                    foreach (ListViewItem item in listViewThreads.Items)
                    {
                        if (item.SubItems[2].Text == Constants.PAUSED_TEXT)
                            item.SubItems[2].Text = Constants.PROCESSING_TEXT;
                    }
                    break;
                case "toolBarButtonStop":
                    //Utility.ApplicationLog("Stopping active all tasks");
                    SpiderThread.CancelAll();
                    foreach (ListViewItem item in listViewThreads.Items)
                    {
                        if (item.SubItems[2].Text == Constants.PROCESSING_TEXT)
                            item.SubItems[2].Text = Constants.CANCELLED_TEXT;
                    }
                    break;
                case "toolBarButtonDeleteAll":
                    //Utility.ApplicationLog("Deleting active all tasks");
                    while (listViewThreads.Items.Count > 0)
                    {
                        listViewThreads.Items[0].Selected = true;
                        TasksDelete();
                    }
                    break;
                //case "toolBarButtonSettings":
                //    Form form1 = new frmSettings();
                //    ShowForm(form1);
                //    break;
                //case "toolBarButtonPriority":
                //    Form form = new ProductPriority();
                //    ShowForm(form);
                //    break;
            }
        }
        #endregion

        #region [ Context Menu ]

        #region [mouse Click]
        private void listViewThreads_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewThreads.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    ListViewItem item = listViewThreads.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        var selectedItems = listViewThreads.SelectedItems;
                        if (selectedItems.Count == 1)
                        {
                            // Single Selected Item
                            //item.Selected = true;
                            TaskDetail taskDetail = GetTaskFromListViewItem(selectedItems[0]);
                            toolStripMenuItemIgnitoMode.Text = String.Format("Ignito Mode {0}", taskDetail.IncognitoMode ? "OFF" : "ON");
                            toolStripMenuItemDownloadImages.Text = String.Format("DownloadImages {0}", taskDetail.DownloadImages ? "OFF" : "ON");
                            
                        }
                        else
                        {
                            // Multiple Selected Items
                            List<TaskDetail> taskDetails = new List<TaskDetail>();
                            foreach (ListViewItem lvItem in selectedItems)
                                taskDetails.Add(GetTaskFromListViewItem(lvItem));
                            Boolean IgnitoMode = (taskDetails.Where(x => x.IncognitoMode).Count() == taskDetails.Count);
                            Boolean DownloadImages = (taskDetails.Where(x => x.DownloadImages).Count() == taskDetails.Count);

                            toolStripMenuItemIgnitoMode.Text = String.Format("Ignito Mode {0}", IgnitoMode ? "OFF" : "ON");
                            toolStripMenuItemDownloadImages.Text = String.Format("DownloadImages {0}", DownloadImages ? "OFF" : "ON");
                        }
                        contextMenuStripTaskEdit.Show(listViewThreads, e.Location);
                    }
                    //contextMenuStripTaskEdit.Show(Cursor.Position);
                }
            }
        }
        #endregion

        #region [Ignito Mode Toggle Task Details]
        private void toolStripMenuItemIgnitoMode_Click(object sender, EventArgs e)
        {
            String Text = (sender as ToolStripMenuItem).Text;
            TaskIgnitoModeToggle(Text.EndsWith("ON"));
        }
        #endregion

        #region [Download Images Toggle Task Detail]
        private void toolStripMenuItemDownloadImages_Click(object sender, EventArgs e)
        {
            String Text = (sender as ToolStripMenuItem).Text;
            TaskDownloadImagesToggle(Text.EndsWith("ON"));
        }
        #endregion

        #region [Refresh Task Details]
        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            TasksRefresh();
        }
        #endregion

        #region [Delete Task Details]
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            TasksDelete();
        }
        #endregion

        #region [Task Edit]

        #region [ Ignito Mode Toggle ]
        void TaskIgnitoModeToggle(Boolean IgnitoMode)
        {
            var items = listViewThreads.SelectedItems;
            if (!ReferenceEquals(items, null))
            {
                foreach (ListViewItem item in items)
                {
                    TaskDetail taskDetail = GetTaskFromListViewItem(item);
                    taskDetail = new TasksScheduler().GetTaskDetailByID(taskDetail.TaskID);
                    if (!ReferenceEquals(taskDetail, null))
                    {
                        // Ignito Mode Toggle
                        taskDetail.IncognitoMode = IgnitoMode; // !taskDetail.IgnitoMode;
                        new TasksScheduler().SaveTaskDetail(taskDetail);
                        SetTaskDetailInListViewItem(taskDetail);
                    }
                }
                ReloadTaskDetails();
            }
        }
        #endregion

        #region [ Download Images Toggle ]
        void TaskDownloadImagesToggle(Boolean DownloadImages)
        {
            var items = listViewThreads.SelectedItems;
            if (!ReferenceEquals(items, null))
            {
                foreach (ListViewItem item in items)
                {
                    TaskDetail taskDetail = GetTaskFromListViewItem(item);
                    taskDetail = new TasksScheduler().GetTaskDetailByID(taskDetail.TaskID);
                    if (!ReferenceEquals(taskDetail, null))
                    {
                        // Download Images Toggle
                        taskDetail.DownloadImages = DownloadImages; //!taskDetail.DownloadImages;
                        new TasksScheduler().SaveTaskDetail(taskDetail);
                        SetTaskDetailInListViewItem(taskDetail);
                    }
                }
                ReloadTaskDetails();
            }
        }
        #endregion

        #region [ Refresh Task Items ]
        void TasksRefresh()
        {
            var items = listViewThreads.SelectedItems;
            if (!ReferenceEquals(items, null))
            {
                foreach (ListViewItem item in items)
                {
                    TaskDetail taskDetail = GetTaskFromListViewItem(item);
                    taskDetail = new TasksScheduler().GetTaskDetailByID(taskDetail.TaskID);
                    if (!ReferenceEquals(taskDetail, null))
                    {
                        SetTaskDetailInListViewItem(taskDetail);
                    }
                }
            }
        }
        #endregion

        #region [Delete Task Items]
        void TasksDelete()
        {
            var items = listViewThreads.SelectedItems;
            if (!ReferenceEquals(items, null))
            {
                foreach (ListViewItem item in items)
                {
                    TaskDetail taskDetail = GetTaskFromListViewItem(item);
                    taskDetail = new TasksScheduler().GetTaskDetailByID(taskDetail.TaskID);
                    if (!ReferenceEquals(taskDetail, null))
                    {
                        if (new TasksScheduler().DeleteTaskDetail(taskDetail.TaskID) > 0)
                            item.Remove();
                        //SetTaskDetailInListViewItem(taskDetail);
                    }
                }
                ReloadTaskDetails();
            }
        }
        #endregion

        #endregion

        #region [ Key Press ]
        private void listViewThreads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                TasksDelete();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.I)
            {
                TaskIgnitoModeToggle(false);
            }
            else if (e.KeyCode == Keys.I)
            {
                TaskIgnitoModeToggle(true);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                TaskDownloadImagesToggle(false);
            }
            else if (e.KeyCode == Keys.D)
            {
                TaskDownloadImagesToggle(true);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in listViewThreads.Items)
                    item.Selected = true;
            }
        }
        #endregion
        
        #endregion

        #region[Get Set Task Details]
        private TaskDetail GetTaskFromListViewItem(ListViewItem lvItem)
        {
            TaskDetail td = new TaskDetail()
            {
                TaskID = Convert.ToInt64(lvItem.SubItems[0].Text),
                TaskNameValue = lvItem.SubItems[1].Text,
                TaskNameText = lvItem.SubItems[2].Text,
                TaskStatusText = lvItem.SubItems[3].Text,
                TaskType = lvItem.SubItems[4].Text,
                TaskMode = lvItem.SubItems[5].Text,
                IncognitoMode = lvItem.SubItems[6].Text == "YES" ? true : false,
                DownloadImages = lvItem.SubItems[7].Text == "YES" ? true : false,
                TaskSite = lvItem.SubItems[8].Text
            };
            return td;
        }

        private void SetTaskDetailInListViewItem(TaskDetail taskDetail, ref ListViewItem lvItem)
        {
            String[] text = { 
                                taskDetail.TaskID.ToString(), 
                                taskDetail.TaskNameValue, 
                                taskDetail.TaskNameText, 
                                taskDetail.TaskStatusText, 
                                taskDetail.TaskType, 
                                taskDetail.TaskMode,
                                taskDetail.IncognitoMode ? "YES" : "NO", 
                                taskDetail.DownloadImages ? "YES" : "NO", 
                                taskDetail.TaskSite 
                            };
            if (lvItem == null)
                lvItem = new ListViewItem(text);
            else
            {
                for (int i = 0; i < text.Length; i++)
                    lvItem.SubItems[i].Text = text[i];
            }
        }

        private void SetTaskDetailInListViewItem(TaskDetail taskDetail)
        {
            ListViewItem lvItem = FindListItemByTask(taskDetail);
            if (lvItem == null)
            {
                //lvItem = new ListViewItem();
                SetTaskDetailInListViewItem(taskDetail, ref lvItem);
                listViewThreads.Items.Add(lvItem);
            }
            else
            {
                SetTaskDetailInListViewItem(taskDetail, ref lvItem);
            }
        }

        private ListViewItem FindListItemByTask(TaskDetail taskDetail)
        {
            return listViewThreads.FindItemWithText(taskDetail.TaskID.ToString());
        }
        #endregion

        #region [Reload Task Details From Db]
        private void ReloadTaskDetails(Boolean FullReload = false, Int64 SchedulerID = -1)
        {
            if (FullReload)
            {
                listViewThreads.Items.Clear();
            }
            List<TaskDetail> dbtasks = new TasksScheduler().GetAllTasks(SchedulerID);

            List<TaskDetail> listTasks = new List<TaskDetail>();
            foreach (ListViewItem item in listViewThreads.Items)
            {
                listTasks.Add(GetTaskFromListViewItem(item));
            }

            foreach (TaskDetail task in dbtasks)
            {
                SetTaskDetailInListViewItem(task);
            }
        }
        #endregion

        #region [START BUTTON]

        private void btnStart_Click(object sender, EventArgs e)
        {
            BackgroundWorker startWorker = new BackgroundWorker();
            startWorker.DoWork += startWorker_DoWork;
            startWorker.RunWorkerCompleted += startWorker_RunWorkerCompleted;
            StatusBarSyncTimer.Enabled = false;
            startWorker.RunWorkerAsync();
        }

        private void startWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Utility.ApplicationLog("Starting to process tasks");
                ChangeButtonText(btnStart, "Please wait...");
                ChangeControlState(btnStart, false);
                ChangeControlState(cmbAdiMode, false);
                ChangeControlState(treeCatagory, false);
                ChangeControlState(chkAdiDownloadImage, false);
                ChangeControlState(btnTimer, false);
                ChangeControlState(chkAdiIncognito, false);

                if (btnStart.Enabled)
                    return;

                List<TaskDetail> tasksList = new TasksScheduler().GetPendingTasks();

                for (int index = 0; index < tasksList.Count; index++)
                {
                    if (tasksList[index].TaskMode == Constants.TaskMode.ADI_CRAWL)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.CrawlProduct), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_UPDATE)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.UpdateProduct), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_CLEARANCE_ZONE)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.GetClearanzeZone), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_HOT_DEALS)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.GetHotDeals), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_ONLINE_SPECIALS)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.GetOnlineSpecials), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_SALE_CENTER)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.GetSaleCenter), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_IN_STOCK)
                        SpiderThread.Add(new ParameterizedThreadStart(AdiSpider.GetInStockItems), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.SECLOCK_MANUFACTURER_CRAWL)
                        SpiderThread.Add(new ParameterizedThreadStart(SecLockSpider.Crawl), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.SECLOCK_CATEGORY_CRAWL)
                        SpiderThread.Add(new ParameterizedThreadStart(SecLockSpider.Crawl), tasksList[index]);
                    else if (tasksList[index].TaskMode == Constants.TaskMode.SECLOCK_PRODUCT_UPDATE)
                        SpiderThread.Add(new ParameterizedThreadStart(SecLockSpider.Update), tasksList[index]);
                }
                //Utility.ApplicationLog("Tasks Processed");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to process tasks");
                Utility.ErrorLog(ex, null);
                if (Settings.GetValue("MailErrors") == true)
                    Utility.ErrorLog(ex, null, Constants.EmailErrorFile);
                //treeCatagory.Enabled = true;
                //btnStart.Text = "Start";
                //btnStart.Enabled = true;
                //rbtnCrawl.Enabled = rbtnUpdate.Enabled = true;
                //Utility.SendAlertMail("Login Failed", "Login Failed for http://adiglobal.us");
                ChangeButtonText(btnStart, "Start");
                ChangeControlState(btnStart, true);
                ChangeControlState(cmbAdiMode, true);
                ChangeControlState(treeCatagory, true);
                ChangeControlState(chkAdiDownloadImage, true);
                ChangeControlState(btnTimer, true);
                ChangeControlState(chkAdiIncognito, true);
            }
        }

        private void startWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusBarSyncTimer.Enabled = true;
            timerTasks.Enabled = true;
        }

        #region [Delegated & Events]
        
        #endregion

        #endregion

        #endregion

        #region [Spider Scheduler]
        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (btnTimer.Text == "Timer Start")
            {
                timerSpider.Enabled = true;
                btnTimer.Text = "Timer Stop";
            }
            else
            {
                timerSpider.Enabled = false;
                btnTimer.Text = "Timer Start";
            }
        }

        private void timerSpider_Tick(object sender, EventArgs e)
        {
            try
            {              
                string DayPart = System.DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
                if (System.DateTime.Now.Hour.ToString() == Settings.GetValue("SpiderTime").ToString() && System.DateTime.Now.Minute.ToString() == Settings.GetValue("SpiderMin").ToString())
                {
                    if (DayPart.ToUpper() == Settings.GetValue("SpiderDay").ToString() || "ALL" == Settings.GetValue("SpiderDay").ToString())
                    {
                        timerSpider.Enabled = false;
                        btnTimer.Text = "Timer Start";
                        btnStart.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

        }
        #endregion

        #region [Ping Rate Timer]
        private void timerPingRate_Tick(object sender, EventArgs e)
        {
            pingCount = 1;
        }
        #endregion

        #region[Task Referesh Timer]
        private void timerTasks_Tick(object sender, EventArgs e)
        {
            ReloadTaskDetails();
        }
        #endregion

        #region [AdiGlobal]

        #region [ Adi Mode ]
        private void cmbAdiMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            String Text = cmbAdiMode.SelectedItem.ToString();
            treeCatagory.Nodes.Clear();
            treeViewBrands.Nodes.Clear();
            treeViewAdiProducts.Nodes.Clear();
            switch(Text)
            {
                case "Crawl":
                case "Clearance Zone":
                case "Hot Deals":
                case "Online Specials":
                case "Sale Center":
                    treeCatagory.Enabled = true;
                    treeViewBrands.Enabled = true;
                    treeViewAdiProducts.Enabled = false;
                    LoadAllCategoryBrands();
                    tabAdiGlobal.SelectedTab = tabPageAdiCategory;
                    chkAdiIncognito.Checked = true;
                    chkAdiIncognito.Enabled = true;
                    break;
                case "In Stock":
                    treeCatagory.Enabled = true;
                    treeViewBrands.Enabled = true;
                    treeViewAdiProducts.Enabled = false;
                    LoadAllCategoryBrands();
                    tabAdiGlobal.SelectedTab = tabPageAdiCategory;
                    chkAdiIncognito.Checked = false;
                    chkAdiIncognito.Enabled = false;
                    break;
                case "Update":
                    treeCatagory.Enabled = false;
                    treeViewBrands.Enabled = false;
                    treeViewAdiProducts.Enabled = true;
                    LoadUpdateProducts();
                    tabAdiGlobal.SelectedTab = tabPageAdiProducts;
                    chkAdiIncognito.Checked = false;
                    chkAdiIncognito.Enabled = false;
                    break;
            }
        }
        #endregion

        #region [ Categories ]
        private void LoadCategory()
        {
            List<AdiCategory> oCategories = AdiSpider.LoadCatagory(true);
            AddToCategory(null, oCategories, Constants.TaskMode.ADI_CRAWL);
        }

        private void LoadClearanceZoneCategory()
        {
            List<AdiCategory> oCategories = AdiSpider.LoadClearanceZoneCategories(true);
            AddToCategory(null, oCategories, Constants.TaskMode.ADI_CLEARANCE_ZONE);
        }

        private void LoadHotDealsCategory()
        {
            List<AdiCategory> oCategories = AdiSpider.LoadHotDealsCategories(true);
            AddToCategory(null, oCategories, Constants.TaskMode.ADI_HOT_DEALS);
        }

        private void LoadOnlineSpecialsCategory()
        {
            List<AdiCategory> oCategories = AdiSpider.LoadOnlineSpecialsCategories(true);
            AddToCategory(null, oCategories, Constants.TaskMode.ADI_ONLINE_SPECIALS);
        }

        private void LoadSaleCenterCategory()
        {
            List<AdiCategory> oCategories = AdiSpider.LoadSaleCenterCategories(true);
            AddToCategory(null, oCategories, Constants.TaskMode.ADI_SALE_CENTER);
        }
        private void LoadInStockCategory()
        {
            List<AdiCategory> oCategories = AdiSpider.LoadInStockCategories(true);
            AddToCategory(null, oCategories, Constants.TaskMode.ADI_IN_STOCK);
        }
        #endregion

        #region [CATEGORY/BRAND TREE]

        #region [Catagory Tree]
        private void AddToCategory(TreeNode tNode, List<AdiCategory> oCategories, String taskMode)
        {
            TaskDetailManager mgr = new TaskDetailManager(Constants.ConnectionString);
            foreach (AdiCategory c in oCategories)
            {
                TreeNode tn = new TreeNode();
                tn.Text = c.DisplayName;
                tn.Tag = c.Value;
                tn.Checked = (mgr.GetDataByTaskDetail(-1, Constants.SiteName.ADIGLOBAL, taskMode, Constants.TaskType.ADI_CATEGORY, c.Value).Count() == 1);
                if (ReferenceEquals(tNode, null))
                    //treeCatagory.Nodes.Add(tn);
                    AddTreeRootNode(treeCatagory, tn);
                else
                    //tNode.Nodes.Add(tn);
                    AddTreeSubNode(tNode, tn);
                if (c.SubCategory.Count > 0)
                    AddToCategory(tn, c.SubCategory, taskMode);
            }
        }

        private void treeCatagory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
            TaskFromTreeViewNode(e.Node, e.Node.Checked, cmbAdiMode.Text, Constants.TaskType.ADI_CATEGORY, Constants.SiteName.ADIGLOBAL, chkAdiIncognito.Checked, chkAdiDownloadImage.Checked );
            ReloadTaskDetails();
        }
        #endregion

        #region [Brand Tree]
        private void AddToBrand(TreeNode tNode, List<AdiBrand> oBrands, String taskMode)
        {
            TaskDetailManager mgr = new TaskDetailManager(Constants.ConnectionString);
            foreach (AdiBrand b in oBrands)
            {
                TreeNode tn = new TreeNode();
                tn.Text = b.DisplayName;
                tn.Tag = b.Value;
                tn.Checked = (mgr.GetDataByTaskDetail(-1, Constants.SiteName.ADIGLOBAL, taskMode, Constants.TaskType.ADI_BRAND, b.Value).Count() == 1);
                if (ReferenceEquals(tNode, null))
                    //treeCatagory.Nodes.Add(tn);
                    AddTreeRootNode(treeViewBrands, tn);
                else
                    //tNode.Nodes.Add(tn);
                    AddTreeSubNode(tNode, tn);
            }
        }

        private void treeViewBrands_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
            TaskFromTreeViewNode(e.Node, e.Node.Checked, cmbAdiMode.Text, Constants.TaskType.ADI_BRAND, Constants.SiteName.ADIGLOBAL, chkAdiIncognito.Checked, chkAdiDownloadImage.Checked);
            ReloadTaskDetails();
        }
        #endregion

        #region [ Products ]
        private void LoadAdiUpdateProductsFromDb()
        {
            List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table> updateProducts = AdiSpider.GetUpdateProducts();
            foreach (var item in updateProducts)
            {
                TreeNode tn = new TreeNode();
                tn.Text = String.Format("{0} | {1}", item.AID_PART, item.VDR_IT_DSC);
                tn.Tag = item.AID_PART;
                //tn.Checked = true;
                AddTreeRootNode(treeViewAdiProducts, tn);
            }
        }

        private void treeViewAdiProducts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TaskFromTreeViewNode(e.Node, e.Node.Checked, cmbAdiMode.Text, Constants.TaskType.ADI_UPDATE, Constants.SiteName.ADIGLOBAL, chkAdiIncognito.Checked, chkAdiDownloadImage.Checked);
            ReloadTaskDetails();
        }

        private void chkAdiProductsCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            BackgroundWorker adiProductsTreeNodeCheckBackgroundWorker = new BackgroundWorker();
            adiProductsTreeNodeCheckBackgroundWorker.DoWork +=adiProductsTreeNodeCheckBackgroundWorker_DoWork;
            adiProductsTreeNodeCheckBackgroundWorker.RunWorkerCompleted +=adiProductsTreeNodeCheckBackgroundWorker_RunWorkerCompleted;
            chkAdiProductsCheckAll.Enabled = false;
            adiProductsTreeNodeCheckBackgroundWorker.RunWorkerAsync();
        }

        private void adiProductsTreeNodeCheckBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            chkAdiProductsCheckAll.Enabled = true;
        }

        private void adiProductsTreeNodeCheckBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (TreeNode tn in treeViewAdiProducts.Nodes)
                CheckNode(tn, chkAdiProductsCheckAll.Checked);
        }
        #endregion

        #region [ Tree General ]
        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;
            }
        }

        private void TaskFromTreeViewNode(TreeNode node, Boolean isChecked, string taskMode, string taskType, string site, bool incognito, bool downloadImages)
        {
            if (node.Nodes.Count > 0)
                return;

            TaskDetail task;
            switch (site)
            {
                case Constants.SiteName.ADIGLOBAL:
                    #region [ADIGLOBAL]
                    switch (taskMode)
                    {
                        case "Crawl":
                            taskMode = Constants.TaskMode.ADI_CRAWL;
                            break;
                        case "Update":
                            taskMode = Constants.TaskMode.ADI_UPDATE;
                            break;
                        case "Clearance Zone":
                            taskMode = Constants.TaskMode.ADI_CLEARANCE_ZONE;
                            break;
                        case "Hot Deals":
                            taskMode = Constants.TaskMode.ADI_HOT_DEALS;
                            break;
                        case "Online Specials":
                            taskMode = Constants.TaskMode.ADI_ONLINE_SPECIALS;
                            break;
                        case "Sale Center":
                            taskMode = Constants.TaskMode.ADI_SALE_CENTER;
                            break;
                        case "In Stock":
                            taskMode = Constants.TaskMode.ADI_IN_STOCK;
                            break;
                    }
                    #endregion
                    break;
                case Constants.SiteName.SECLOCK:
                    #region [ SECLOCK ]
                    #endregion
                    break;
            }
            

            var items = listViewScheduleTasks.SelectedItems;
            Int64 TaskHeaderID = Convert.ToInt64((items.Count == 1) ? items[0].Text : "-1");

            if (isChecked == true)
            {
                
                task = new TaskDetail();
                task.TaskID = 0;
                task.TaskHeaderID = TaskHeaderID;
                task.TaskNameText = node.Text;
                task.TaskNameValue = node.Tag.ToString();
                task.TaskStatusText = Constants.OPEN_TEXT;
                task.TaskStatus = TaskDetailStatus.Open;
                task.DownloadImages = downloadImages;
                task.IncognitoMode = incognito;
                task.TaskType = taskType;
                task.TaskMode = taskMode;
                task.TaskSite = site;
                task.TaskID = new TasksScheduler().SaveTaskDetail(task);
            }
            else
            {
                RemoveEntry(node.Tag.ToString(), taskMode, taskType, site);
            }
        }

        private void RemoveEntry(string lValue, string taskMode, string taskType, string Site)
        {

            foreach (ListViewItem item in listViewThreads.Items)
            {
                TaskDetail td = GetTaskFromListViewItem(item);
                //if (item.SubItems[1].Text == lValue)
                if (td.TaskNameValue == lValue && td.TaskMode == taskMode && td.TaskType == taskType && td.TaskSite == Site)
                {
                    //Int64 taskId = Convert.ToInt64(item.SubItems[0].Text);
                    //new TasksScheduler().DeleteTaskDetail(taskId);
                    new TasksScheduler().DeleteTaskDetail(td.TaskID);
                    listViewThreads.Items.Remove(item);
                }
            }


        }
        #endregion

        #endregion

        #region [Load Brands]
        private void LoadBrands()
        {
            List<AdiBrand> oBrands = AdiSpider.GetAllBrands();
            treeViewBrands.Nodes.Clear();
            AddToBrand(null, oBrands, Constants.TaskMode.ADI_CRAWL);
        }

        private void LoadClearanceZoneBrands()
        {
            List<AdiBrand> oBrands = AdiSpider.GetClearanceZoneBrands(true);
            AddToBrand(null, oBrands, Constants.TaskMode.ADI_CLEARANCE_ZONE);
        }

        private void LoadHotDealsBrands()
        {
            List<AdiBrand> oBrands = AdiSpider.GetHotDealsBrands(true);
            AddToBrand(null, oBrands, Constants.TaskMode.ADI_HOT_DEALS);
        }

        private void LoadOnlineSpecialsBrands()
        {
            List<AdiBrand> oBrands = AdiSpider.GetOnlineSpecialsBrands(true);
            AddToBrand(null, oBrands, Constants.TaskMode.ADI_ONLINE_SPECIALS);
        }

        private void LoadSaleCenterBrands()
        {
            List<AdiBrand> oBrands = AdiSpider.GetSaleCenterBrands(true);
            AddToBrand(null, oBrands, Constants.TaskMode.ADI_SALE_CENTER);
        }

        private void LoadInStockBrands()
        {
            List<AdiBrand> oBrands = AdiSpider.GetInStockBrands(true);
            AddToBrand(null, oBrands, Constants.TaskMode.ADI_IN_STOCK);
        }
        #endregion

        #region [Load Categories/Brand]
        Loading l;
        Boolean LoadingCategory, LoadingBrand;
        private void LoadAllCategoryBrands()
        {
            // Initialize the dialog that will contain the progress bar
            #region [Load Catagory]
            BackgroundWorker brandLoaderWorker = new BackgroundWorker();
            brandLoaderWorker.RunWorkerCompleted += brandLoaderWorker_RunWorkerCompleted;
            BackgroundWorker categoryLoaderWorker = new BackgroundWorker();
            categoryLoaderWorker.RunWorkerCompleted += categoryLoaderWorker_RunWorkerCompleted;

            String Text = cmbAdiMode.SelectedItem.ToString();
            switch (Text)
            {
                case "Crawl":
                    categoryLoaderWorker.DoWork += categoryLoaderWorker_DoWork;
                    brandLoaderWorker.DoWork += brandLoaderWorker_DoWork;
                    break;
                case "Update":
                    break;
                case "Clearance Zone":
                    categoryLoaderWorker.DoWork += clearanceZoneCategoryLoaderWorker_DoWork;
                    brandLoaderWorker.DoWork += clearanceZoneBrandLoaderWorker_DoWork;
                    break;
                case "Hot Deals":
                    categoryLoaderWorker.DoWork += hotDealsCategoryLoaderWorker_DoWork;
                    brandLoaderWorker.DoWork += hotDealsBrandLoaderWorker_DoWork;
                    break;
                case "Online Specials":
                    categoryLoaderWorker.DoWork += onlineSpecialsCategoryLoaderWorker_DoWork;
                    brandLoaderWorker.DoWork += onlineSpecialsBrandLoaderWorker_DoWork;
                    break;
                case "Sale Center":
                    categoryLoaderWorker.DoWork += saleCenterCategoryLoaderWorker_DoWork;
                    brandLoaderWorker.DoWork += saleCenterBrandLoaderWorker_DoWork;
                    break;
                case "In Stock":
                    categoryLoaderWorker.DoWork += categoryLoaderWorker_DoWork;
                    brandLoaderWorker.DoWork += brandLoaderWorker_DoWork;
                    //categoryLoaderWorker.DoWork += inStockCategoryLoaderWorker_DoWork;
                    //brandLoaderWorker.DoWork += inStockBrandLoaderWorker_DoWork;
                    break;
            }


            l = new Loading();
            ShowForm(l);
            LoadingCategory = true;
            LoadingBrand = true;
            categoryLoaderWorker.RunWorkerAsync();
            brandLoaderWorker.RunWorkerAsync();
            #endregion
        }


        #region [Worker Complete]
        private void categoryLoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingCategory = false;
            if (!LoadingBrand && !LoadingCategory)
                CloseForm(l);
        }

        private void brandLoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingBrand = false;
            if (!LoadingBrand && !LoadingCategory)
                CloseForm(l);
        }
        #endregion

        #region [All Category/Brand Workers]
        private void categoryLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadCategory();
            }
            catch { }
        }

        private void brandLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadBrands();
                //String Text = cmbAdiMode.SelectedItem.ToString();
                //switch (Text)
                //{
                //    case "Crawl":
                //        LoadBrands();
                //        break;
                //    case "Update":
                //        break;
                //    case "Clearance Zone":
                //        LoadClearanceZoneBrands();
                //        break;
                //    case "Hot Deals":
                //        LoadHotDealsBrands();
                //        break;
                //    case "Online Specials":
                //        LoadOnlineSpecialsBrands();
                //        break;
                //    case "Sale Center":
                //        LoadSaleCenterBrands();
                //        break;
                //}
            }
            catch { }
        }
        #endregion

        #region [ Clearance Zone Category/Brand Worker ]
        private void clearanceZoneCategoryLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadClearanceZoneCategory();
            }
            catch { }
        }

        private void clearanceZoneBrandLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadClearanceZoneBrands();
            }
            catch { }
        }
        #endregion

        #region [ Hot Deals Category/Brand Worker ]
        private void hotDealsCategoryLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadHotDealsCategory();
            }
            catch { }
        }

        private void hotDealsBrandLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadHotDealsBrands();
            }
            catch { }
        }
        #endregion

        #region [ Online Specials Category/Brand Worker ]
        private void onlineSpecialsCategoryLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadOnlineSpecialsCategory();
            }
            catch { }
        }

        private void onlineSpecialsBrandLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadOnlineSpecialsBrands();
            }
            catch { }
        }
        #endregion

        #region [ Sale Center Category/Brand Worker ]
        private void saleCenterCategoryLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadSaleCenterCategory();
            }
            catch { }
        }

        private void saleCenterBrandLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadSaleCenterBrands();
            }
            catch { }
        }
        #endregion

        #region [ InStock Category/Brand Worker ]
        private void inStockBrandLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadInStockBrands();
            }
            catch { };
        }

        private void inStockCategoryLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadInStockCategory();
            }
            catch { };
        }
        #endregion
        #endregion

        #region [ Load update Products ]
        public void LoadUpdateProducts()
        {
            BackgroundWorker updateProductLoaderWorker = new BackgroundWorker();
            updateProductLoaderWorker.RunWorkerCompleted +=updateProductLoaderWorker_RunWorkerCompleted;
            updateProductLoaderWorker.DoWork +=updateProductLoaderWorker_DoWork;
            l = new Loading();
            ShowForm(l);
            LoadingCategory = true;
            LoadingBrand = true;
            updateProductLoaderWorker.RunWorkerAsync();
        }

        private void updateProductLoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseForm(l);
        }

        private void updateProductLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadAdiUpdateProductsFromDb();
        }
        #endregion

        private void toolBarAdiGlobal_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Name)
            {
                case "toolBarButtonAdiPriority":
                    Form form = new ProductPriority();
                    ShowForm(form);
                    break;
                case "toolBarButtonAdiReload":
                    var result = MessageBox.Show("Reload all Categories and Brands from AdiGlobal? It may take a long time to load all Categories and Brands.", "Reload Categories and Brands", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        AdiSpider.ClearAllBrands();
                        AdiSpider.ClearAllCategories();
                        LoadAllCategoryBrands();
                    }
                    break;
                case "toolBarButtonAdiScheduler":
                    MessageBox.Show("Not yet implemented.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
        
        #endregion

        #region [ SecLock ]

        #region [ Manufacturer / Category / Products Tree ]

        #region [ Manufacturer ]
        #region  [ Load Manufactuerer ]
        public void LoadSecLockManufactueres()
        {
            BackgroundWorker seclockManufacturerWorker = new BackgroundWorker();
            seclockManufacturerWorker.DoWork += seclockManufacturerWorker_DoWork;
            seclockManufacturerWorker.RunWorkerAsync();
        }

        private void seclockManufacturerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var manufacturers = new SecLockSpider().GetManufacturers();
            for (int i = 0; i < manufacturers.Count(); i++)
            {
                var manufacturer = manufacturers[i];
                TreeNode node = new TreeNode();
                node.Tag = manufacturer.Code;
                node.Text = manufacturer.Name;
                AddTreeRootNode(tvSecLockManufacturers, node);
            }
        }
        #endregion
        

        #region [ Manufacturer Check ]
        private void tvSecLockManufacturers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
            TaskFromTreeViewNode(e.Node, e.Node.Checked, Constants.TaskMode.SECLOCK_MANUFACTURER_CRAWL, Constants.TaskType.SECLOCK_CRAWL, Constants.SiteName.SECLOCK, chkSeclockIncognito.Checked, chkSeclockDownloadImages.Checked);
            ReloadTaskDetails();
        }
        #endregion

        #region [ Before Expand ]
        private void tvSecLockManufacturers_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }
        #endregion
        #endregion

        #region [ Categories ]

        #region [ Load categories ]
        public void LoadSecLockCategories()
        {
            BackgroundWorker SecLockCategoryWorker = new BackgroundWorker();
            SecLockCategoryWorker.DoWork += SecLockCategoryWorker_DoWork;
            SecLockCategoryWorker.RunWorkerAsync();
        }

        private void SecLockCategoryWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var categories = new SecLockSpider().GetCategories();
            for (int i = 0; i < categories.Count(); i++)
            {
                var category = categories[i];
                TreeNode tn = new TreeNode();
                tn.Tag = category.Code ;
                tn.Text = category.Name;
                //tvSecLockCategories.Nodes.Add(category.Code, category.Name);
                AddTreeRootNode(tvSecLockCategories, tn);
                //SecLockSpider.GetCategoryProducts(category);
            }
        }
        #endregion

        #region [ Category Click ]
        private void tvSecLockCategories_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
            TaskFromTreeViewNode(e.Node, e.Node.Checked, Constants.TaskMode.SECLOCK_CATEGORY_CRAWL, Constants.TaskType.SECLOCK_CRAWL, Constants.SiteName.SECLOCK, chkSeclockIncognito.Checked, chkSeclockDownloadImages.Checked);
            ReloadTaskDetails();
        }
        #endregion

        #endregion

        #region [ Update Product ]
        public void LoadSecLockUpdateProducts()
        {
            var products = new SecLockSpider().GetProducts();
            foreach (var p in products)
            {
                TreeNode tn = new TreeNode();
                tn.Text = p.Name;
                tn.Tag = p.Code;
                AddTreeRootNode(tvsecLockProducts, tn);
            }
        }

        public void LoadSecLockCSVUpdateProducts()
        {
            var products = new SecLockSpider().GetUpdateProducts();
            foreach (var p in products)
            {
                TreeNode tn = new TreeNode();
                tn.Text = p.Name;
                tn.Tag = p.Code;
                AddTreeRootNode(tvSecLockCSVProduct, tn);
            }

        }
        private void tvsecLockProducts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
            TaskFromTreeViewNode(e.Node, e.Node.Checked, Constants.TaskMode.SECLOCK_PRODUCT_UPDATE, Constants.TaskType.SECLOCK_UPDATE, Constants.SiteName.SECLOCK, chkSeclockIncognito.Checked, chkSeclockDownloadImages.Checked);
            ReloadTaskDetails();
        }

        #region [ Update SecLock CSV Product ]
        private void tvsecLockCSVProducts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ReloadTaskDetails();
        }
        private void btnSecLockCSVStart_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in tvSecLockCSVProduct.Nodes)
            {
                if (tn.Checked)
                    TaskFromTreeViewNode(tn, tn.Checked, Constants.TaskMode.SECLOCK_PRODUCT_UPDATE, Constants.TaskType.SECLOCK_UPDATE, Constants.SiteName.SECLOCK, chkSeclockCSVIncognito.Checked, chkSeclockCSVDownloadImages.Checked);
            }
            ReloadTaskDetails();
        }
        #endregion

        #endregion

        #region [SecLock Manufanufacturers SelectAll ]
        private void chkSecLockManufanufacturersSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            BackgroundWorker bwSecLockManufanufacturersSelectAll = new BackgroundWorker();
            bwSecLockManufanufacturersSelectAll.DoWork += bwSecLockManufanufacturersSelectAll_DoWork;
            bwSecLockManufanufacturersSelectAll.RunWorkerAsync();
        }

        private void bwSecLockManufanufacturersSelectAll_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (TreeNode tn in tvSecLockManufacturers.Nodes)
            {
                CheckNode(tn, chkSecLockManufanufacturersSelectAll.Checked);
            }
        }
        #endregion

        #region [ SecLock Categories SelectAll ]
        private void chkSecLockCategoriessSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            BackgroundWorker bwSecLockCategoriessSelectAll = new BackgroundWorker();
            bwSecLockCategoriessSelectAll.DoWork += chkSecLockCategoriessSelectAll_DoWork;
            bwSecLockCategoriessSelectAll.RunWorkerAsync();
        }
        private void chkSecLockCategoriessSelectAll_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (TreeNode tn in tvSecLockCategories.Nodes)
            {
                CheckNode(tn, chkSecLockCategoriessSelectAll.Checked);
            }
        }
        #endregion

        #region [ SecLock Products SelectAll ]
        private void chkSecLockProductsSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            BackgroundWorker bwSecLockProductsSelectAll = new BackgroundWorker();
            bwSecLockProductsSelectAll.DoWork += chkSecLockProductsSelectAll_DoWork;
            bwSecLockProductsSelectAll.RunWorkerAsync();

        }

        private void chkSecLockProductsSelectAll_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (TreeNode tn in tvsecLockProducts.Nodes)
            {
                CheckNode(tn, chkSecLockProductsSelectAll.Checked);
            }
        }
        #endregion

        #region [ SecLockCSV  Products SelectAll ]
        private void chkSecLockCSVProductSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            BackgroundWorker bwSecLockProductsSelectAll = new BackgroundWorker();
            bwSecLockProductsSelectAll.DoWork += chkSecLockCSVProductSelectAll_DoWork;
            bwSecLockProductsSelectAll.RunWorkerAsync();

        }

        private void chkSecLockCSVProductSelectAll_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (TreeNode tn in tvSecLockCSVProduct.Nodes)
            {
                CheckNode(tn, chkSecLockCSVProductSelectAll.Checked);
            }
        }
        #endregion


        #endregion

        #region [ SecLock Toolbar ]
        private void toolBarSecLock_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Name)
            {
                case "toolBarButtonSecLockRefresh":
                    BackgroundWorker bwReloadSecLockmanufacturers = new BackgroundWorker();
                    bwReloadSecLockmanufacturers.DoWork += bwReloadSecLockManufacturers_DoWork;

                    BackgroundWorker bwReloadSecLockCategories = new BackgroundWorker();
                    bwReloadSecLockCategories.DoWork += bwReloadSecLockCategories_DoWork;

                    bwReloadSecLockmanufacturers.RunWorkerAsync();
                    bwReloadSecLockCategories.RunWorkerAsync();

                    new SecLockSpider().ReloadAllManufacturers();
                    new SecLockSpider().ReloadallCategories();
                    break;
            }
        }

        private void bwReloadSecLockManufacturers_DoWork(object sender, DoWorkEventArgs e)
        {
            ClearTreeNodes(tvSecLockManufacturers);
            var manufacturers = new SecLockSpider().ReloadAllManufacturers();
            for (int i = 0; i < manufacturers.Count(); i++)
            {
                var manufacturer = manufacturers[i];
                TreeNode node = new TreeNode();
                node.Tag = manufacturer.Code;
                node.Text = manufacturer.Name;

                AddTreeRootNode(tvSecLockManufacturers, node);
            }
        }

        private void bwReloadSecLockCategories_DoWork(object sender, DoWorkEventArgs e)
        {
            ClearTreeNodes(tvSecLockCategories);
            var categories = new SecLockSpider().GetCategories();
            for (int i = 0; i < categories.Count(); i++)
            {
                var category = categories[i];
                if (category.Name.Trim().ToUpper() == "ALL CATEGORIES")
                    continue;
                TreeNode tn = new TreeNode();
                tn.Tag = category.Code;
                tn.Text = category.Name;
                AddTreeRootNode(tvSecLockCategories, tn);
            }
        }
        #endregion

        #endregion

        #region [ Settings Tab ]

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
                Settings.SetValue("ScheduleDay", typeof(String), String.Join(",", str));
                Settings.SetValue("ScheduleTime", typeof(int), cmbSHour.SelectedItem);
                Settings.SetValue("ScheduleEndTime", typeof(int), cmbSEndHour.SelectedItem);
                Settings.SetValue("SpiderDay", typeof(String), cmbSpiderDay.SelectedItem);
                Settings.SetValue("SpiderTime", typeof(int), cmbSpiderHour.SelectedItem);
                Settings.SetValue("SpiderMin", typeof(int), cmbSpiderMin.SelectedItem);
                Settings.SetValue("WebSpiderDB", typeof(String), txtcrawlDB.Text);
                Settings.SetValue("FinalTable", typeof(String), txtUpdateDB.Text);
                Settings.SetValue("PingRate", typeof(int), txtPing.Text);

                Settings.SetValue("CrawlImageFolder", typeof(String), txtCrawlImageFolder.Text);
                Settings.SetValue("UpdateImageFolder", typeof(String), txtUpdateImageFolder.Text);
                Settings.SetValue("DocFolder", typeof(String), txtDocFolder.Text);

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
                Settings.SetValue("ADILoginPage", typeof(String), txtAdiGlobalLoginPage.Text);
                Settings.SetValue("ADIUsername", typeof(String), txtAdiGlobalUsername.Text);
                Settings.SetValue("ADIPassword", typeof(String), txtAdiGlobalPassword.Text);
                Settings.SetValue("ADIProductDefaultUpdateInterval", typeof(Int32), Convert.ToInt32(txtAdiGlobalProductUpdateInterval.Text));
                Settings.SetValue("AdiCategoryUpdateInterval", typeof(Int32), Convert.ToInt32(txtAdiGlobalCatagoryUpdateInterval.Text));
                Settings.SetValue("AdiImagePrefix", typeof(String), txtAdiGlobalImagePrefix.Text);
                #endregion

                #region [ Sec Lock ]
                Settings.SetValue("SecLockLoginPage", typeof(String), txtSecLockLoginPage.Text);
                Settings.SetValue("SecLockUsername", typeof(String), txtSecLockUserName.Text);
                Settings.SetValue("SecLockPassword", typeof(String), txtSecLockPassword.Text);
                Settings.SetValue("SecLockImagePrefix", typeof(String), txtSecLockImagePrefix.Text);
                #endregion

                #region [ Tri-ed Lock ]
                Settings.SetValue("TriedLoginPage", typeof(String), txtTriedLoginPage.Text);
                Settings.SetValue("TriedUsername", typeof(String), txtTriedUserName.Text);
                Settings.SetValue("TriedPassword", typeof(String), txtTriedPassword.Text);
                Settings.SetValue("TriedImagePrefix", typeof(String), txtTriedImagePrefix.Text);
                #endregion


                Settings.Save();
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Couldn't save application settings");
                MessageBox.Show(String.Format("Error saving settings!\n{0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            try
            {
                //Utility.ApplicationLog("Loading application settings");
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

                String[] str = ((String)Settings.GetValue("ScheduleDay").ToString()).Split(',');
                for (int index = 0; index < lstSDay.Items.Count; index++)
                {
                    String value = (String)lstSDay.Items[index];
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
                txtDocFolder.Text = Settings.GetValue("DocFolder");
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
                txtAdiGlobalLoginPage.Text = Settings.GetValue("ADILoginPage");
                txtAdiGlobalUsername.Text = Settings.GetValue("ADIUsername");
                txtAdiGlobalPassword.Text = Settings.GetValue("ADIPassword");
                txtAdiGlobalProductUpdateInterval.Text = Settings.GetValue("ADIProductDefaultUpdateInterval").ToString();
                txtAdiGlobalCatagoryUpdateInterval.Text = Settings.GetValue("AdiCategoryUpdateInterval").ToString();
                txtAdiGlobalImagePrefix.Text = Settings.GetValue("AdiImagePrefix");
                #endregion

                #region [ Sec Lock ]
                txtSecLockLoginPage.Text = Settings.GetValue("SecLockLoginPage");
                txtSecLockUserName.Text = Settings.GetValue("SecLockUsername");
                txtSecLockPassword.Text = Settings.GetValue("SecLockPassword");
                //txtSecLockProductUpdateInterval.Text = Settings.GetValue("SecLockProductDefaultUpdateInterval").ToString();
                //txtSecLockCatagoryUpdateInterval.Text = Settings.GetValue("SecLockCategoryUpdateInterval").ToString();
                txtSecLockImagePrefix.Text = Settings.GetValue("SecLockImagePrefix");
                #endregion

                #region [ Tri_ed ]
                txtTriedLoginPage.Text = Settings.GetValue("TriedLoginPage");
                txtTriedUserName.Text = Settings.GetValue("TriedUsername");
                txtTriedPassword.Text = Settings.GetValue("TriedPassword");
                txtTriedImagePrefix.Text = Settings.GetValue("TriedImagePrefix");
                #endregion


                //Utility.ApplicationLog("Application settings loaded sucessfully");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Error loading application settings");
            }
        }

        private void chkCaching_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCaching.Checked)
                txtCacheDuration.Enabled = chkCaching.Checked;
            else
            {
                var result = MessageBox.Show(this, "This will delete all cached data. Continue?", "Disable cache?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    new Cache().Clear();
                }
            }
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

        private void btnDocFolderBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder(txtDocFolder);
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

        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            #region [General]
            if (chkCaching.Checked)
            {
                try
                {
                    Convert.ToInt32(txtCacheDuration.Text);
                }
                catch
                {
                    ShowValidationMessage("Cache Duration is missing");
                    return;
                }
            }

            if (chkErrorLog.Checked && txtErrorFileName.Text.Length == 0)
            {
                ShowValidationMessage("Error File Name is missing");
                return;
            }

            try
            {
                Convert.ToInt32(txtRetryCount.Text);
            }
            catch
            {
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
            if (txtAdiGlobalLoginPage.Text.Length == 0)
            {
                ShowValidationMessage("ADIGlobal Login Page is required");
                return;
            }
            if (txtAdiGlobalUsername.Text.Length == 0)
            {
                ShowValidationMessage("ADIGlobal username is required");
                return;
            }
            if (txtAdiGlobalPassword.Text.Length == 0)
            {
                ShowValidationMessage("ADIGlobal password is required");
                return;
            }
            try
            {
                Convert.ToInt32(txtAdiGlobalProductUpdateInterval.Text);
            }
            catch
            {
                ShowValidationMessage("ADI Product update interval is missing or invalid");
                return;
            }
            try
            {
                Convert.ToInt32(txtAdiGlobalCatagoryUpdateInterval.Text);
            }
            catch
            {
                ShowValidationMessage("ADI Catagory update interval is missing or invalid");
                return;
            }

            #endregion

            #region [ Sec Lock ]
            Settings.SetValue("SecLockLoginPage", typeof(String), txtSecLockLoginPage.Text);
            Settings.SetValue("SecLockUsername", typeof(String), txtSecLockUserName.Text);
            Settings.SetValue("SecLockPassword", typeof(String), txtSecLockPassword.Text);
            //txtSecLockProductUpdateInterval.Text = Settings.GetValue("SecLockProductDefaultUpdateInterval").ToString();
            //txtSecLockCatagoryUpdateInterval.Text = Settings.GetValue("SecLockCategoryUpdateInterval").ToString();
            Settings.SetValue("SecLockImagePrefix", typeof(String), txtSecLockImagePrefix.Text);
            #endregion

            SaveSettings();
            MessageBox.Show("Settings saved!", "WebSpider", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region [ Table Generation ]
        private void btnGenerateAdiCrawlSchema_Click(object sender, EventArgs e)
        {
            String FileName = Settings.GetValue("WebSpiderDB");
            String MessageText = WebSpiderTableGenerator.Generate(FileName);
            MessageBox.Show(this, MessageText, "Table Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGenerateAdiUpdateSchema_Click(object sender, EventArgs e)
        {
            String FileName = Settings.GetValue("FinalTable");
            //String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));
            String MessageText = UpdateSpiderTableGenerator.Generate(FileName);
            MessageBox.Show(this, MessageText, "Table Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        
        #endregion

        #region [Export]
        System.Timers.Timer exportTimer = new System.Timers.Timer();

        private void InitExport()
        {
            exportTimer.Interval = 100;
            exportTimer.Elapsed += exportTimer_Elapsed;
            exportTimer.Start();
        }

        private void exportTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            exportTimer.Stop();
            try
            {
                AdiSpider.Export(ref toolStripStatusExport);
                SecLockSpider.Export(ref toolStripStatusExport);
            }
            catch { }
            finally
            {
                exportTimer.Start();
            }
        }
        #endregion

        #region [ Scheduler ]
        public void InitScheduler()
        {
            LoadScheduleHeaders();
            listViewScheduleTasks.Items[0].Selected = true;
        }

        private void LoadScheduleHeaders(Boolean DeleteAndReload = false)
        {
            if (DeleteAndReload)
            {
                for (int i = 1; i < listViewScheduleTasks.Items.Count; i++)
                    listViewScheduleTasks.Items[i].Remove();
            }
            List<TaskHeader> headers = new TaskHeaderManager(Constants.ConnectionString).GetData();
            foreach (TaskHeader h in headers)
            {
                SetTaskScheduleHeader(h);
            }
        }

        private void SetTaskScheduleHeader(TaskHeader h)
        {
            String[] text = {
                                h.ScheduleID.ToString(),
                                h.TaskName,
                                h.TaskDescription,
                                h.Site,
                                h.ScheduleFrom.ToShortDateString(),
                                h.TaskRepeat ? "YES" : "NO",
                                String.Format("{0} {1}", h.TaskRepeatInterval, h.TaskRepeatUnit),
                                h.Enabled ? "YES" : "NO",
                                h.LastRun.ToString(),
                                h.NextRun.ToString(),
                            };
            ListViewItem lvItem = listViewScheduleTasks.FindItemWithText(h.ScheduleID.ToString());
            if (lvItem == null)
            {
                lvItem = new ListViewItem(text);
                listViewScheduleTasks.Items.Add(lvItem);
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                    lvItem.SubItems[i].Text = text[i];
            }
        }

        private TaskHeader GetTaskSchedulerHeader(ListViewItem lvItem)
        {
            TaskHeader h = new TaskHeader();
            h.ScheduleID = Convert.ToInt64(lvItem.SubItems[0].Text);
            h.TaskName = lvItem.SubItems[1].Text;
            h.TaskDescription = lvItem.SubItems[2].Text;
            h.Site = lvItem.SubItems[3].Text;
            h.ScheduleFrom = DateTime.Parse(lvItem.SubItems[4].Text);
            h.TaskRepeat = (lvItem.SubItems[5].Text == "YES");
            String itemText = lvItem.SubItems[6].Text;
            h.TaskRepeatInterval = Convert.ToInt32(itemText.Substring(0, itemText.LastIndexOf(' ')));
            h.TaskRepeatUnit = itemText.Substring(itemText.LastIndexOf(' '));
            h.Enabled = (lvItem.SubItems[7].Text == "YES");
            h.LastRun = lvItem.SubItems[8].Text == "" ? (DateTime?) null : Convert.ToDateTime(lvItem.SubItems[8].Text);
            h.NextRun = lvItem.SubItems[9].Text == "" ? (DateTime?) null : Convert.ToDateTime(lvItem.SubItems[9].Text);

            return h;
        }

        private void btnNewSchedule_Click(object sender, EventArgs e)
        {
            ScheduleDetail frm = new ScheduleDetail();
            var val = frm.ShowDialog(this);
            //MessageBox.Show(val.ToString());
            if (val == System.Windows.Forms.DialogResult.OK)
                LoadScheduleHeaders(true);
        }

        private void btnDeleteSchedule_Click(object sender, EventArgs e)
        {
            var items = listViewScheduleTasks.SelectedItems;
            if (items != null && items.Count > 0)
            {
                var result = MessageBox.Show("Do you want to delete the schedule?", "Delete Schedule", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    TaskHeader th = GetTaskSchedulerHeader(items[0]);
                    TasksScheduler mgr = new TasksScheduler();
                    mgr.DeleteTaskHeader(th);
                    LoadScheduleHeaders(true);
                }
            }
        }

        private void listViewScheduleTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (listViewScheduleTasks.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    ListViewItem item = listViewScheduleTasks.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        TaskHeader th = GetTaskSchedulerHeader(item);
                        ScheduleDetail frm = new ScheduleDetail(th.ScheduleID);
                        var val = frm.ShowDialog(this);
                    }
                }
            }
        }

        private void listViewScheduleTasks_MouseClick(object sender, MouseEventArgs e)
        {
        
        }
        #endregion

        private void listViewScheduleTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listViewScheduleTasks.SelectedItems;
            if (item != null && item.Count > 0)
            {
                ReloadTaskDetails(true, Convert.ToInt64(item[0].Text));
                if (item[0].Text == "-1")
                {
                    btnStart.Visible = true;
                    toolBarTasks.Enabled = true;
                }
                else
                {
                    btnStart.Visible = false;
                    toolBarTasks.Enabled = false;
                }
            }
        }
        private void btnCleanDb_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to clean all internal data? This will remove all scrapped data stored inside the application. Make sure all tasks are stopped before you continue. Application will be restarted once data is cleaned.", "WebSpider", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                exportTimer.Stop();
                try
                {
                    GenerateDatabase.Generate();
                    Application.Restart();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exportTimer.Start();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrowseFile(txtcrawlDB);
        }
    }
}
