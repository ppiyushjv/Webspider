using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;
using WebSpider.Objects;

namespace WebSpider.Core
{
    public class Utility
    {
        public static String GetFileExtension(string FileName)
        {
            try
            {
                return FileName.Substring(FileName.LastIndexOf('.')+1);
            }
            catch
            {
                return "";
            }
        }

        public static String GetValidFileName(String FileName)
        {
            return GetValidDirName(FileName);
        }
        public static String GetValidDirName(String DirName)
        {
            try
            {
                //com1, com2, com3, com4, com5, com6, com7, com8, com9, lpt1, lpt2, lpt3, lpt4, lpt5, lpt6, lpt7, lpt8, lpt9, con, nul, and prn
                DirName = DirName.Replace(" ", "_");
                DirName = DirName.Replace("/", "_");
                DirName = DirName.Replace("?", "_");
                DirName = DirName.Replace("<", "_");
                DirName = DirName.Replace(">", "_");
                DirName = DirName.Replace("\\", "_");
                DirName = DirName.Replace(":", "_");
                DirName = DirName.Replace("*", "_");
                DirName = DirName.Replace("|", "_");
                DirName = DirName.Replace("\"", "_");

                return DirName;
            }
            catch
            {
                return "";
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
                    writer.WriteLine(String.Format("{0} : {1}", DateTime.Now.ToString(DateFormat), Message));
                    //TextWriter.Synchronized(writer).WriteLine(String.Format("{0} : {1}", DateTime.Now.ToString(DateFormat), Message));
                }
                finally
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }

        #region [ Application Log ]
        public static void ApplicationLog(String LogText)
        {
            String ErrorFilename = Settings.GetValue("ErrorFileName");
            Utility.LogFile(ErrorFilename, LogText);
        }

        public static void ApplicationLog(String LogText, String FileName)
        {
            String ErrorFilename = FileName;
            if (File.Exists (ErrorFilename))
                Utility.LogFile(ErrorFilename, LogText);
        }
        #endregion

        #region [ Error log ]
        public static void ErrorLog(Exception ex, String ErrorData)
        {
            try
            {
                if (Settings.GetValue("ErrorsToFile") == true)
                {
                    Thread.BeginCriticalRegion();
                    String ErrorFilename = Constants.ApplicationLogFile; // Settings.GetValue("ErrorFileName");
                    ErrorLog(ex, ErrorData, ErrorFilename);
                    Thread.EndCriticalRegion();
                }
            }
            catch { }
        }
        public static void ErrorLog(Exception ex, String ErrorData, String FileName)
        {
            try
            {
                Thread.BeginCriticalRegion();
                String ErrorFilename = FileName;
                Utility.LogFile(ErrorFilename, ErrorData);
                Utility.LogFile(ErrorFilename, ex.Message);
                Utility.LogFile(ErrorFilename, ex.StackTrace);
                Thread.EndCriticalRegion();
            }
            catch { }
        }
        #endregion

        public static void SendErrorMail(String FilePath)
        {
            if (Settings.GetValue("MailErrors") == true && File.Exists(FilePath))
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = Settings.GetValue("SmtpServer").ToString();
                    smtpClient.Port = Settings.GetValue("SmtpPort");
                    smtpClient.EnableSsl = Settings.GetValue("SmtpSSL");
                    NetworkCredential credentials = new NetworkCredential(
                        Settings.GetValue("SmtpUserName")
                        , Settings.GetValue("SmtpPassword")
                    );
                    smtpClient.Credentials = credentials;

                    MailMessage oMessage = new MailMessage();
                    oMessage.From = new MailAddress(Settings.GetValue("ErrorMailFrom"));
                    oMessage.To.Add(Settings.GetValue("ErrorMailTo"));
                    oMessage.IsBodyHtml = false;
                    oMessage.Subject = Settings.GetValue("ErrorMailSubject");
                    oMessage.Body = File.ReadAllText(FilePath);
                    smtpClient.Send(oMessage);
                }
                catch { }
            }
            
        }

        public static void SendAlertMail(string subject,String Body)
        {
          
                try
                {
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = Settings.GetValue("SmtpServer").ToString();
                    smtpClient.Port = Settings.GetValue("SmtpPort");
                    smtpClient.EnableSsl = Settings.GetValue("SmtpSSL");
                    NetworkCredential credentials = new NetworkCredential(
                        Settings.GetValue("SmtpUserName")
                        , Settings.GetValue("SmtpPassword")
                    );
                    smtpClient.Credentials = credentials;

                    MailMessage oMessage = new MailMessage();
                    oMessage.From = new MailAddress(Settings.GetValue("ErrorMailFrom"));
                    oMessage.To.Add(Settings.GetValue("ErrorMailTo"));
                    oMessage.IsBodyHtml = false;
                    oMessage.Subject = subject;
                    oMessage.Body = Body;
                    smtpClient.Send(oMessage);
                }
                catch { }
            

        }
    }
}
