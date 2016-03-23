using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;
using System.Configuration;
using WebSpider.Objects;
using WebSpider.Core;
using WebSpider;
//using WebSpider.Objects.AdiGlobal;
using WebSpider.Objects.General;
using System.Threading;
using WebSpider.Data.General;

namespace SpiderAlert
{
    public class AdiScheduler
    {
        private static string fileName = Application.StartupPath + "\\Log.txt";
        public static void Start()
        {

        }

        public static void Stop()
        {
        }

        public static void Pause()
        {

        }

        private  bool processing;
        public  void Process()
        {
            if (!processing)
            {
                processing = true;
                try
                {
                    GenerateSchedules();
                    RunAdiSchedules();


                    #region [ backup ]
                    //if (AdiSpider.Login())
                    //{
                    //    LogFile(fileName, "Login Done");
                    //    List<AdiProduct> productsList = AdiSpider.GetAllPriorityProducts();
                    //    LogFile(fileName, "Product Found-" + productsList.Count.ToString());
                    //    for (int index = 0; index < productsList.Count; index++)
                    //    {
                    //        if (productsList[index].LastUpdateDatetime <= System.DateTime.Now.AddDays(-1))
                    //        {
                    //            AdiProduct product = productsList[index];

                    //            //product = oSpider.GetProduct(product);
                    //            //oSpider.SaveProduct(product, false);
                    //            //oSpider.GetProductDetails(product, true);
                    //            double total = AdiSpider.GetProductInventory(product, AdiMode.UPDATE, false);
                    //            if (total <= productsList[index].LeastCount)
                    //            {
                    //                //Send Mail
                    //                Utility.SendAlertMail("Stock Alert", "Product- " + productsList[index].AdiNumber + " Live Stock Quantity- " + total.ToString());
                    //                //Utility.LogFile(fileName, "Mail Send-" + productsList[index].AdiNumber);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    //LogFile("C:\\Users\\ANUP\\Desktop\\WebSpider_20141028A\\WebSpider\\SpiderAlert\\bin\\Debug\\Log.txt", "Login Failed");
                    //    Utility.SendAlertMail("Login Failed", "Login Failed for http://adiglobal.us");
                    //}
                    ////Utility.LogFile(fileName, "Process End");
                    #endregion
                    
                }
                catch(Exception ex) {
                    Utility.ErrorLog(ex, null);
                 //LogFile("C:\\Users\\ANUP\\Desktop\\WebSpider_20141028A\\WebSpider\\SpiderAlert\\bin\\Debug\\Log.txt", ex.ToString());
                }
                processing = false;
            }
        }

        #region [ Run Schedules ]
        private void RunAdiSchedules()
        {
            List<TaskDetail> tasksList = new TasksScheduler().GetPendingSchedules(Constants.SiteName.ADIGLOBAL);
            for (int index = 0; index < tasksList.Count; index++)
            {
                if (tasksList[index].TaskMode == Constants.TaskMode.ADI_CRAWL)
                    AdiSpider.CrawlProduct(tasksList[index]);
                else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_UPDATE)
                    AdiSpider.UpdateProduct(tasksList[index]);
                else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_CLEARANCE_ZONE)
                    AdiSpider.GetClearanzeZone(tasksList[index]);
                else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_HOT_DEALS)
                    AdiSpider.GetHotDeals(tasksList[index]);
                else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_ONLINE_SPECIALS)
                    AdiSpider.GetOnlineSpecials(tasksList[index]);
                else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_SALE_CENTER)
                    AdiSpider.GetSaleCenter(tasksList[index]);
                else if (tasksList[index].TaskMode == Constants.TaskMode.ADI_IN_STOCK)
                    AdiSpider.GetInStockItems(tasksList[index]);
                else
                    AdiSpider.ProcessProductLeastCount();
                TaskheaderUpdate(tasksList[index].TaskHeaderID);
            }
        }
        #endregion

        #region [  Update Header ]
        private void TaskheaderUpdate(Int64 TaskHeaderID)
        {
            TaskHeaderManager headerManager = new TaskHeaderManager(Constants.ConnectionString);
            List<TaskHeader> taskHeaders = headerManager.GetData(TaskHeaderID);
            if (taskHeaders.Count == 1)
            {
                taskHeaders[0].LastRun = taskHeaders[0].NextRun;
                headerManager.Save(taskHeaders[0]);
                headerManager.GenerateScheduleNextRun(TaskHeaderID);
            }
        }
        #endregion

        #region [ Schedule Generator ]
        private void GenerateSchedules()
        {
            new TasksScheduler().GenerateSchedules();
        }
        #endregion

        public static void LogFile(String FileName, String Message)
        {
            if (Message != null && Message.Length > 0)
            {
                StreamWriter writer = File.AppendText(FileName);
                try
                {
                    //String DateFormat = Settings.GetValue("DateFormat").ToString();
                    writer.WriteLine(Message);
                }
                finally
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}
