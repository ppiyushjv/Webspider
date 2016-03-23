using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Data.General;
using WebSpider.Objects;
using WebSpider.Objects.General;
using WebSpider.SecLock.Data.Internal;
using WebSpider.SecLock.Data.External;
using WebSpider.SecLock.Objects.External;
using WebSpider.SecLock.Objects.Internal;
using System.IO;

namespace WebSpider.Core
{
    public class SecLockSpider
    {
        private static IDbConnection _connection;

        #region [Variables/Constants]
        public string siteUrl = "http://www.seclock.com/products/manufacturers.asp";
        public static Boolean LoggedIn { get; set; }
        private static Cache Cache = new Cache();
        private static Browser browser = new Browser();
        private static String finalcon = String.Empty;
        #endregion

        #region [Data Managers]
        private static FinalExportManager exportManager = new FinalExportManager(Constants.ConnectionString);
        #endregion

        #region [Constructor]
        static SecLockSpider()
        {
            browser.MaxRetryCount = 3;

        }
        #endregion

        #region [Login]
        private static Object LoginLock = new object();
        public static bool Login()
        {
            while (true)
            {
                lock (LoginLock)
                {
                    if (LoggedIn) return true;

                    //Utility.ApplicationLog("Log In to SecLock");
                    String Url = "https://www.seclock.com/account/login.asp?m=login"; //Settings.GetValue("ADILoginPage");
                    HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest(Url);
                    NameValueCollection formData = browser.GetFormData(document);
                    formData["pwd"] = Settings.GetValue("SecLockPassword");
                    formData["email"] = Settings.GetValue("SecLockUsername");
                    formData["iRemember"] = "1";
                    HtmlAgilityPack.HtmlDocument doc = browser.PostRequest(Url, null, formData);
                    LoggedIn = doc.DocumentNode.OuterHtml.Contains("Logout");
                    if (LoggedIn)
                    {
                        //Utility.ApplicationLog("SecLock login sucessful");
                        return true;
                    }
                    else
                    {
                        Utility.ApplicationLog("Login failed for SecLock");
                        Utility.SendAlertMail("Login Failed", "Login Failed for https://www.seclock.com");
                        return false;
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
            //return true;
        }
        #endregion

        #region [ Login Check ]
        private static Boolean IsLoggedIn()
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = browser.GetWebRequest("http://www.seclock.com/account/default.asp");
                LoggedIn = true;
                return LoggedIn = doc.DocumentNode.OuterHtml.Contains("Logout");
            }
            catch
            {
                LoggedIn = false;
                return false;
            }
        }
        #endregion

        #region [Logout]
        public static bool Logout()
        {
            //Utility.ApplicationLog("Logging out SecLock");
            String Url = "https://www.seclock.com/account/logout.asp";
            HtmlAgilityPack.HtmlDocument doc = browser.GetWebRequest(Url);
            Boolean result = doc.DocumentNode.OuterHtml.Contains("Sign In");
            if (result)
            {
                //Utility.ApplicationLog("Sucessfully logged out of SecLock");
            }
            else
            {
                //Utility.ApplicationLog("Unable to log out of SecLock");
            }
            return result;
        }
        #endregion

        #region [ Categories ]
        public List<InCategory> GetCategories()
        {
            if (new InCategoryManager(Constants.ConnectionString).Count() > 0)
                return GetAllCategoriesFromDb();
            else
                return GetAllCategoriesFromSite();
        }
        public List<InCategory> ReloadallCategories()
        {
            new InCategoryManager(Constants.ConnectionString).DeleteAll();
            return GetAllCategoriesFromSite();
        }
        private List<InCategory> GetAllCategoriesFromDb()
        {
            return new InCategoryManager(Constants.ConnectionString).GetData();
        }
        private List<InCategory> GetAllCategoriesFromSite()
        {
            if (!LoggedIn)
                Login();
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("option");
            HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest("http://www.seclock.com/products/search.asp?type=all");
            HtmlNodeCollection categoriesNodes = document.DocumentNode.SelectNodes("//select[@id='search_c']//option");
            List<InCategory> categories = new List<InCategory>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                InCategory c = new InCategory() { Code = categoryNode.Attributes["value"].Value, Name = categoryNode.InnerText };
                if (c.Name.Trim().ToUpper() == "ALL CATEGORIES")
                    continue;
                new InCategoryManager(Constants.ConnectionString).Save(c);
                categories.Add(c);

            }

            exportManager.Insert(Constants.SiteName.SECLOCK, Constants.ExportType.SECLOCK_CATEGORY, Constants.ExportType.SECLOCK_CATEGORY);
            return categories;
        }
        #endregion

        #region [ Manufacturerers ]
        public List<InManufacturer> GetManufacturers()
        {
            if (new InManufacturerManager(Constants.ConnectionString).Count() > 0)
                return GetManufacturersFromDb();
            else
                return GetManufacturersFromSite();
        }
        public List<InManufacturer> ReloadAllManufacturers()
        {
            new InManufacturerManager(Constants.ConnectionString).DeleteAll();
            return GetManufacturersFromSite();
        }
        private List<InManufacturer> GetManufacturersFromDb()
        {
            return new InManufacturerManager(Constants.ConnectionString).GetData();
        }

        private List<InManufacturer> GetManufacturersFromSite()
        {
            String Url = "http://www.seclock.com/products/manufacturers.asp";
            HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest(Url);
            var manufacturerCells = document.DocumentNode.SelectNodes("//ul[@class='manufacturer-cells']/li");

            HtmlNode y;
            List<InManufacturer> manufacturers = manufacturerCells.Select(x => new InManufacturer()
            {
                ImagePath = !ReferenceEquals(y = x.Descendants("img").FirstOrDefault(), null) ? y.Attributes["src"].Value : String.Empty,
                Url = x.Descendants("a").LastOrDefault().Attributes["href"].Value,
                Name = x.Descendants("a").FirstOrDefault().InnerHtml,
                Code = x.Descendants("a").LastOrDefault().Attributes["href"].Value.Replace("items.asp?m=", "")
            }).ToList();

            foreach (var m in manufacturers)
                new InManufacturerManager(Constants.ConnectionString).Save(m);

            exportManager.Insert(Constants.SiteName.SECLOCK, Constants.ExportType.SECLOCK_MANUFACTURER, Constants.ExportType.SECLOCK_MANUFACTURER);

            return manufacturers;
        }
        #endregion

        #region [ Manufacturer Series ]
        private void GetProductsByManufacturer(InManufacturer manufacturer)
        {
            //if (new InManufacturerSeriesManager(Constants.ConnectionString).Count(manufacturer) > 0)
            //    GetProductsByManufacturerFromDb(manufacturer);
            //else
                GetProductsByManufacturerFromSite(manufacturer);
        }

        private void GetProductsByManufacturerFromDb(InManufacturer manufacturer)
        {
            var series = new InManufacturerSeriesManager(Constants.ConnectionString).GetData(manufacturer);
            manufacturer.SeriesList.AddRange(series);
        }
        public void GetProductsByManufacturerFromSite(InManufacturer manufacturer)
        {
            String Url = "http://www.seclock.com/products/items.asp?m=" + manufacturer.Code;

            if (!LoggedIn)
                Login();
            HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest(Url);
            
            HtmlNodeCollection seriesNodes = document.DocumentNode.SelectNodes("//ul[@class='products']/li");

            manufacturer.SeriesList = new List<InManufacturerSeries>();
            if (!ReferenceEquals(seriesNodes, null))
            {
                foreach (HtmlNode seriesNode in seriesNodes)
                {
                    InManufacturerSeries series = new InManufacturerSeries();

                    var seriesDetail = seriesNode.SelectSingleNode("a");

                    if (!ReferenceEquals(seriesDetail, null))
                    {
                        series.Name = seriesNode.SelectSingleNode("a").InnerText;
                        series.Products = new List<InProduct>();

                        HtmlNodeCollection anchorNodes = seriesNode.SelectNodes("ul/li/a");

                        foreach (HtmlNode anchorNode in anchorNodes)
                        {
                            InProduct product = new InProduct();
                            product.Code = anchorNode.InnerHtml;
                            var dbProducts = new InProductManager(Constants.ConnectionString).GetData(product);
                            if (dbProducts.Count() > 0)
                            {
                                product = dbProducts[0];
                            }

                            product.Name = anchorNode.InnerText;
                            product.Url = anchorNode.Attributes["href"].Value;
                            product.ManufacturerSeries = series.Name;
                            product.ManufacturerName = manufacturer.Name;
                            product.ManufacturerCode = manufacturer.Code;

                            new InProductManager(Constants.ConnectionString).Save(product);
                            series.Products.Add(product);

                        }

                        new InManufacturerSeriesManager(Constants.ConnectionString).Save(manufacturer.Code, series);
                        manufacturer.SeriesList.Add(series);
                    }
                }
                exportManager.Insert(Constants.SiteName.SECLOCK, Constants.ExportType.SECLOCK_MANUFACTURER_SERIES, manufacturer.Code);
            }
        }
        #endregion

        #region [ Category Products ]
        public void GetCategoryProducts(InCategory category)
        {
            GetCategoryProductsFromSite(category);
        }
        private void GetCategoryProductsFromSite(InCategory category)
        {
            String Url = string.Format("http://www.seclock.com/products/ajax/advanced-search-results.asp?i=&m=&c={0}&search_in=all", category.Code);
            HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest(Url);
            HtmlNodeCollection productNodes = document.DocumentNode.SelectNodes("//a[@class='product-detail-from-search']");

            foreach (HtmlNode productNode in productNodes)
            {
                InProduct product = new InProduct();
                product.Code = productNode.InnerHtml;

                var dbProducts = new InProductManager(Constants.ConnectionString).GetData(product);
                if (dbProducts.Count() > 0)
                {
                    product = dbProducts[0];
                }
                product.Name = productNode.InnerHtml;
                product.Url = productNode.Attributes["href"].Value;
                product.CategoryName = category.Name;
                product.CategoyCode = category.Code;

                new InProductManager(Constants.ConnectionString).Save(product);

                category.Products.Add(product);
            }
        }
        #endregion

        #region [ Get Product Details From Site ]
        public InProduct GetProductFromSite(String ProductCode, Boolean Incongito = false)
        {
            if (!Incongito && !LoggedIn)
                Login();

            String Url = "http://www.seclock.com/products/ajax/details.asp?i=" + ProductCode;
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form");
            HtmlAgilityPack.HtmlDocument doc = browser.GetWebRequest(Url);
            var productsSeriesUl = doc.DocumentNode.SelectNodes("//ul[@class='products']");

            

            InProduct p = new InProduct();
            p.Code = ProductCode;
            var dbProducts = new InProductManager(Constants.ConnectionString).GetData(p);
            if (dbProducts.Count == 1)
                p = dbProducts[0];

            p.Code = doc.DocumentNode.SelectNodes("h2").First().InnerHtml;
            p.Name = doc.DocumentNode.SelectNodes("h2").First().InnerHtml;

            var img = doc.DocumentNode.SelectNodes("//img[@id='itemPic']");
            p.ImageUrl1 = img == null ? String.Empty : img[0].Attributes["src"].Value;
            
            Decimal price;

            var listPriceHtml = doc.DocumentNode.SelectNodes("//div[@class='clearfix big-tight']").FirstOrDefault();
            if (!ReferenceEquals(listPriceHtml, null))
            {
                String ListPrice = listPriceHtml.SelectNodes("//div[@class='input list']").First().InnerHtml.Trim().Replace("$", "");
                p.ListPrice = Decimal.TryParse(ListPrice, out price) ? price : 0m;
            }

            var yourPriceHtml = doc.DocumentNode.SelectNodes("//div[@class='clearfix big-tight your']").FirstOrDefault();
            if (!ReferenceEquals(yourPriceHtml, null)) 
            {
                String YourPrice = yourPriceHtml.SelectNodes("//div[@class='input']").First().InnerHtml.Trim().Replace("$", "");
                p.YourPrice = Decimal.TryParse(YourPrice, out price) ? price : 0m;
            }

            var qty = doc.DocumentNode.SelectNodes("//div[@class='input tight']/h3");
            int stock;
            if (Int32.TryParse(qty[0].InnerHtml.ToLower().Replace("available", "").Trim(), out stock))
                    p.Stock = stock.ToString();

            //var productForm = doc.DocumentNode.SelectNodes("div[@class='input tight']/h3");
            //if (!ReferenceEquals(productForm, null))
            //{
            //    var qty = productForm[0].SelectNodes("h3").FirstOrDefault();
            //    if (!ReferenceEquals(qty, null))
            //    {
            //        p.Stock = Convert.ToInt32(qty.InnerHtml.ToLower().Replace("available", "").Trim()).ToString();
            //    }
            //}
            

            HtmlNode modalBody = doc.DocumentNode.SelectNodes("//div[@id='product-modal-body']").FirstOrDefault();
            if (!ReferenceEquals(modalBody, null))
            {
                var modalBodyP = modalBody.SelectNodes("p");

                int descIndex = 1;
                if (modalBodyP[0].Descendants("img").Count() > 0)
                    descIndex = 2;

                if (!ReferenceEquals(modalBodyP[descIndex], null))
                {
                    var description = modalBodyP[descIndex].InnerHtml;
                    p.Description = description.Trim().Replace('\'', '\"');
                }

                //if (!ReferenceEquals(modalBodyTechDocs, null))
                //{
                //    var techDocs = modalBodyTechDocs.FirstOrDefault().InnerHtml;
                //    p.TechDoc = techDocs.Trim();
                //}

                #region [ Tech Docs Download ]
                var modalBodyTechDocs = modalBody.SelectNodes("//ul[@class='techdocs']");
                if (!ReferenceEquals(modalBodyTechDocs, null))
                {
                    if (modalBodyTechDocs.Count == 2)
                    {
                        

                        List<String> TechDocs = new List<string>();
                        var hrefs = modalBodyTechDocs[0].Descendants("a");
                        foreach (var href in hrefs)
                        {
                            String docUrl = href.Attributes["href"].Value;
                            try
                            {
                                if (docUrl.StartsWith("/"))
                                    docUrl = "http://www.seclock.com/" + docUrl;

                                String DocFolder = Settings.GetValue("DocFolder");
                                if (!Directory.Exists(DocFolder))
                                    Directory.CreateDirectory(DocFolder);

                                String FileName = String.Format("{0}_{1}_{2}",Settings.GetValue("SecLockImagePrefix"), p.Code, docUrl.Substring(docUrl.LastIndexOf("/") + 1));
                                String FilePath = String.Format("{0}\\{1}", DocFolder, FileName);

                                if (File.Exists (FilePath))
                                    File.Delete(FilePath);

                                browser.DownloadFile(docUrl, FilePath);
                                TechDocs.Add(FileName);
                            }
                            catch (Exception ex)
                            {
                                Utility.ErrorLog(ex, null);
                                if (Settings.GetValue("MailErrors") == true)
                                    Utility.ApplicationLog(String.Format("{0}", ex.Message), Constants.EmailErrorFile);
                            }
                            finally{
                                p.TechDoc = String.Join(";", TechDocs);
                            }

                            
                        }
                    }
                }
                #endregion

                //var manLinksNode =modalBody.SelectNodes("//ul[@class='techdocs']").FirstOrDefault();
                //if (!ReferenceEquals(manLinksNode, null))
                //{
                //    var manuFacturerLinks = manLinksNode.InnerHtml;
                //}
            }
            return p;
        }
        #endregion

        #region [ Download Product Images ]
        private String GetImageFolder(TaskDetail taskDetail)
        {
            String FolderName = String.Format("{0}\\Image", Application.StartupPath);
            switch(taskDetail.TaskMode) 
            {
                case Constants.TaskMode.SECLOCK_MANUFACTURER_CRAWL:
                case Constants.TaskMode.SECLOCK_CATEGORY_CRAWL:
                    FolderName = Settings.GetValue("CrawlImageFolder").ToString().Length > 0 ? Settings.GetValue("CrawlImageFolder") : FolderName;
                    break;
                case Constants.TaskMode.SECLOCK_PRODUCT_UPDATE:
                    FolderName = Settings.GetValue("UpdateImageFolder").ToString().Length > 0 ? Settings.GetValue("UpdateImageFolder") : FolderName;
                    break;
            }
            return FolderName;
        }

        private String CreateProductDirectory(TaskDetail taskDetail, InProduct inProduct)
        {
            String ValidDirName = Utility.GetValidDirName(inProduct.Code);
            String DirName;

            DirName = String.Format("{0}\\{1}", GetImageFolder(taskDetail), ValidDirName);
            if (!Directory.Exists(DirName))
            {
                //Utility.ApplicationLog(String.Format("Creating Directory {0}", DirName));
                Directory.CreateDirectory(DirName);
            }
            return ValidDirName;
        }

        private void DownloadProductImages(TaskDetail taskDetail, InProduct inProduct)
        {
            String Prefix = Settings.GetValue("SecLockImagePrefix").ToString();
            String ImageFileName;
            String FileName;
            String Url;

            if (!String.IsNullOrEmpty(inProduct.ImageUrl1))
            {
                ImageFileName = String.Format("{0}{1}_1.{2}", Prefix, inProduct.Code, Utility.GetFileExtension(inProduct.ImageUrl1));
                ImageFileName = Utility.GetValidFileName(ImageFileName);
                // Small Image
                FileName = String.Format("{0}\\{1}\\{2}", GetImageFolder(taskDetail), CreateProductDirectory(taskDetail, inProduct), ImageFileName);
                Url = (inProduct.ImageUrl1.StartsWith("http") ? "" : "http://www.seclock.com") + inProduct.ImageUrl1;
                //Utility.ApplicationLog(String.Format("Downloading Image from {0} to {1}", Url, FileName));
                browser.DownloadFile(Url, FileName);
                inProduct.ImageUrl1 = ImageFileName;
            }
            if (!String.IsNullOrEmpty(inProduct.ImageUrl2))
            {
                ImageFileName = String.Format("{0}{1}_2.{2}", Prefix, inProduct.Code, Utility.GetFileExtension(inProduct.ImageUrl2));
                ImageFileName = Utility.GetValidFileName(ImageFileName);
                // Small Image
                FileName = String.Format("{0}\\{1}\\{2}", GetImageFolder(taskDetail), CreateProductDirectory(taskDetail, inProduct), ImageFileName);
                Url = (inProduct.ImageUrl2.StartsWith("http") ? "" : "http://www.seclock.com") + inProduct.ImageUrl2;
                //Utility.ApplicationLog(String.Format("Downloading Image from {0} to {1}", Url, FileName));
                browser.DownloadFile(Url, FileName);
                inProduct.ImageUrl2 = ImageFileName;
            }
        }
        #endregion

        #region [ Product Updates ]
        public List<InProduct> GetUpdateProducts()
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));
            List<FinalTable> ftProducts = new FinalTableManager(ConnStr).GetData();
            return ftProducts
                .Select(x => new InProduct() { Code = x.SLD_PART, Name = x.SLD_PART })
                .ToList();
        }
        #endregion

        public List<InProduct> GetProducts()
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));
            List<FinalTable> ftProducts = new FinalTableManager(ConnStr).GetData();
            return ftProducts
                .Select(x => new InProduct() { Code = x.SLD_PART, Name = x.SLD_PART })
                .ToList();
        }

        #region [ Static Methods ]

        #region [Login Check]
        private static void LoginCheck(TaskDetail taskDetail)
        {
            if (!taskDetail.IncognitoMode && ! SecLockSpider.IsLoggedIn())
            {
                Login();
            }
        }
        #endregion

        #region [ Crawl ]
        public static void Crawl(object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            TaskDetail taskDetail = (TaskDetail)objItem;
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);

                    List<InProduct> products = new List<InProduct>();
                    if (taskDetail.TaskMode == Constants.TaskMode.SECLOCK_MANUFACTURER_CRAWL){
                        InManufacturer manufacturer = new InManufacturer();
                        manufacturer.Name = taskDetail.TaskNameText;
                        manufacturer.Code = taskDetail.TaskNameValue;
                        new SecLockSpider().GetProductsByManufacturer(manufacturer);
                        foreach (InManufacturerSeries s in manufacturer.SeriesList)
                            products.AddRange(s.Products);
                    }
                    else if (taskDetail.TaskMode == Constants.TaskMode.SECLOCK_CATEGORY_CRAWL)
                    {
                        InCategory category = new InCategory();
                        category.Name = taskDetail.TaskNameText;
                        category.Code = taskDetail.TaskNameValue;
                        new SecLockSpider().GetCategoryProducts(category);
                        products = category.Products;
                    }
                    double totalProducts = products.Count();
                    for (int index = 0; index < totalProducts; index++)
                    {
                        try
                        {
                            products[index] = new SecLockSpider().GetProductFromSite(products[index].Code, taskDetail.IncognitoMode);

                            if (taskDetail.DownloadImages)
                                new SecLockSpider().DownloadProductImages(taskDetail, products[index]);

                            new InProductManager(Constants.ConnectionString).Save(products[index]);

                            

                            FinalExport fe = new FinalExport();
                            fe.ExportSite = Constants.SiteName.SECLOCK;
                            fe.ExportType = Constants.ExportType.SECLOCK_CRAWL;
                            fe.ExportValue = products[index].Code;
                            exportManager.Insert(fe);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                            taskDetail.TaskStatus = TaskDetailStatus.Processing;
                            taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                        }
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                }
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                //Utility.ApplicationLog(String.Format("AdiGlobal product Crawling completed with errors for {0}", taskDetail.TaskNameText));
                String json = null;
                Utility.ErrorLog(ex, json);
                if (Settings.GetValue("MailErrors") == true)
                    Utility.ApplicationLog(String.Format("{0}", ex.Message), Constants.EmailErrorFile);
                taskDetail.TaskStatusText = Constants.COMPLETED_ERROR_TEXT;
                taskDetail.TaskStatus = TaskDetailStatus.CompletedWithError;
                taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
            }
        }

        
        #endregion

        #region [ Update ]
        public static void Update(object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            TaskDetail taskDetail = (TaskDetail)objItem;
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);

                    List<InProduct> products = new List<InProduct>();
                    //if (taskDetail.TaskMode == Constants.TaskMode.SECLOCK_MANUFACTURER_CRAWL)
                    //{
                    //    InManufacturer manufacturer = new InManufacturer();
                    //    manufacturer.Name = taskDetail.TaskNameText;
                    //    manufacturer.Code = taskDetail.TaskNameValue;
                    //    new SecLockSpider().GetProductsByManufacturer(manufacturer);
                    //    foreach (InManufacturerSeries s in manufacturer.SeriesList)
                    //        products.AddRange(s.Products);
                    //}
                    //else if (taskDetail.TaskMode == Constants.TaskMode.SECLOCK_CATEGORY_CRAWL)
                    //{
                    //    InCategory category = new InCategory();
                    //    category.Name = taskDetail.TaskNameText;
                    //    category.Code = taskDetail.TaskNameValue;
                    //    new SecLockSpider().GetCategoryProducts(category);
                    //    products = category.Products;
                    //}
                    products.Add(
                        new InProduct() 
                        { 
                            Code = taskDetail.TaskNameValue, 
                            Name = taskDetail.TaskNameText 
                        }
                    );
                    double totalProducts = products.Count();
                    for (int index = 0; index < totalProducts; index++)
                    {
                        try
                        {
                            products[index] = new SecLockSpider().GetProductFromSite(products[index].Code);

                            if (taskDetail.DownloadImages)
                                new SecLockSpider().DownloadProductImages(taskDetail, products[index]);

                            new InProductManager(Constants.ConnectionString).Save(products[index]);

                            FinalExport fe = new FinalExport();
                            fe.ExportSite = Constants.SiteName.SECLOCK;
                            fe.ExportType = Constants.ExportType.SECLOCK_UPDATE;
                            fe.ExportValue = products[index].Code;
                            exportManager.Insert(fe);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                            taskDetail.TaskStatus = TaskDetailStatus.Processing;
                            taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                        }
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                }
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                //Utility.ApplicationLog(String.Format("AdiGlobal product Crawling completed with errors for {0}", taskDetail.TaskNameText));
                String json = null;
                Utility.ErrorLog(ex, json);
                if (Settings.GetValue("MailErrors") == true)
                    Utility.ApplicationLog(String.Format("{0}", ex.Message), Constants.EmailErrorFile);
                taskDetail.TaskStatusText = Constants.COMPLETED_ERROR_TEXT;
                taskDetail.TaskStatus = TaskDetailStatus.CompletedWithError;
                taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
            }
        }
        #endregion

        #region [ Export ]
        public static Boolean PendingExports()
        {
            return (!ReferenceEquals(exportManager.GetTopBySite(Constants.SiteName.SECLOCK), null));
        }

        public static void Export(ref ToolStripStatusLabel lbl)
        {
            //List<FinalExport> finalExportList = exportManager.GetBySite(Constants.SiteName.ADIGLOBAL);
            List<FinalExport> finalExportList = new List<FinalExport>();
            finalExportList.Add(exportManager.GetTopBySite(Constants.SiteName.SECLOCK));
            for (int index = 0; index < finalExportList.Count; index++)
            {
                FinalExport finalExport = finalExportList[index];
                if (finalExport == null)
                {
                    lbl.Text = String.Empty;
                    return;
                }
                if (!ReferenceEquals(lbl, null))
                {
                    lbl.Text = String.Format("Saving {0} {1} {2}", "SecLock", finalExport.ExportType, finalExport.ExportValue);
                }
                try
                {
                    if (finalExport.ExportType == Constants.ExportType.SECLOCK_MANUFACTURER)
                    {
                        // BRAND EXPORT
                        //Utility.ApplicationLog("Exporting Brands");
                        ExportManufacturers();
                        //Utility.ApplicationLog("Brands exported sucessfully");
                    }
                    else if (finalExport.ExportType == Constants.ExportType.SECLOCK_MANUFACTURER_SERIES)
                    {
                        ExportManufacturerSeries(finalExport.ExportValue);
                    }
                    else if (finalExport.ExportType == Constants.ExportType.SECLOCK_CATEGORY)
                    {
                        // CATEGORY EXPORT
                        //Utility.ApplicationLog("Exporting Categories");
                        ExportCategories();
                        //Utility.ApplicationLog("Categories exported sucessfully");
                    }
                    else if (finalExport.ExportType == Constants.ExportType.SECLOCK_CRAWL)
                    {
                        // PRODUCT CRAWLING
                        //Utility.ApplicationLog("Exproting product");
                        ExportProduct(finalExport.ExportValue);
                        //Utility.ApplicationLog("Product exported sucessfully");
                    }
                    else if (finalExport.ExportType == Constants.ExportType.SECLOCK_UPDATE)
                    {
                        // PRODUCT UPDATE
                        //Utility.ApplicationLog("Exporting product Update");
                        ExportProductUpdate(finalExport.ExportValue);
                        //Utility.ApplicationLog("Product update exported sucessfully");
                    }

                }
                catch (Exception ex)
                {
                    Utility.ApplicationLog(String.Format("Saving failed for {0} {1} {2}", finalExport.ExportSite, finalExport.ExportType, finalExport.ExportValue));
                    String json = null;
                    Utility.ErrorLog(ex, json);
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Saving failed for {0} {1} {2}", finalExport.ExportSite, finalExport.ExportType, finalExport.ExportValue), Constants.EmailErrorFile);
                }
                finally
                {
                    exportManager.Delete(finalExport.ExportSite, finalExport.ExportType, finalExport.ExportValue, finalExport.CreatedDate);
                }
            }
        }

        private static void ExportCategories()
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));

            List<InCategory> categories = new InCategoryManager(Constants.ConnectionString).GetData();
            foreach (InCategory category in categories)
                new CategoryManager(ConnStr).Save(category);
        }

        private static void ExportManufacturers()
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));

            List<InManufacturer> manufacturers = new InManufacturerManager(Constants.ConnectionString).GetData();
            foreach (InManufacturer manufacturer in manufacturers)
                new ManufacturerManager(ConnStr).Save(manufacturer);


            //OleDbConnection OleConn = new OleDbConnection(ConnStr);
            //OleDbDataAdapter OleAdp = new OleDbDataAdapter(SQL, OleConn);
            //DataTable dt = new DataTable();
            //OleAdp.Fill(dt);
            //foreach (DataRow dRow in categories.Rows)
            //{
            //    dt.Rows.Add(dt.NewRow().ItemArray = dRow.ItemArray);
            //}
            //OleAdp.InsertCommand = new OleDbCommand(INSERT);
            //OleAdp.InsertCommand.Parameters.Add("@Value", OleDbType.VarChar, 255, "Value");
            //OleAdp.InsertCommand.Parameters.Add("@DisplayName", OleDbType.VarChar, 255, "DisplayName");
            //OleAdp.InsertCommand.Parameters.Add("@ParentValue", OleDbType.VarChar, 255, "ParentValue");
            //OleAdp.InsertCommand.Parameters.Add("@CategoryUrl", OleDbType.VarChar, 255, "CategoryUrl");
            //OleAdp.InsertCommand.Parameters.Add("@ClearanceZone", OleDbType.Boolean, 8, "ClearanceZone");
            //OleAdp.InsertCommand.Parameters.Add("@SaleCenter", OleDbType.Boolean, 8, "SaleCenter");
            //OleAdp.InsertCommand.Parameters.Add("@OnlineSpecials", OleDbType.Boolean, 8, "OnlineSpecials");
            //OleAdp.InsertCommand.Parameters.Add("@HotDeals", OleDbType.Boolean, 8, "HotDeals");
            //OleAdp.InsertCommand.Connection = OleConn;
            //OleAdp.InsertCommand.Connection.Open();
            //OleAdp.Update(dt);
            //OleAdp.InsertCommand.Connection.Close();
        }

        private static void ExportManufacturerSeries(String ManufacturerCode)
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));

            InManufacturer manufacturer = new InManufacturer();
            manufacturer.Code = ManufacturerCode;
            List<InManufacturerSeries> series = new InManufacturerSeriesManager(Constants.ConnectionString).GetData(manufacturer);

            foreach (InManufacturerSeries s in series)
                new ManufacturerSeriesManager(ConnStr).Save(ManufacturerCode, s);
        }

        private static void ExportProduct(string productCode)
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));

            InProduct product = new InProduct();
            product.Code = productCode;
            var secLockProduct = new InProductManager(Constants.ConnectionString).GetData(product);
            if (secLockProduct.Count() == 0)
                return;

            product = secLockProduct[0];
            FinalTable ft = new FinalTable();
            ft.SLD_SOURCE_ID = String.Empty;
            ft.SLD_COST = product.YourPrice;
            ft.SLD_PART = product.Code;
            ft.SLD_IMG1 = product.ImageUrl1;
            ft.SLD_IMG2 = product.ImageUrl2;
            ft.SLD_VENDOR = product.ManufacturerName;
            ft.SLD_INV = product.Stock;
            ft.SLD_DESC = product.Description;
            ft.SLD_TECHDOC = product.TechDoc;
            ft.SLD_LastUpdate = DateTime.Now.ToString(Settings.GetValue("DateFormat"));

            new FinalTableManager(ConnStr).SaveProduct(ft);
        }

        public static void ExportProductUpdate(String productCode)
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));

            InProduct product = new InProduct();
            product.Code = productCode;
            var secLockProduct = new InProductManager(Constants.ConnectionString).GetData(product);
            if (secLockProduct.Count() == 0)
                return;

            product = secLockProduct[0];
            FinalTable ft = new FinalTable();
            ft.SLD_SOURCE_ID = String.Empty;
            ft.SLD_COST = product.YourPrice;
            ft.SLD_PART = product.Code;
            ft.SLD_IMG1 = product.ImageUrl1;
            ft.SLD_IMG2 = product.ImageUrl2;
            ft.SLD_VENDOR = product.ManufacturerName;
            ft.SLD_INV = product.Stock;
            ft.SLD_LastUpdate = DateTime.Now.ToString(Settings.GetValue("DateFormat"));

            new FinalTableManager(ConnStr).SaveProduct(ft);
        }

        #endregion

        #endregion


    }
}
