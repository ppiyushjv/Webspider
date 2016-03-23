using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Specialized;

namespace WebSpider.Core
{
    class Crawler
    {
        public string Url { get; set; }

        public Crawler() { }

        public Crawler(string Url)
        {
            this.Url = Url;
        }


        public XDocument GetXDocument()
        {
            HtmlAgilityPack.HtmlWeb doc1 = new HtmlAgilityPack.HtmlWeb();
            doc1.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            HtmlAgilityPack.HtmlDocument doc2 = doc1.Load(Url);
            doc2.OptionOutputAsXml = true;
            doc2.OptionAutoCloseOnEnd = true;
            doc2.OptionDefaultStreamEncoding = System.Text.Encoding.UTF8;
            XDocument xdoc = XDocument.Parse(doc2.DocumentNode.SelectSingleNode("html").OuterHtml);
            return xdoc;
        }

        public HtmlDocument GetDocument()
        {
            HtmlAgilityPack.HtmlWeb doc1 = new HtmlAgilityPack.HtmlWeb();
            doc1.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            HtmlAgilityPack.HtmlDocument doc2 = doc1.Load(Url);
            doc2.OptionOutputAsXml = true;
            doc2.OptionAutoCloseOnEnd = true;
            doc2.OptionDefaultStreamEncoding = System.Text.Encoding.UTF8;
            return doc2;
        }


        public void Save(String FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            WebClient client = new WebClient();
            if (Url.StartsWith("http"))
                client.DownloadFile(Url, FileName);
            else
                client.DownloadFile("http:" + Url, FileName);
        }


        class ProductSpecs
        {
            String [][]GeneralInformation;
            String [][]TechnicalInformation;
        }

        

        public String GetInventory(Browser browser)
        {
            //WebClient webclient = new WebClient();
            //Uri uristring = null;
            ////Please replace your webservice url here                                                  
            //uristring = new Uri("https://adiglobal.us/_vti_bin/requests.asmx/InventoryRingPuddle ");
            //httpClient.Headers["ContentType"] = "application/json";
            //httpClient.Headers["Content-Type"] = "application/json";
            ////webclient.Headers["Cookie"] = "ASP.NET_SessionId=4pxkjsbzk5agicfkkfabic55";
            //string JsonStringParams = "{'productId':'BS-BEPHOC'}";
            //webclient.UploadStringCompleted += wc_UploadStringCompleted;
            ////Post data like this                                                                          
            //webclient.UploadStringAsync(uristring, "POST", JsonStringParams);

            String Url = "https://adiglobal.us/_vti_bin/requests.asmx/InventoryRingPuddle";
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("productId", "BS-BEPHOC");
            String responseJson = browser.AjaxPost(Url, parameters);
            return responseJson;
        }

        //public void GetProductSpecification(String ProductID, String ProductName)
        //{
        //    WebClient webclient = new WebClient();
        //    Uri uristring = null;
        //    //Please replace your webservice url here                                                 
        //    uristring = new Uri("http://adiglobal.us/_vti_bin/requests.asmx/SubmitQuery");
        //    webclient.Headers["ContentType"] = "application/json";
        //    webclient.Headers["Content-Type"] = "application/json";
        //    string JsonStringParams = "{'request':{'SearchCriterias':[],'CategoryName':'6400','SortBy':'b','PageNumber':5,'ResultsPerPage':64,'ReturnRefinersOnly':false,'PageCode':6,'ExcludedRefiners':'','SearchTerm':''},'Adsrequest':{'Rcat':'6400','FirstParentRcat':'6000','VendorId':'','Mode':'c','PromoOption':null,'SearchTerm':null}}";
        //    webclient.UploadStringCompleted += wc_UploadStringCompleted;
        //    //Post data like this                                                                         
        //    webclient.UploadStringAsync(uristring, "POST", JsonStringParams);
        //}

        private void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {

                if (e.Result != null)
                {
                    string responce = e.Result.ToString();
                    //To Do Your functionality
                }
            }
            catch
            {
            }
        }
    }
}
