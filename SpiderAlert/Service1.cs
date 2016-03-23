using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using WebSpider.Core;

namespace SpiderAlert
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            LogFile(Application.StartupPath + "\\SchedulerLog.txt", "Start");
        }

        protected override void OnStart(string[] args)
        {
            LogFile(Application.StartupPath + "\\Log.txt", Application.StartupPath);
        }

        protected override void OnStop()
        {
        }

        private void TimerAlert_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string DayPart = System.DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
                if (int.Parse(System.DateTime.Now.Hour.ToString()) >= int.Parse(Settings.GetValue("ScheduleTime").ToString()) && int.Parse(System.DateTime.Now.Hour.ToString()) <= int.Parse(Settings.GetValue("ScheduleEndTime").ToString()))
                {
                    String[] scheduledDays = ((String)Settings.GetValue("ScheduleDay").ToString()).Split(',');

                    //if (DayPart.ToUpper() == Settings.GetValue("ScheduleDay").ToString() || "ALL" == Settings.GetValue("ScheduleDay").ToString())
                    if (scheduledDays.Contains(DayPart))
                    {
                        TimerAlert.Enabled = false;
                        LogFile(Application.StartupPath + "\\SchedulerLog.txt", "Tick");
                        AdiScheduler a = new AdiScheduler();
                        a.Process();
                        TimerAlert.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                TimerAlert.Enabled = true;
                LogFile(Application.StartupPath + "\\SchedulerLog.txt", ex.ToString());
            }
        }

        public static void LogFile(String FileName, String Message)
        {
            if (Message != null && Message.Length > 0)
            {
                StreamWriter writer = File.AppendText(FileName);
                try
                {
                    String DateFormat = Settings.GetValue("DateFormat").ToString();
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
