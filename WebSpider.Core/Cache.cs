using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.Xml.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace WebSpider.Core
{
   public class Cache : CacheDb
    {
        #region [Add Cache]
        /// <summary>
        /// Add URL to cache
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        //private static HtmlAgilityPack.HtmlDocument AddUrl(String Url)
        //{
        //    if (!IsCachedUrl(Url))
        //    {
        //        Crawler crawler = new Crawler();
        //        crawler.Url = Url;
        //        HtmlAgilityPack.HtmlDocument document = crawler.GetDocument();
        //        String FileName = GenerateFileName();
        //        document.Save(GetFullFileName(FileName));
        //        DataRow dRow = CacheDB.Tables[_dtName].NewRow();
        //        dRow["Url"] = Url;
        //        dRow["FileName"] = FileName;
        //        dRow["LastUpdated"] = DateTime.Now;
        //        dRow["ValidTill"] = DateTime.Now.AddMinutes(_cacheValidity);
        //        CacheDB.Tables[_dtName].Rows.Add(dRow);
        //        SaveCache();

        //        return document;
        //    }
        //    return new HtmlAgilityPack.HtmlDocument();
        //}

        public byte[] AddUrl(String Url, NameValueCollection formData = null )
        {
            Crawler crawler = new Crawler();
            byte[] responseBytes;
            crawler.Url = Url;
            WebClient webClient = new WebClient();
            if (!ReferenceEquals(formData, null))
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                responseBytes = webClient.UploadValues(Url, "POST", formData);
            }
            else
            {
                responseBytes = webClient.DownloadData(Url);
            }
            string resultAuthTicket = Encoding.UTF8.GetString(responseBytes);
            webClient.Dispose();
            //String FileName = GenerateFileName();
            //File.WriteAllBytes(FileName, responseBytes);
            //DataRow dRow = CacheDB.Tables[_dtName].NewRow();
            //dRow["Url"] = Url;
            //dRow["FileName"] = FileName;
            //dRow["LastUpdated"] = DateTime.Now;
            //dRow["ValidTill"] = DateTime.Now.AddMinutes(_cacheValidity);
            //CacheDB.Tables[_dtName].Rows.Add(dRow);
            //SaveCache();
            SaveCache(Url, responseBytes);

            return responseBytes;
        }
        #endregion

        #region [Get Url]
        /// <summary>
        /// Get URL Data From cache is exists, otherwise from web saveing into cache
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public HtmlAgilityPack.HtmlDocument GetUrl(String Url)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            byte[] responseBytes;
            if (!IsCachedUrl(Url))
                responseBytes = AddUrl(Url);
            else
                responseBytes = GetCachedUrl(Url);
            MemoryStream mStream = new MemoryStream(responseBytes);
            document.Load(mStream);
            return document;
        }

        public byte[] GetUrl(String Url, NameValueCollection formData)
        {
            if (!IsCachedUrl(Url))
                return AddUrl(Url, formData);
            else
                return GetCachedUrl(Url);
        }

        public static HtmlAgilityPack.HtmlDocument PostUrl(String Url, WebHeaderCollection Header, NameValueCollection formData)
        {
            Crawler crawler = new Crawler();
            byte[] responseBytes;
            crawler.Url = Url;
            WebClient webClient = new WebClient();
            if (!ReferenceEquals(Header, null))
            {
                webClient.Headers = Header;
            }
            if (!ReferenceEquals(formData, null))
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                responseBytes = webClient.UploadValues(Url, "POST", formData);
            }
            else
            {
                responseBytes = webClient.DownloadData(Url);
            }
            string resultAuthTicket = Encoding.UTF8.GetString(responseBytes);
            webClient.Dispose();
            MemoryStream mStream = new MemoryStream(responseBytes);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load(mStream);
            return document;
        }
        #endregion

        public NameValueCollection GetFormData(String Url)
        {
            NameValueCollection formData = new NameValueCollection();
            HtmlAgilityPack.HtmlDocument document = GetUrl(Url);
            var inputItems = document.DocumentNode.SelectNodes("//body").Descendants()
                .Where(x => x.Attributes.Contains("name") && x.Attributes.Contains("value"))
                .Select(x => new { Name = x.Attributes["name"].Value.ToString(), 
                    Value = x.Attributes["value"].Value.ToString() }
                );


            foreach (var items in inputItems)
            {
                formData.Add(items.Name, items.Value);
            }
            return formData;

        }
    }
}
