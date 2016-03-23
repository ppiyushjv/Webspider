using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace WebSpider.Core
{
    public class Browser : CacheDb
    {
        #region [Data Members]
        private WebHeaderCollection WebHeader { get; set; }
        private static CacheDb cache;
        private Boolean EnableCaching;
        #endregion

        #region [Properties]
        public int MaxRetryCount { get; set; }
        public int MaxRequestsPerMinute { get; set; }
        #endregion

        #region [Constructor]
        public Browser()
        {
            cache = new CacheDb();
            //httpClient = new HttpClient();
            EnableCaching = Settings.GetValue("EnableCaching");
        }
        #endregion

        public HtmlDocument GetWebRequest(String Url)
        {
            for (int RetryCount = 1; true; RetryCount++)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();

                    HtmlDocument document = new HtmlDocument();
                    document.OptionAutoCloseOnEnd = true;
                    document.OptionCheckSyntax = true;
                    document.OptionFixNestedTags = true;
                    //document.OptionWriteEmptyNodes = true;
                    byte[] responseBytes;
                    if (EnableCaching && !cache.IsCachedUrl(Url))
                    {
                        responseBytes = httpClient.DownloadData(Url);
                        CacheDb.SaveCache(Url, responseBytes);
                    }
                    else if (EnableCaching)
                        responseBytes = cache.GetCachedUrl(Url);
                    else
                        responseBytes = httpClient.DownloadData(Url);
                    MemoryStream mStream = new MemoryStream(responseBytes);
                    document.Load(mStream);
                    return document;
                }
                catch (Exception ex)
                {
                    if (RetryCount == MaxRetryCount)
                        throw ex;
                }
            }
        }

        public HtmlDocument PostRequest(String Url, WebHeaderCollection Header, NameValueCollection formData)
        {
            for (int RetryCount = 1 ; true ; RetryCount++)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();

                    Crawler crawler = new Crawler();
                    byte[] responseBytes;
                    crawler.Url = Url;
                    //WebClient httpClient = new WebClient();
                    if (!ReferenceEquals(Header, null))
                    {
                        httpClient.Headers = Header;
                    }
                    if (!ReferenceEquals(formData, null))
                    {
                        httpClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        responseBytes = httpClient.UploadValues(Url, "POST", formData);
                    }
                    else
                    {
                        responseBytes = httpClient.DownloadData(Url);
                    }
                    string resultAuthTicket = Encoding.UTF8.GetString(responseBytes);
                    httpClient.Dispose();
                    MemoryStream mStream = new MemoryStream(responseBytes);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.Load(mStream);
                    return document;
                }
                catch (Exception ex)
                {
                    if (RetryCount == MaxRetryCount)
                        throw ex;
                }
            }
        }

        public String AjaxPost(String Url, NameValueCollection parameters)
        {
            //HttpClient httpClient = new HttpClient();

            //Uri uristring = new Uri(Url);
            //httpClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
            //httpClient.Headers["ContentType"] = "application/json";
            List<String> Parameters = new List<String>();
            foreach (String key in parameters.AllKeys)
            {
                Parameters.Add(String.Format("\'{0}\':\'{1}\'", key, parameters[key]));
            }

            string JsonStringParams = "{" + String.Join(",", Parameters) + "}";
            //return httpClient.UploadString(Url, JsonStringParams);
            return AjaxPost(Url, JsonStringParams);
        }

        public String AjaxPost(String Url, String JsonStringParams)
        {
            for (int RetryCount = 1 ; true ; RetryCount++)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    //httpClient.Proxy = new WebProxy("23.254.4.34",80);
                    //httpClient.Credentials = new NetworkCredential("john20", "doe20");

                    Uri uristring = new Uri(Url);
                    httpClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
                    httpClient.Headers["ContentType"] = "application/json";
                    return httpClient.UploadString(Url, JsonStringParams);
                }
                catch (Exception ex)
                {
                    if (RetryCount == MaxRetryCount)
                        throw ex;
                }
            }
        }

        public HtmlDocument ParseDocument(String htmlString)
        {
            try
            {
                byte[] htmlByte = Encoding.UTF8.GetBytes(htmlString);
                MemoryStream mStream = new MemoryStream(htmlByte);
                HtmlDocument document = new HtmlDocument();
                document.Load(mStream);
                return document;
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, object> parseJson(String JsonText)
        {
            Dictionary<string, object> temp = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonText);
            return temp;
        }

        public void DownloadFile(String Url, String FileName)
        {
            for (int RetryCount = 1 ; true ; RetryCount++)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DownloadFile(Url, FileName);
                    return;
                }
                catch (Exception ex)
                {
                    if (RetryCount == MaxRetryCount)
                        throw ex;
                }
            }
        }

        public NameValueCollection GetFormData(HtmlDocument document)
        {
            NameValueCollection formData = new NameValueCollection();
            var inputItems = document.DocumentNode.SelectNodes("//input");//.Descendants()
                //.Where(x => x.Attributes.Contains("name") && x.Attributes.Contains("value"))
                //.Select(x => new
                //{
                //    Name = x.Attributes["name"].Value.ToString(),
                //    Value = x.Attributes["value"].Value.ToString()
                //}
                //);

            foreach (var items in inputItems)
            {
                if (!ReferenceEquals(items.Attributes["name"], null))
                    formData.Add(items.Attributes["name"].Value, items.Attributes["value"] == null ? String.Empty : items.Attributes["value"].Value);
            }
            //foreach (var items in inputItems)
            //{
            //    formData.Add(items.Name, items.Value);
            //}
            return formData;
        }
    }
}
