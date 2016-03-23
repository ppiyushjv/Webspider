using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using WebSpider.Core;

namespace WebSpiderScheduler
{
    public partial class Service1 : ServiceBase
    {
        private const string fileName = "Log.txt";
        System.Windows.Forms.Timer timer;

        public Service1()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = Settings.GetValue("MailSchedulerInterval");
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LogFile(fileName, "Timer Tick Start");
            AdiScheduler.Process();
        }

        protected override void OnStart(string[] args)
        {
            LogFile(fileName, "Service Start");
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
           LogFile(fileName, "Service End");
        }

        public static void LogFile(String FileName, String Message)
        {
            if (Message != null && Message.Length > 0)
            {
                StreamWriter writer = File.AppendText(FileName);
                try
                {
                    String DateFormat ="MM/dd/yyyy hh:mm";
                    writer.WriteLine(String.Format("{0} : {1}", DateTime.Now.ToString(DateFormat), Message));
                }
                finally
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }

        private void TimerAlert_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogFile(fileName, "Timer Tick Start");
            AdiScheduler.Process();
        }
    }
}
