using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Linq;
using WebSpider;
using WebSpider.Data;
using WebSpider.AdiGlobal.Data.AdiGlobal;
using WebSpider.Data.General;
using WebSpider.Data.UpdateSpider1TableAdapters;
using WebSpider.Data.DatabaseConnection;
using WebSpider.AdiGlobal.Data.AdiExport;
using WebSpider.Objects;
using WebSpider.AdiGlobal.Objects.AdiGlobal;
using WebSpider.Objects.General;
using WebSpider.AdiGlobal.Objects.AdiExport;
using System.Text.RegularExpressions;

namespace WebSpider.Core
{
    public class TriSpider
    {
        private static IDbConnection _connection;

        #region [Variables/Constants]
        public static string siteUrl = "http://www.tri-ed.com";
        public static Boolean LoggedIn { get; set; }
        private static Cache Cache = new Cache();
        private static Browser browser = new Browser();
        private static String finalcon = String.Empty;
        #endregion

        #region [Data Managers]
        private static ADICategoryManager adiCategoryManager = new ADICategoryManager(Constants.ConnectionString);
        private static ADIBrandManager adiBrandManager = new ADIBrandManager(Constants.ConnectionString);
        private static ADIProductManager adiProductManager = new ADIProductManager(Constants.ConnectionString);
        private static ADIProductSpecificationManager adiProductSpecificationManager = new ADIProductSpecificationManager(Constants.ConnectionString);
        private static ADICategoryExportManager adiCategoryExportManager = new ADICategoryExportManager(Constants.ConnectionString);
        private static ADIInventoryDetailsManager adiInventoryDetailsManager = new ADIInventoryDetailsManager(Constants.ConnectionString);
        private static ADIInventoryExportManager adiInventoryExportManager = new ADIInventoryExportManager(Constants.ConnectionString);
        private static TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
        private static FinalExportManager exportManager = new FinalExportManager(Constants.ConnectionString);
        #endregion

        #region [Constructor]
        static TriSpider()
        {
            browser.MaxRetryCount = 3;
            TasksInit();
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

                    //Utility.ApplicationLog("Log In to AdiGlobal");
                    String Url = Settings.GetValue("TriedLoginPage");
                    HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest(Url);
                    NameValueCollection formData = browser.GetFormData(document);
                    //formData["__EVENTARGUMENT"] = "";
                    //formData["__EVENTTARGET"] = "ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$MainLoginView$MainLogin$LoginButton";
                    formData["txtEmail"] = Settings.GetValue("ADIPassword");
                    formData["txtPassword"] = Settings.GetValue("ADIUsername");
                    HtmlAgilityPack.HtmlDocument doc = browser.PostRequest(Url, null, formData);
                    LoggedIn = doc.DocumentNode.OuterHtml.Contains("LogOff");
                    if (LoggedIn)
                    {
                        return true;
                    }
                    else
                    {
                        Utility.ApplicationLog("Login failed for AdiGlobal");
                        Utility.SendAlertMail("Login Failed", "Login Failed for https://adiglobal.us");
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
                HtmlAgilityPack.HtmlDocument doc = browser.GetWebRequest("http://ecomm.tri-ed.com/default.aspx");
                LoggedIn = true;
                return LoggedIn = doc.DocumentNode.OuterHtml.Contains("LogOff");
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
            //Utility.ApplicationLog("Logging out AdiGlobal");
            String Url = "http://ecomm.tri-ed.com/default.aspx?Page=Logon&Action=LogOff";
            HtmlAgilityPack.HtmlDocument doc = browser.GetWebRequest(Url);
            Boolean result = doc.DocumentNode.OuterHtml.Contains("Logon");
            if (result)
            {
                //Utility.ApplicationLog("Sucessfully logged out of AdiGlobal");
            }
            else
            {
                //Utility.ApplicationLog("Unable to log out of AdiGlobal");
            }
            return result;
        }
        #endregion

        #region [ Ajax ]
        private static AdiResponse GetAjaxSubmitQuery(AdiRequest adiRequest)
        {
            String Url = "https://adiglobal.us/_vti_bin/requests.asmx/SubmitQuery";
            String Criteria = JsonConvert.SerializeObject(adiRequest);
            String response = browser.AjaxPost(Url, Criteria);
            response = browser.parseJson(response)["d"].ToString();
            AdiResponse adiResponse = JsonConvert.DeserializeObject<AdiResponse>(response);
            return adiResponse;
        }
        #endregion

        #region [CATEGORY]

        #region [ General ]
        public static List<AdiCategory> GetAllCategories(String SearchCategory, String ExcludedRefiners, List<AdiSearchCriteria> adiSearchCriteriaList)
        {
            List<AdiCategory> categories = new List<AdiCategory>();
            String criteria = String.Empty;
            AdiRequest adiRequest = new AdiRequest();
            adiRequest.request.CategoryName = SearchCategory;
            adiRequest.request.ExcludedRefiners = ExcludedRefiners;
            adiRequest.request.ResultsPerPage = 2;
            adiRequest.request.SearchCriterias = adiSearchCriteriaList;

            AdiResponse adiResponse = GetAjaxSubmitQuery(adiRequest);


            var refiners = adiResponse
                .Response
                .Refiners
                .Where(x => x.DisplayName == "Category")
                .FirstOrDefault();

            if (!ReferenceEquals(refiners, null))
            {
                categories = refiners
                    .Options
                    .Select(y => new AdiCategory()
                    {
                        DisplayName = y.DisplayName,
                        Value = y.Value,
                    })
                    .ToList();
            }

            String[] SearchCriteriaProperty = adiSearchCriteriaList.Select(x => x.Property).ToArray();
            String[] SearchCriteriaValue = adiSearchCriteriaList.Select(x => x.Value).ToArray();
            if (categories.Count > 0)
            {
                for (int index = 0; index < categories.Count; index++)
                {
                    categories[index].ParentValue = SearchCategory;
                    categories[index].CategoryUrl = String.Format("https://adiglobal.us/Pages/Results.aspx?c={0}&m=c", categories[index].Value);
                    adiCategoryManager.SaveCategory(categories[index]);

                    if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Clearance Zone"))
                    {
                        adiCategoryManager.SetClearanceZoneByCategoryValue(categories[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Hot Deals"))
                    {
                        adiCategoryManager.SetHotDealsByCategoryValue(categories[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Online Specials"))
                    {
                        adiCategoryManager.SetOnlineSpecialsByCategoryValue(categories[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Sale Center"))
                    {
                        adiCategoryManager.SetSaleCenterByCategoryValue(categories[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixstockavailability") && SearchCriteriaValue.Contains("In Stock"))
                    {
                        adiCategoryManager.SetStockAvailability(categories[index].Value);
                    }

                    List<AdiCategory> subCategories = GetAllCategories(categories[index].Value, ExcludedRefiners, adiSearchCriteriaList);
                    categories[index].SubCategory = subCategories;
                }
            }
            return categories;
        }

        #region [Rearrange Category as Tree]
        private static List<AdiCategory> CategoryTree(List<AdiCategory> allCategories, String ParentCategoryID)
        {
            List<AdiCategory> categories = new List<AdiCategory>();
            categories = allCategories.Where(x => x.ParentValue == ParentCategoryID).ToList();
            if (categories.Count > 0)
            {
                foreach (AdiCategory category in categories)
                {
                    category.SubCategory = CategoryTree(allCategories, category.Value);
                }
            }
            return categories;
        }
        #endregion
        #endregion

        #region [Clear Categories]
        public static void ClearAllCategories()
        {
            //Utility.ApplicationLog("Flashing all adiglobal categories");
            adiCategoryManager.ClearCatagory();
            adiCategoryExportManager.ClearCatagory();
            //Utility.ApplicationLog("Flashing AdiGlobal categories completre");
        }
        #endregion

        #region [ All categories ]
        public static List<AdiCategory> LoadCatagory(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal categories");
            List<AdiCategory> oCategories = new List<AdiCategory>();
            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiCategoryManager.CategoryCount() == 0)
                        LoadFromDB = false;

                    int UpdateInterVal = Settings.GetValue("AdiCategoryUpdateInterval");
                    DateTime LastUpdateDateTime = Settings.GetValue("AdiCategoryLastUpdateDateTime");
                    if (LastUpdateDateTime.AddMinutes(UpdateInterVal) <= DateTime.Now)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }

                if (LoadFromDB)
                    oCategories = LoadCategoryFromDb();
                else
                {
                    oCategories = LoadCatagoryFromSite();
                }
                //Utility.ApplicationLog("AdiGlobal categories loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal categories");
                String json = "Unable to Load Catagories";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal categories. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }

            return oCategories;
        }
        private static List<AdiCategory> LoadCategoryFromDb()
        {
            List<AdiCategory> categories = adiCategoryManager.GetData();
            return CategoryTree(categories, "0000");
        }
        private static List<AdiCategory> LoadCatagoryFromSite()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            List<AdiCategory> categories =  GetAllCategories("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_CATEGORY, Constants.ExportType.ADI_CATEGORY);
            return categories;
        }
        #endregion

        #region [ Clearance Zone ]
        public static List<AdiCategory> LoadClearanceZoneCategories(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Clearance Zone categories");
            List<AdiCategory> oCategories = new List<AdiCategory>();

            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiCategoryManager.ClearanceZoneCategoryCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }

                if (LoadFromDB)
                    oCategories = LoadClearanceZoneCategoriesFromDB();
                else
                {
                    oCategories = LoadClearanceZoneCategoriesFromSite();
                }
                //Utility.ApplicationLog("AdiGlobal Clearance Zone categories loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Clearance Zone categories");
                String json = "Unable to Load Catagories";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Clearance Zone categories. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            return oCategories;
        }
        private static List<AdiCategory> LoadClearanceZoneCategoriesFromDB()
        {
            List<AdiCategory> categories = adiCategoryManager.GetClearanceZoneData();//GetData(true, false, false, false, false);
            return CategoryTree(categories, "0000");
        }
        private static List<AdiCategory> LoadClearanceZoneCategoriesFromSite()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Clearance Zone"
            });
            List<AdiCategory> categories = GetAllCategories("0000", "", adiSearchCriteriaList); ;
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_CATEGORY, Constants.ExportType.ADI_CATEGORY);
            return categories;
        }
        #endregion

        #region [ Hot Deals ]
        public static List<AdiCategory> LoadHotDealsCategories(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Hot Deals categories");
            List<AdiCategory> oCategories = new List<AdiCategory>();

            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiCategoryManager.HotDealsCategoryCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }

                if (LoadFromDB)
                    oCategories = LoadHotDealsCategoriesFromDB();
                else
                {
                    oCategories = LoadHotDealsCategoriesFromSite();
                }
                //Utility.ApplicationLog("AdiGlobal Hot Deals categories loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Hot Deals categories");
                String json = "Unable to Load Hot Deal Catagories";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Hot Deals categories. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            return oCategories;
        }
        private static List<AdiCategory> LoadHotDealsCategoriesFromDB()
        {
            List<AdiCategory> categories = adiCategoryManager.GetHotDealsData();//GetData(false, false, false, true, false);
            return CategoryTree(categories, "0000");
        }
        private static List<AdiCategory> LoadHotDealsCategoriesFromSite()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Hot Deals"
            });
            List<AdiCategory> categories = GetAllCategories("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_CATEGORY, Constants.ExportType.ADI_CATEGORY);
            return categories;
        }
        #endregion

        #region [ Online Specials ]
        public static List<AdiCategory> LoadOnlineSpecialsCategories(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Online Specials categories");
            List<AdiCategory> oCategories = new List<AdiCategory>();

            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiCategoryManager.OnlineSpecialsCategoryCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }

                if (LoadFromDB)
                    oCategories = LoadOnlineSpecialsCategoriesFromDB();
                else
                {
                    oCategories = LoadOnlineSpecialsCategoriesFromSite();
                }
                //Utility.ApplicationLog("AdiGlobal Online Specials categories loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Online Specials categories");
                String json = "Unable to Load Online Specials Catagories";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Online Specials categories. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            return oCategories;
        }
        private static List<AdiCategory> LoadOnlineSpecialsCategoriesFromDB()
        {
            List<AdiCategory> categories = adiCategoryManager.GetOnlineSpecialsData();//GetData(false, false, true, false, false);
            return CategoryTree(categories, "0000");
        }
        private static List<AdiCategory> LoadOnlineSpecialsCategoriesFromSite()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Online Specials"
            });
            List<AdiCategory> categories = GetAllCategories("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_CATEGORY, Constants.ExportType.ADI_CATEGORY);
            return categories;
        }
        #endregion

        #region [ Sale Center ]
        public static List<AdiCategory> LoadSaleCenterCategories(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Sale Center categories");
            List<AdiCategory> oCategories = new List<AdiCategory>();

            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiCategoryManager.SaleCenterCategoryCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }

                if (LoadFromDB)
                    oCategories = LoadSaleCenterCategoriesFromDB();
                else
                {
                    oCategories = LoadSaleCenterCategoriesFromSite();
                }
                //Utility.ApplicationLog("AdiGlobal Sale Center categories loaded");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Sale Center categories");
                String json = "Unable to Load Sale Center Catagories";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Sale Center categories. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            return oCategories;
        }
        private static List<AdiCategory> LoadSaleCenterCategoriesFromDB()
        {
            List<AdiCategory> categories = adiCategoryManager.GetSaleCenterData();//GetData(false, true, false, false, false);
            return CategoryTree(categories, "0000");
        }
        private static List<AdiCategory> LoadSaleCenterCategoriesFromSite()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Sale Center"
            });
            List<AdiCategory> categories = GetAllCategories("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_CATEGORY, Constants.ExportType.ADI_CATEGORY);
            return categories;
        }
        #endregion

        #region [ InStock Items ]
        public static List<AdiCategory> LoadInStockCategories(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal In Stock categories");
            List<AdiCategory> oCategories = new List<AdiCategory>();

            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiCategoryManager.InStockCategoryCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }

                if (LoadFromDB)
                    oCategories = LoadInStockCategoriesFromDB();
                else
                {
                    oCategories = LoadInStockCategoriesFromSite();
                }
                //Utility.ApplicationLog("AdiGlobal Instrock categoried loaded successfully");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal In Stock categories");
                String json = "Unable to Load In Stock Catagories";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal In Stock categories. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            return oCategories;
        }
        private static List<AdiCategory> LoadInStockCategoriesFromDB()
        {
            List<AdiCategory> categories = adiCategoryManager.GetInStockData();//GetData(false, false, false, false, true);
            return CategoryTree(categories, "0000");
        }
        private static List<AdiCategory> LoadInStockCategoriesFromSite()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixstockavailability",
                Value = "In Stock"
            });
            List<AdiCategory> categories = GetAllCategories("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_CATEGORY, Constants.ExportType.ADI_CATEGORY);
            return categories;
        }
        #endregion

        #endregion

        #region [BRANDS]

        #region [Clear Brands]
        public static void ClearAllBrands()
        {
            //Utility.ApplicationLog("Flushing AdiGlobal Brands");
            adiBrandManager.ClearBrands();
            //Utility.ApplicationLog("Flushing AdiGlobal Brands Complete");
        }
        #endregion

        #region [ General ]
        public static List<AdiBrand> GetAllBrands(String SearchCategory, String ExcludedRefiners, List<AdiSearchCriteria> adiSearchCriteriaList)
        {
            List<AdiBrand> brandsList = new List<AdiBrand>();
            String criteria = String.Empty;
            AdiRequest adiRequest = new AdiRequest();
            adiRequest.request.CategoryName = SearchCategory;
            adiRequest.request.ExcludedRefiners = ExcludedRefiners;
            adiRequest.request.ResultsPerPage = 2;
            adiRequest.request.SearchCriterias = adiSearchCriteriaList;

            AdiResponse adiResponse = GetAjaxSubmitQuery(adiRequest);

            var refiners = adiResponse
                .Response
                .Refiners
                .Where(x => x.DisplayName == "Brand")
                .FirstOrDefault();
            if (!ReferenceEquals(refiners, null))
            {
                brandsList = refiners
                    .Options
                    .Select(y => new AdiBrand()
                    {
                        DisplayName = y.DisplayName,
                        Value = y.Value,
                    })
                    .ToList();
            }
            String[] SearchCriteriaProperty = adiSearchCriteriaList.Select(x => x.Property).ToArray();
            String[] SearchCriteriaValue = adiSearchCriteriaList.Select(x => x.Value).ToArray();
            if (brandsList.Count > 0)
            {
                for (int index = 0; index < brandsList.Count; index++)
                {
                    adiBrandManager.SaveBrand(brandsList[index]);
                    if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Clearance Zone"))
                    {
                        adiBrandManager.SetClearanceZone(brandsList[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Hot Deals"))
                    {
                        adiBrandManager.SetHotDeals(brandsList[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Online Specials"))
                    {
                        adiBrandManager.SetOnlineSpecials(brandsList[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixissaleitem") && SearchCriteriaValue.Contains("Sale Center"))
                    {
                        adiBrandManager.SetSaleCenter(brandsList[index].Value);
                    }
                    else if (SearchCriteriaProperty.Contains("adixstockavailability") && SearchCriteriaValue.Contains("In Stock"))
                    {
                        adiBrandManager.SetStockAvailability(brandsList[index].Value);
                    }
                }
            }
            return brandsList.OrderBy(x => x.DisplayName).ToList();
        }
        #endregion

        #region [ All Brands ]
        public static List<AdiBrand> GetAllBrands(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal brands");
            List<AdiBrand> allBrands = new List<AdiBrand>();
            Boolean LoadFromDB = true;

            try
            {
                try
                {
                    if (adiBrandManager.BrandCount() == 0)
                        LoadFromDB = false;

                    int UpdateInterVal = Settings.GetValue("AdiCategoryUpdateInterval");
                    DateTime LastUpdateDateTime = Settings.GetValue("AdiCategoryLastUpdateDateTime");
                    if (LastUpdateDateTime.AddMinutes(UpdateInterVal) <= DateTime.Now)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }
                if (LoadFromDB)
                    allBrands = GetAllBrandsFromDB();
                else
                    allBrands = GetAllBrandsFromWeb();
                //Utility.ApplicationLog("AdiGlobal brands loaded");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal brands");
                String json = "Unable to Load Brands";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal brands. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }
            return allBrands;
        }
        private static List<AdiBrand> GetAllBrandsFromDB()
        {
            List<AdiBrand> allBrands = new List<AdiBrand>();
            allBrands = adiBrandManager.GetData();
            return allBrands;
        }
        private static List<AdiBrand> GetAllBrandsFromWeb()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            List<AdiBrand> brands = GetAllBrands("0000", "", adiSearchCriteriaList); 
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_BRAND, Constants.ExportType.ADI_BRAND);
            return brands;
        }
        #endregion

        #region [ Clearance Zone Brands ]
        public static List<AdiBrand> GetClearanceZoneBrands(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Clearance Zone brands");
            List<AdiBrand> allBrands = new List<AdiBrand>();
            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiBrandManager.ClearanceZoneBrandCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }
                if (LoadFromDB)
                    allBrands = GetClearanceZoneBrandsFromDB();
                else
                    allBrands = GetClearanceZoneBrandsFromWeb();
                //Utility.ApplicationLog("AdiGlobal Clearance Zone brands loaded");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Clearance Zone brands");
                String json = "Unable to Load Clearance Zone Brands";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Clearance Zone brands. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }
            return allBrands;
        }
        private static List<AdiBrand> GetClearanceZoneBrandsFromDB()
        {
            return adiBrandManager.GetData(true, false, false, false, false);
        }
        private static List<AdiBrand> GetClearanceZoneBrandsFromWeb()
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Clearance Zone"
            });
            List<AdiBrand> brands = GetAllBrands("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_BRAND, Constants.ExportType.ADI_BRAND);
            return brands;
        }
        #endregion

        #region [ Hot Deals Brand ]
        public static List<AdiBrand> GetHotDealsBrands(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Hot Deals brands");
            List<AdiBrand> allBrands = new List<AdiBrand>();
            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiBrandManager.HotDealsBrandCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }
                if (LoadFromDB)
                    allBrands = GetHotDealsBrandsFromDB();
                else
                    allBrands = GetHotDealsBrandsFromWeb();
                //Utility.ApplicationLog("AdiGlobal Hot Deals brands loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Hot Deals brands");
                String json = "Unable to Load Clearance Zone Brands";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Hot Deals brands. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }
            return allBrands;
        }
        private static List<AdiBrand> GetHotDealsBrandsFromDB()
        {
            return adiBrandManager.GetData(false, false, false, true, false);
        }
        private static List<AdiBrand> GetHotDealsBrandsFromWeb(Boolean EmailErrors = false)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Hot Deals"
            });
            List<AdiBrand> brands = GetAllBrands("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_BRAND, Constants.ExportType.ADI_BRAND);
            return brands;
        }
        #endregion

        #region [ Online Specials Brands ]
        public static List<AdiBrand> GetOnlineSpecialsBrands(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Online Specials brands");
            List<AdiBrand> allBrands = new List<AdiBrand>();
            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiBrandManager.OnlineSpecialsBrandCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }
                if (LoadFromDB)
                    allBrands = GetOnlineSpecialsBrandsFromDB();
                else
                    allBrands = GetOnlineSpecialsBrandsFromWeb();
                //Utility.ApplicationLog("AdiGlobal Online Specials brands loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Online Specials brands");
                String json = "Unable to Load Clearance Zone Brands";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Online Specials brands. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }
            return allBrands;
        }
        private static List<AdiBrand> GetOnlineSpecialsBrandsFromDB()
        {
            return adiBrandManager.GetData(false, false, true, false, false);
        }
        private static List<AdiBrand> GetOnlineSpecialsBrandsFromWeb(Boolean EmailErrors = false)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Online Specials"
            });
            List<AdiBrand> brands = GetAllBrands("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_BRAND, Constants.ExportType.ADI_BRAND);
            return brands;
        }
        #endregion

        #region [ Sale Center Brands ]
        public static List<AdiBrand> GetSaleCenterBrands(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal Sale Center brands");
            List<AdiBrand> allBrands = new List<AdiBrand>();
            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiBrandManager.SaleCenterBrandCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }
                if (LoadFromDB)
                    allBrands = GetSaleCenterBrandsFromDB();
                else
                    allBrands = GetSaleCenterBrandsFromWeb();
                //Utility.ApplicationLog("AdiGlobal Sale Center brands loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal Sale Center brands");
                String json = "Unable to Load Clearance Zone Brands";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal Sale Center brands. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }
            return allBrands;
        }
        private static List<AdiBrand> GetSaleCenterBrandsFromDB()
        {
            return adiBrandManager.GetData(false, true, false, false, false);
        }
        private static List<AdiBrand> GetSaleCenterBrandsFromWeb(Boolean EmailErrors = false)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Sale Center"
            });
            List<AdiBrand> brands = GetAllBrands("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_BRAND, Constants.ExportType.ADI_BRAND);
            return brands;
        }
        #endregion

        #region [ In Stock Items ]
        public static List<AdiBrand> GetInStockBrands(Boolean EmailErrors = false)
        {
            //Utility.ApplicationLog("Loading AdiGlobal In Stock brands");
            List<AdiBrand> allBrands = new List<AdiBrand>();
            Boolean LoadFromDB = true;
            try
            {
                try
                {
                    if (adiBrandManager.InStockBrandCount() == 0)
                        LoadFromDB = false;
                }
                catch
                {
                    LoadFromDB = false;
                }
                if (LoadFromDB)
                    allBrands = GetInStockBrandsFromDB();
                else
                    allBrands = GetInStockBrandsFromWeb();
                //Utility.ApplicationLog("AdiGlobal In Stock brands loading complete");
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog("Failed to load AdiGlobal In Stock brands");
                String json = "Unable to Load In Stock Brands";
                Utility.ErrorLog(ex, json);
                if (EmailErrors)
                {
                    if (Settings.GetValue("MailErrors") == true)
                        Utility.ApplicationLog(String.Format("Failed to load AdiGlobal In Stock brands. {0}", ex.Message), Constants.EmailErrorFile);
                }
            }
            finally
            {
                if (!LoadFromDB)
                {
                    Settings.SetValue("AdiCategoryLastUpdateDateTime", typeof(DateTime), DateTime.Now);
                    Settings.Save();
                }
            }
            return allBrands;
        }
        private static List<AdiBrand> GetInStockBrandsFromDB()
        {
            return adiBrandManager.GetData(false, false, false, false, true);
        }
        private static List<AdiBrand> GetInStockBrandsFromWeb(Boolean EmailErrors = false)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixstockavailability",
                Value = "In Stock"
            });
            List<AdiBrand> brands = GetAllBrands("0000", "", adiSearchCriteriaList);
            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_BRAND, Constants.ExportType.ADI_BRAND);
            return brands;
        }
        #endregion

        #endregion

        #region [ Get Products List ]

        #region [Get Products List]
        private static List<AdiProduct> GetProducts(String SearchCategory, String ExcludedRefiners, List<AdiSearchCriteria> adiSearchCriteriaList)
        {
            List<AdiProduct> Products = new List<AdiProduct>();
            AdiRequest adiRequest = new AdiRequest();
            adiRequest.request.CategoryName = SearchCategory;
            adiRequest.request.ExcludedRefiners = ExcludedRefiners;

            adiRequest.request.PageNumber = 1;
            adiRequest.request.SearchCriterias = adiSearchCriteriaList;

            int CurrentPage = 0, TotalPages = 1;
            do
            {
                CurrentPage++;
                String criteria = String.Empty;

                adiRequest.request.PageNumber = CurrentPage;
                AdiResponse adiResponse = GetAjaxSubmitQuery(adiRequest);

                CurrentPage = adiResponse.Response.Products.CurrentPage;
                TotalPages = adiResponse.Response.Products.TotalPages;
                Products.AddRange(adiResponse.Response.Products.Products);

                foreach (AdiProduct p in Products)
                {
                    p.Url = String.Format("/Pages/Product.aspx?pid={0}", p.AdiNumber.Replace("+", " "));
                    p.CatagoryID = SearchCategory == String.Empty ? String.Empty : SearchCategory;
                }
            } while (CurrentPage < TotalPages);
            return Products;
        }
        #endregion

        #region [ All Products ]
        public static List<AdiProduct> ParseCatagoryProducts(string CategoryID)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            return GetProducts(CategoryID, "", adiSearchCriteriaList);
        }

        public static List<AdiProduct> GetBrandProducts(String BrandValue)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixbrandid",
                Value = BrandValue
            });
            return GetProducts("0000", "adixbrandid", adiSearchCriteriaList);
        }
        #endregion

        #region [ Save now ]
        #region [ Clearance Zone ]
        public static List<AdiProduct> GetClearanceZoneCategoryProducts(String CategoryID)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Clearance Zone"
            });
            return GetProducts(CategoryID, "", adiSearchCriteriaList);
        }
        public static List<AdiProduct> GetClearanceZoneBrandProducts(String BrandValue)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Clearance Zone"
            });
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixbrandid",
                Value = BrandValue
            });
            return GetProducts("0000", "adixbrandid", adiSearchCriteriaList);
        }
        #endregion

        #region [Hot Deals]
        public static List<AdiProduct> GetHotDealsCategoryProducts(String CategoryID)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Hot Deals"
            });
            return GetProducts(CategoryID, "", adiSearchCriteriaList);
        }
        public static List<AdiProduct> GetHotDealsBrandProducts(String BrandValue)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Hot Deals"
            });
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixbrandid",
                Value = BrandValue
            });
            return GetProducts("0000", "adixbrandid", adiSearchCriteriaList);
        }
        #endregion

        #region [Online Specials]
        public static List<AdiProduct> GetOnlineSpecialCategoryProducts(String CategoryID)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Online Specials"
            });
            return GetProducts(CategoryID, "", adiSearchCriteriaList);
        }
        public static List<AdiProduct> GetOnlineSpecialBrandProducts(String BrandValue)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Online Specials"
            });
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixbrandid",
                Value = BrandValue
            });
            return GetProducts("0000", "adixbrandid", adiSearchCriteriaList);
        }
        #endregion

        #region [Sale Center]
        public static List<AdiProduct> GetSaleCenterCategoryProducts(String CategoryID)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Sale Center"
            });
            return GetProducts(CategoryID, "", adiSearchCriteriaList);
        }
        public static List<AdiProduct> GetSaleCenterBrandProducts(String BrandValue)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixissaleitem",
                Value = "Sale Center"
            });
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixbrandid",
                Value = BrandValue
            });
            return GetProducts("0000", "adixbrandid", adiSearchCriteriaList);
        }
        #endregion

        #region [In Stock]
        public static List<AdiProduct> GetInStockCategoryProducts(String CategoryID)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixstockavailability",
                Value = "In Stock"
            });
            return GetProducts(CategoryID, "", adiSearchCriteriaList);
        }
        public static List<AdiProduct> GetInStockBrandProducts(String BrandValue)
        {
            List<AdiSearchCriteria> adiSearchCriteriaList = new List<AdiSearchCriteria>();
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixstockavailability",
                Value = "In Stock"
            });
            adiSearchCriteriaList.Add(new AdiSearchCriteria()
            {
                Property = "adixbrandid",
                Value = BrandValue
            });
            return GetProducts("0000", "adixbrandid", adiSearchCriteriaList);
        }
        #endregion
        #endregion

        #endregion

        #region [PRODUCTS]
        #region [Parse Product]

        public static void GetProductDetails(AdiProduct product, Boolean SaveImage = false, AdiMode mode=AdiMode.CRAWL)
        {
            String Url = GetAbsoluteUrl(product.Url);
            var productDoc = browser.GetWebRequest(Url);

            try
            {
                var overview = productDoc.DocumentNode.SelectSingleNode("//div[@class='ProductDetailtab-container']")
                    .SelectSingleNode("//div[@class='tab-content overview clearfix']").Descendants("div");

                foreach (HtmlNode node in overview)
                {
                    var h3 = node.Descendants("h3").FirstOrDefault();
                    if (h3 != null)
                    {
                        String Name = h3.InnerHtml;
                        String Value = node.InnerHtml.Replace(h3.OuterHtml, "").Trim();
                        Value = Regex.Replace(Value, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;", String.Empty);
                        adiProductSpecificationManager.Save(product.AdiNumber, Name, Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog(String.Format("Error downloading product overview"));
                String json = null;
                Utility.ErrorLog(ex, json);
                if (Settings.GetValue("MailErrors") == true)
                    Utility.ApplicationLog(String.Format("Error downloading product overview. {0}", ex.Message), Constants.EmailErrorFile);
            }

            try
            {
                if (SaveImage)
                {
                    var productImages = productDoc.DocumentNode.SelectSingleNode("//div[@class='product-img-big']").Descendants("img");

                    product.BigImage = productImages.FirstOrDefault().Attributes["src"].Value;
                    if (!product.BigImage.StartsWith("http"))
                        product.BigImage = GetAbsoluteUrl(product.BigImage);

                    if (productImages.Count() > 1)
                    {
                        product.SmallImage = productImages.ToList()[1].Attributes["src"].Value;
                        if (!product.SmallImage.StartsWith("http"))
                            product.SmallImage = GetAbsoluteUrl(product.SmallImage);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog(String.Format("Error downloading product image"));
                String json = null;
                Utility.ErrorLog(ex, json);
                if (Settings.GetValue("MailErrors") == true)
                    Utility.ApplicationLog(String.Format("Error downloading product image. {0}", ex.Message), Constants.EmailErrorFile);
            }
            GetProductSpecification(ref product,mode);
            UpdateProductAid(product, SaveImage, mode);
        }

        public static AdiProduct GetProduct(AdiProduct product)
        {
            product.Url = String.Format("/Pages/Product.aspx?pid={0}", product.AdiNumber);

            //Browser browser = new Browser();
            String Url = GetAbsoluteUrl(product.Url);
            HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest(Url);

            var nodes = document.DocumentNode.Descendants().Where(x => x.Attributes["class"] != null);

            String productName = nodes.Where(x => x.GetAttributeValue("class", "").Contains("productTitle")).FirstOrDefault().InnerHtml;
            String vendorName = nodes.Where(x => x.GetAttributeValue("class", "").Contains("vendorName")).FirstOrDefault().InnerHtml;
            String vendorModel = nodes.Where(x => x.GetAttributeValue("class", "").Contains("vendorNbr")).FirstOrDefault().InnerHtml;
            String partNumber = nodes.Where(x => x.GetAttributeValue("class", "").Contains("partNbr")).FirstOrDefault().InnerHtml;
            String markrtingMessage = document.DocumentNode.SelectSingleNode("//div[@class='MTmessage']").InnerHtml;

            String productPrice = nodes.Where(x => x.GetAttributeValue("class", "").Contains("col-value price")).FirstOrDefault().InnerHtml;

            product.Name = HtmlRemoval.StripTagsRegexCompiled(productName).Replace("\n", "").Replace("\r", "").Trim();
            product.VendorName = HtmlRemoval.StripTagsRegexCompiled(vendorName).Replace("\n", "").Replace("\r", "").Trim();
            product.VendorModel = HtmlRemoval.StripTagsRegexCompiled(vendorModel).Replace("\n", "").Replace("\r", "").Replace("Model #:", "").Trim();
            product.PartNumber = HtmlRemoval.StripTagsRegexCompiled(partNumber).Replace("\n", "").Replace("\r", "").Replace("ADI #:", "").Trim();
            product.MarketingMessage = HtmlRemoval.StripTagsRegexCompiled(markrtingMessage).Replace("\n", "").Replace("\r", "").Trim();
            product.Price = Convert.ToDecimal(HtmlRemoval.StripTagsRegexCompiled(productPrice).Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("$", "").Trim());

            return product;
        }

        #region [Product Specifications]
        //public static void GetProductSpecification(String ProductID, String ProductName)
        public static void GetProductSpecification(ref AdiProduct product,AdiMode mode)
        {
            //ADIChildTableAdapter adapter = new ADIChildTableAdapter();
            NameValueCollection JsonPatameters = new NameValueCollection();
            JsonPatameters.Add("productID", product.AdiNumber);
            JsonPatameters.Add("productDefinitionName", product.ProductDescription);
            String Url = "https://adiglobal.us/_vti_bin/requests.asmx/ProductSpecifications";
            String responseJson = browser.AjaxPost(Url, JsonPatameters);

            Dictionary<string, object> d = browser.parseJson(browser.parseJson(responseJson)["d"].ToString());

            foreach (String rootKey in d.Keys)
            {
                Dictionary<string, object> dInfo = browser.parseJson(d[rootKey].ToString());
                foreach (String key in dInfo.Keys)
                {
                    adiProductSpecificationManager.Save(product.AdiNumber, key, dInfo[key].ToString());
                    //if (adiProductSpecificationManager.GetValue(product.AdiNumber, key) == null)
                    //    adiProductSpecificationManager.Insert(product.AdiNumber, key, dInfo[key].ToString());
                    //else
                    //    adiProductSpecificationManager.UpdateValue(dInfo[key].ToString(), product.AdiNumber, key);
                }
            }
        }
        #endregion
        #endregion

        #region [Save Products]
        private static object SavingProduct = new object();
        public static bool SaveProduct(AdiProduct product,AdiMode mode)
        {
            //Utility.ApplicationLog(String.Format("Saving Adi product {0} {1}", product.AdiNumber, product.ProductDescription));
            bool Saved = false;
            Saved = false;
            var products = adiProductManager.GetDataByPartNum(product.AdiNumber);
            if (products.Count == 1)
            {
                var prod = products[0];
                product.CatagoryID = (product.CatagoryID != null && product.CatagoryID.Length > 0) ? product.CatagoryID : prod.CatagoryID == null ? null : prod.CatagoryID;
                //product.BrandValue = (product.BrandValue != null && product.BrandValue.Length > 0) ? product.BrandValue : prod.BrandValue == null ? null : prod.BrandValue;
                long adiID = prod.ID;
                product.LastUpdateDatetime = DateTime.Now;
                Saved = (adiProductManager.UpdateByID(products[0].ID, product.AdiNumber, product.VendorName, product.VendorNumber, product.VendorModel
                    , product.PartNumber, product.Name, product.Url, product.AllowedToBuy, product.DangerousGoodsMessage
                    , product.InventoryMessage, product.MarketingMessage, product.MinQty, product.ModelNumber, product.Price
                    , product.ProductDescription, product.ProductImagePath, product.RecycleFee, product.SaleMessageIndicator, product.SaleType
                    , product.ST, product.SMI, product.InventoryMessageCode, product.CatagoryID, product.SmallImage, product.BigImage
                    , product.IsUpdating, product.UpdateInterval, product.LastUpdateDatetime) == 1);
            }
            else
            {
                //product.ProductImagePath = Utility.GetValidDirName(product.VendorName);
                product.LastUpdateDatetime = DateTime.Now;
                product.UpdateInterval = Settings.GetValue("ADIProductDefaultUpdateInterval");

                Saved = (adiProductManager.InsertNew(product.ID, product.AdiNumber, product.VendorName, product.VendorNumber, product.VendorModel
                    , product.PartNumber, product.Name, product.Url, product.AllowedToBuy, product.DangerousGoodsMessage
                    , product.InventoryMessage, product.MarketingMessage, product.MinQty, product.ModelNumber, product.Price
                    , product.ProductDescription, product.ProductImagePath, product.RecycleFee, product.SaleMessageIndicator, product.SaleType
                    , product.ST, product.SMI, product.InventoryMessageCode, product.CatagoryID, product.SmallImage, product.BigImage
                    , product.IsUpdating, product.UpdateInterval, product.LastUpdateDatetime) == 1);
            }
            return Saved;
        }
        #endregion

        #region [Update Product Details]
        private static void UpdateProductAid(AdiProduct product, Boolean SaveProductImage, AdiMode mode)
        {
            var products = adiProductManager.GetDataByPartNum(product.AdiNumber);
            if (products.Count == 1)
            {
                var prod = products[0];
                String CatagoryID = (product.CatagoryID != null && product.CatagoryID.Length > 0) ? product.CatagoryID : prod.CatagoryID == null ? null : prod.CatagoryID;
                //String BrandValue = (product.BrandValue != null && product.BrandValue.Length > 0) ? product.BrandValue : prod.BrandValue == null ? null : prod.BrandValue;
                long adiID = prod.ID;
                //adiProductManager.UpdateByID(product.CatagoryID, product.BrandValue, product.Price, product.AdiNumber, product.VendorName, DateTime.Now, product.MarketingMessage, adiID);

                prod.ProductImagePath = Utility.GetValidDirName(product.VendorName);
                //prod.CatagoryID = CatagoryID;
                prod.LastUpdateDatetime = DateTime.Now;

                adiProductManager.UpdateByID(prod.ID, prod.AdiNumber, prod.VendorName, prod.VendorNumber, prod.VendorModel
                    , prod.PartNumber, prod.Name, prod.Url, prod.AllowedToBuy, prod.DangerousGoodsMessage
                    , prod.InventoryMessage, prod.MarketingMessage, prod.MinQty, prod.ModelNumber, prod.Price
                    , prod.ProductDescription, prod.ProductImagePath, prod.RecycleFee, prod.SaleMessageIndicator, prod.SaleType
                    , prod.ST, prod.SMI, prod.InventoryMessageCode, prod.CatagoryID, prod.SmallImage, prod.BigImage
                    , prod.IsUpdating, prod.UpdateInterval, prod.LastUpdateDatetime);

                if (SaveProductImage) { }
                    adiProductManager.UpdateImageByID(adiID, SaveProductSmallImage(product, mode), SaveProductBigImage(product, mode));
            }
        }
        #endregion

        #region [Save Product Image]
        private static String GetImageFolder(AdiMode mode)
        {
            String FolderName = String.Format("{0}\\Image", Application.StartupPath);
            if (mode == AdiMode.CRAWL)
                FolderName = Settings.GetValue("CrawlImageFolder").ToString().Length > 0 ? Settings.GetValue("CrawlImageFolder") : FolderName;
            else
                FolderName = Settings.GetValue("UpdateImageFolder").ToString().Length > 0 ? Settings.GetValue("UpdateImageFolder") : FolderName;
            return FolderName;
        }

        private static String CreateProductDirectory(AdiProduct product, AdiMode mode)
        {
            String ValidDirName = Utility.GetValidDirName(product.VendorName);
            String DirName ;

            DirName = String.Format("{0}\\{1}", GetImageFolder(mode), ValidDirName);
            if (!Directory.Exists(DirName))
            {
                //Utility.ApplicationLog(String.Format("Creating Directory {0}", DirName));
                Directory.CreateDirectory(DirName);
            }
            return ValidDirName;
        }

        private static String SaveProductSmallImage(AdiProduct p, AdiMode mode)
        {
            String Prefix = Settings.GetValue("AdiImagePrefix").ToString();

            if (p.ProductImagePath == null || p.ProductImagePath.Length == 0)
                return null;

            String ImageFileName = String.Format("{0}{1}_small.{2}", Prefix, p.AdiNumber, Utility.GetFileExtension(p.ProductImagePath));
            // Small Image
            String FileName = String.Format("{0}\\{1}\\{2}", GetImageFolder(mode), CreateProductDirectory(p, mode), ImageFileName);
            String Url = (p.ProductImagePath.StartsWith("https") ? "" : "https:") + p.ProductImagePath;
            //Utility.ApplicationLog(String.Format("Downloading Image from {0} to {1}", Url, FileName));
            browser.DownloadFile(Url, FileName);

            return ImageFileName;
        }

        private static String SaveProductBigImage(AdiProduct p, AdiMode mode)
        {
            String Prefix = Settings.GetValue("AdiImagePrefix").ToString();

            if (p.BigImage == null || p.BigImage.Length == 0)
                return null;

            String ImageFileName = String.Format("{0}{1}.{2}", Prefix, p.AdiNumber, Utility.GetFileExtension(p.BigImage));
            // Big Image

            String FileName = String.Format("{0}\\{1}\\{2}", GetImageFolder(mode), CreateProductDirectory(p,mode), ImageFileName);
            String Url = (p.BigImage.StartsWith("https") ? "" : "https:") + p.BigImage;
            //Utility.ApplicationLog(String.Format("Downloading Image from {0} to {1}", Url, FileName));
            browser.DownloadFile(Url, FileName);

            return ImageFileName;
        }
        #endregion

        #region [Save product Inventory]
        public static double GetProductInventory(AdiProduct product,AdiMode mode, bool saveInventory = true)
        {
            //Utility.ApplicationLog(String.Format("Getting Inventory for {0} {1}", product.AdiNumber, product.ProductDescription));
            String Url = "https://adiglobal.us/_vti_bin/requests.asmx/InventoryByProduct";
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("productId", product.AdiNumber);
            String responseJson = browser.AjaxPost(Url, parameters);
            //String responseJson = File.ReadAllText("JsonResult.txt");

            Dictionary<string, object> response = browser.parseJson(browser.parseJson(browser.parseJson(responseJson)["d"].ToString())["Response"].ToString());
            List<ADIInventoryDetails> storesList  = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ADIInventoryDetails>>(response["Stores"].ToString());
            storesList.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ADIInventoryDetails>>(response["Regional"].ToString()));
            storesList.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ADIInventoryDetails>>(response["National"].ToString()));
            storesList.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ADIInventoryDetails>>(response["Hubs"].ToString()));
            storesList.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ADIInventoryDetails>>(response["Ring"].ToString()));
            return SaveAdiInventory(product, storesList,mode,saveInventory);
        }

        private static double SaveAdiInventory(AdiProduct product, List<ADIInventoryDetails> storesList,AdiMode mode, bool saveInventory = true)
        {
            adiInventoryDetailsManager.DeleteByPart(product.AdiNumber);
            foreach (var store in storesList)
            {
                adiInventoryDetailsManager.Insert(product.AdiNumber, store.id, store.dc, store.region, store.storeName, store.address1
                    , store.address2, store.address3, store.country, store.city, store.state, store.stateName, store.zip
                    , store.phone, store.fax, store.lat, store.lon, store.inventory, store.manager, store.responseCode
                    , store.responseMessage, store.IsHub);
            }

            double TotalInventory = 0, Dallas = 0, DC_AtlantaHub = 0, DC_Dallas_Hub = 0,
                    DC_Elk_Grove_Hub = 0, DC_Feura_Bush = 0, DC_Louisville_Hub = 0,
                    DC_Reno_Hub = 0, DC_Richmond_Dist_Ctr = 0, Oklahama = 0, RemainingBranches = 0;

            List<ADIInventoryDetails> hubsList = storesList.Where(x => x.IsHub).ToList();

            Dallas = GetInventory(storesList, "dallas");
            DC_AtlantaHub = GetInventory(hubsList, "distribution center: atlanta hub");
            DC_Dallas_Hub = GetInventory(hubsList, "distribution center: dallas hub");
            DC_Elk_Grove_Hub = GetInventory(hubsList, "distribution center: elk grove hub");
            DC_Feura_Bush = GetInventory(hubsList, "distribution center: feura bush");
            DC_Louisville_Hub = GetInventory(hubsList, "distribution center: louisville hub");
            DC_Reno_Hub = GetInventory(hubsList, "distribution center: reno hub");
            DC_Richmond_Dist_Ctr = GetInventory(hubsList, "distribution center: richmond dist ctr");
            Oklahama = GetInventory(storesList, "oklahama");
            TotalInventory = storesList.Sum(x => Convert.ToDouble("0" + x.inventory));
            try
            {
                RemainingBranches = TotalInventory - (Dallas + DC_AtlantaHub + DC_Dallas_Hub + DC_Elk_Grove_Hub + DC_Feura_Bush + DC_Louisville_Hub + DC_Reno_Hub + DC_Richmond_Dist_Ctr + Oklahama);
            }
            catch { }
            
            //DataTable oDataTable = adiInventoryManager.GetDataByProduct(product.AdiNumber);
            ADIInventoryExport adiInventory = adiInventoryExportManager.GetDataByProduct(product.AdiNumber);
            if (saveInventory)
            {
                if (!ReferenceEquals(adiInventory, null))
                {
                    // update
                    adiInventoryExportManager.UpdateByPartNum(
                        product.AdiNumber,
                        (int)TotalInventory,
                        (int)Dallas,
                        (int)DC_AtlantaHub,
                        (int)DC_Dallas_Hub,
                        (int)DC_Elk_Grove_Hub,
                        (int)DC_Feura_Bush,
                        (int)DC_Louisville_Hub,
                        (int)DC_Reno_Hub,
                        (int)DC_Richmond_Dist_Ctr,
                        (int)Oklahama,
                        (int)RemainingBranches,
                        DateTime.Now);
                }
                else
                {
                    // insert
                    adiInventoryExportManager.Insert(
                        product.AdiNumber,
                        (int)TotalInventory,
                        (int)Dallas,
                        (int)DC_AtlantaHub,
                        (int)DC_Dallas_Hub,
                        (int)DC_Elk_Grove_Hub,
                        (int)DC_Feura_Bush,
                        (int)DC_Louisville_Hub,
                        (int)DC_Reno_Hub,
                        (int)DC_Richmond_Dist_Ctr,
                        (int)Oklahama,
                        (int)RemainingBranches,
                        DateTime.Now);
                }
            }
            return TotalInventory;
        }

        public static void SaveFinalTableWithInventory(AdiProduct product, double totalInventory)
        {
            FinalTableManager fadapter = new FinalTableManager(finalcon);
            int count = fadapter.GetDataByAdiPart(product.AdiNumber).Count;
            if (count == 1)
            {
                fadapter.UpdateInvBYPartNO(totalInventory.ToString(), DateTime.Now.ToString(Settings.GetValue("DateFormat")), product.AdiNumber);
            }
            
        }

        private static double GetInventory(List<ADIInventoryDetails> storesList, String storeName)
        {
            double inventoryCount = 0;
            try
            {
                inventoryCount = Convert.ToDouble(storesList.Where(x => x.storeName == storeName).Select(x => x.inventory).First());
            }
            catch { }
            return inventoryCount;
        }
        #endregion

        #region [Product Is updating]
        public static bool ProductIsUpdating(AdiProduct product)
        {
            try
            {
                if (adiProductManager.IsUpdating(product.AdiNumber) == true)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void SetProductUpdating(AdiProduct product, Boolean isUpdating)
        {
            adiProductManager.SetUpdating(product.AdiNumber, isUpdating);
        }
        #endregion

        #region [Products pending update]
        public static List<AdiProduct> GetAllPendingProducts()
        {
            List<AdiProduct> products = new List<AdiProduct>();
            try
            {
                products = adiProductManager.GetDataByNextUpdateDateTime();
            }
            catch { }
            return products;
        }
        #endregion

        #region [Get Priority Product]
        public static List<AdiProduct> GetAllPriorityProducts()
        {
            List<AdiProduct> products = new List<AdiProduct>();
            try
            {
                //FinalTableManager fadapter = new FinalTableManager(finalcon);
                ADIProductManager fadapter = new ADIProductManager(Constants.ConnectionString);
                //fadapter.Connection = finalcon;

                //var oDt = fadapter.GetPriorityData(true);
                //products = oDt.Select(x => new AdiProduct()
                //        {
                //            AdiNumber = x.AID_PART,
                //            LastUpdateDatetime = DateTime.ParseExact(x.AID_LastUpdate, Settings.GetValue("DateFormat"), System.Globalization.CultureInfo.InvariantCulture),
                //            LeastCount = x.ADI_LeastCount == null ? 0 : (int) x.ADI_LeastCount
                //        }
                //    ).ToList();
                return fadapter.GetAllPriorityProducts();
            }
            catch (Exception ex)
            {
                Utility.ErrorLog(ex, null);
            }
            return products;
        }
        #endregion

        #endregion

        #region [Utility Methods]
        private static String RemoveHtmlCharacters(String text)
        {
            text = text.Replace("&amp;", "&");
            text = text.Replace("+;", " ");
            return text;
        }

        private static string GetAbsoluteUrl(String Url)
        {
            String absoluteUrl = "";
            if (LoggedIn)
                absoluteUrl = "https://adiglobal.us:443" + Url;
            else
                absoluteUrl = "https://adiglobal.us" + Url;
            return absoluteUrl;
        }
        #endregion

        #region [GetAllUpdateProducts]
        public static List<AdiProduct> GetAllUpdateProducts(String AID_PART)
        {
            List<AdiProduct> updateAdiProducts = new List<AdiProduct>();
            //FinalTableManager finalTableTableAdapter = new FinalTableManager(finalcon);
            ////finalTableTableAdapter.Connection = finalcon;
            //var oDataTable = finalTableTableAdapter.GetAllAdiProducts();

            List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table> updateProducts = new List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table>();
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));
            UpdateFinalTableManager ftManager = new UpdateFinalTableManager(ConnStr);
            updateProducts = ftManager.GetDataByAdiPart(AID_PART);

            updateAdiProducts = updateProducts.Select(x => new AdiProduct()
            {
                VendorModel = x.VDR_PART,
                ProductDescription = x.VDR_IT_DSC,
                AdiNumber = x.AID_PART,
                Price = x.AID_COST,
                SmallImage = x.AID_IMG1,
                BigImage = x.AID_IMG2,
                VendorName = x.AID_VENDOR

            }).ToList();

            return updateAdiProducts;
        }

        public static List<AdiProduct> GetAllUpdateProducts()
        {
            List<AdiProduct> updateAdiProducts = new List<AdiProduct>();
            //FinalTableManager finalTableTableAdapter = new FinalTableManager(finalcon);
            ////finalTableTableAdapter.Connection = finalcon;
            //var oDataTable = finalTableTableAdapter.GetAllAdiProducts();

            List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table> updateProducts = new List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table>();
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));
            UpdateFinalTableManager ftManager = new UpdateFinalTableManager(ConnStr);
            updateProducts = ftManager.GetAllAdiProducts();

            updateAdiProducts = updateProducts.Select(x => new AdiProduct()
            {
                VendorModel = x.VDR_PART,
                ProductDescription = x.VDR_IT_DSC,
                AdiNumber = x.AID_PART,
                Price = x.AID_COST,
                SmallImage = x.AID_IMG1,
                BigImage = x.AID_IMG2,
                VendorName = x.AID_VENDOR

            }).ToList();

            return updateAdiProducts;
        }

        public static List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table> GetUpdateProducts()
        {
            List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table> updateProducts = new List<WebSpider.AdiGlobal.Objects.AdiExport.Final_Table>();
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));
            UpdateFinalTableManager ftManager = new UpdateFinalTableManager(ConnStr);
            updateProducts = ftManager.GetAllAdiProducts();
            return updateProducts;
        }
        #endregion
        
        #region [public static Tasks]

        #region [Login Check]
        private static void LoginCheck(TaskDetail taskDetail)
        {
            if (!taskDetail.IncognitoMode && !TriSpider.IsLoggedIn())
            {
                AdiSpider.Login();
            }
            //while (true)
            //{
            //    lock (LoginLock)
            //    {
            //        break;
            //    }
            //    System.Threading.Thread.Sleep(new Random().Next(10000));
            //}
        }
        #endregion

        #region [Ping Count]
        private static int pingRate = Settings.GetValue("PingRate");
        static int pingCount = 0;

        private static System.Timers.Timer pingTimer = new System.Timers.Timer();
        private static void TasksInit()
        {
            pingTimer.Interval = 60000;
            pingTimer.Elapsed += pingTimer_Elapsed;
            pingTimer.Enabled = true;
            pingTimer.Start();
        }

        private static void pingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            pingCount = 0;
        }

        private static void PingCheck()
        {
            while (true)
            {
                if (pingCount <= pingRate)
                {
                    pingCount++;
                    break;
                }
                else
                    System.Threading.Thread.Sleep(new Random().Next(1000));
            }
        }

        #endregion
        
        #region [CRAWL]
        public static void CrawlProduct(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager adiProductManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);

                    List<AdiProduct> products = new List<AdiProduct>();
                    if (taskDetail.TaskType == Constants.ADICatagory)
                        products = AdiSpider.ParseCatagoryProducts(taskDetail.TaskNameValue);
                    else
                        products = AdiSpider.GetBrandProducts(taskDetail.TaskNameValue);

                    double totalProducts = products.Count;

                    for (int index = 0; index < products.Count; index++)
                    {
                        AdiProduct p = products[index];
                        //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        try
                        {
                            PingCheck();
                            AdiSpider.SaveProduct(p, mode);
                            AdiSpider.GetProductDetails(p, taskDetail.DownloadImages);
                            if (taskDetail.IncognitoMode == false)
                            {
                                AdiSpider.GetProductInventory(p, mode);
                            }
                            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);

                        }
                        catch (Exception ex)
                        {
                            //Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            String json = JsonConvert.SerializeObject(p, Formatting.None);
                            Utility.ErrorLog(ex, json);
                            if (Settings.GetValue("MailErrors") == true)
                            {
                                Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                            }
                            HasError = true;
                        }
                        taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                        taskDetail.TaskStatus = TaskDetailStatus.Processing;
                        taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    //Utility.ApplicationLog(String.Format("AdiGlobal product Crawling complete for {0}",taskDetail.TaskNameText));
                }
                //if (HasError) throw new Exception();
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

        #region [Update]
        public static void UpdateProduct(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.UPDATE;
            bool HasError = false;
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal product Updating started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {
                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    if (taskDetail.TaskSite == Constants.SiteName.ADIGLOBAL)
                    {
                        
                        List<AdiProduct> products = new List<AdiProduct>();

                        LoginCheck(taskDetail);
                        products = AdiSpider.GetAllUpdateProducts(taskDetail.TaskNameValue);
                        double totalProducts = products.Count;
                        

                        for (int index = 0; index < products.Count; index++)
                        {
                            AdiProduct p = products[index];
                            //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            try
                            {
                                p = AdiSpider.GetProduct(p);

                                if (AdiSpider.SaveProduct(p, mode))
                                {
                                    PingCheck();
                                    try
                                    {
                                        AdiSpider.GetProductDetails(p, taskDetail.DownloadImages, mode);
                                    }
                                    catch (Exception ex)
                                    {
                                        String json = JsonConvert.SerializeObject(p, Formatting.Indented);
                                        Utility.ErrorLog(ex, json);
                                        if (Settings.GetValue("MailErrors") == true)
                                            Utility.ErrorLog(ex, json, "errmail.tmp");
                                        HasError = true;
                                    }
                                    try
                                    {
                                        AdiSpider.GetProductInventory(p, mode);
                                    }
                                    catch (Exception ex)
                                    {
                                        String json = JsonConvert.SerializeObject(p, Formatting.None);
                                        Utility.ErrorLog(ex, json);
                                        if (Settings.GetValue("MailErrors") == true)
                                            Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                                        HasError = true;
                                    }
                                    exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_UPDATE, p.AdiNumber);
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                //Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                                String json = JsonConvert.SerializeObject(p, Formatting.Indented);
                                Utility.ErrorLog(ex, json);
                                if (Settings.GetValue("MailErrors") == true)
                                {
                                    Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                                }
                                HasError = true;
                            }
                            taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                            taskDetail.TaskStatus = TaskDetailStatus.Processing;
                            taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                        }
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    //Utility.ApplicationLog(String.Format("AdiGlobal product Updating complete for {0}", taskDetail.TaskNameText));
                }
                //if (HasError) throw new Exception();
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                //Utility.ApplicationLog(String.Format("AdiGlobal product Updating completed with errors for {0}", taskDetail.TaskNameText));
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

        #region [Clearance Zone]
        public static void GetClearanzeZone(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal Clearance Zone product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);
                    List<AdiProduct> products = new List<AdiProduct>();
                    if (taskDetail.TaskType == Constants.ADICatagory)
                        products = AdiSpider.GetClearanceZoneCategoryProducts(taskDetail.TaskNameValue);
                    else
                        products = AdiSpider.GetClearanceZoneBrandProducts(taskDetail.TaskNameValue);
                    

                    double totalProducts = products.Count;
                    for (int index = 0; index < products.Count; index++)
                    {
                        AdiProduct p = products[index];
                        //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        try
                        {
                            PingCheck();
                            AdiSpider.SaveProduct(p, mode);
                            productManager.SetClearanceZone(p.AdiNumber);
                            AdiSpider.GetProductDetails(p, taskDetail.DownloadImages);
                            if (taskDetail.IncognitoMode == false)
                            {
                                AdiSpider.GetProductInventory(p, mode);
                            }
                            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);
                        }
                        catch (Exception ex)
                        {
                            Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            String json = JsonConvert.SerializeObject(p, Formatting.None);
                            Utility.ErrorLog(ex, json);
                            if (Settings.GetValue("MailErrors") == true)
                            {
                                Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                            }
                            HasError = true;
                        }
                        taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                        taskDetail.TaskStatus = TaskDetailStatus.Processing;
                        taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    //Utility.ApplicationLog(String.Format("AdiGlobal Clearance Zone product Crawling complete for {0}", taskDetail.TaskNameText));
                }
                //if (HasError) throw new Exception();
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog(String.Format("AdiGlobal Clearance Zone product Crawling completed with errors for {0}", taskDetail.TaskNameText));
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

        #region [Sale Center]
        public static void GetSaleCenter(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal Sale Center product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);
                    AdiSpider.LoggedIn = !taskDetail.IncognitoMode;
                    List<AdiProduct> products = new List<AdiProduct>();
                    if (taskDetail.TaskType == Constants.ADICatagory)
                        products = AdiSpider.GetSaleCenterCategoryProducts(taskDetail.TaskNameValue);
                    else
                        products = AdiSpider.GetSaleCenterBrandProducts(taskDetail.TaskNameValue);

                    double totalProducts = products.Count;
                    for (int index = 0; index < products.Count; index++)
                    {
                        AdiProduct p = products[index];
                        //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        try
                        {
                            PingCheck();
                            AdiSpider.SaveProduct(p, mode);
                            productManager.SetSaleCenter(p.AdiNumber);
                            AdiSpider.GetProductDetails(p, taskDetail.DownloadImages);
                            if (taskDetail.IncognitoMode == false)
                            {
                                AdiSpider.GetProductInventory(p, mode);
                            }
                            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);
                        }
                        catch (Exception ex)
                        {
                            Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            String json = JsonConvert.SerializeObject(p, Formatting.None);
                            Utility.ErrorLog(ex, json);
                            if (Settings.GetValue("MailErrors") == true)
                            {
                                Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                            }
                            HasError = true;
                        }
                        taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                        taskDetail.TaskStatus = TaskDetailStatus.Processing;
                        taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                }
                Utility.ApplicationLog(String.Format("AdiGlobal Sale Center product Crawling complete for {0}", taskDetail.TaskNameText));
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                //Utility.ApplicationLog(String.Format("AdiGlobal Sale Center product Crawling completed with errors for {0}", taskDetail.TaskNameText));
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

        #region [Online Specials]
        public static void GetOnlineSpecials(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal Online Specials product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);
                    AdiSpider.LoggedIn = !taskDetail.IncognitoMode;
                    List<AdiProduct> products = new List<AdiProduct>();
                    if (taskDetail.TaskType == Constants.ADICatagory)
                        products = AdiSpider.GetOnlineSpecialCategoryProducts(taskDetail.TaskNameValue);
                    else
                        products = AdiSpider.GetOnlineSpecialBrandProducts(taskDetail.TaskNameValue);

                    double totalProducts = products.Count;
                    
                    for (int index = 0; index < products.Count; index++)
                    {
                        AdiProduct p = products[index];
                        //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        try
                        {
                            PingCheck();
                            AdiSpider.SaveProduct(p, mode);
                            productManager.SetOnlineSpecials(p.AdiNumber);
                            AdiSpider.GetProductDetails(p, taskDetail.DownloadImages);
                            if (taskDetail.IncognitoMode == false)
                            {
                                AdiSpider.GetProductInventory(p, mode);
                            }
                            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);
                        }
                        catch (Exception ex)
                        {
                            Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            String json = JsonConvert.SerializeObject(p, Formatting.None);
                            Utility.ErrorLog(ex, json);
                            if (Settings.GetValue("MailErrors") == true)
                            {
                                Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                            }
                            HasError = true;
                        }
                        taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                        taskDetail.TaskStatus = TaskDetailStatus.Processing;
                        taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                }
                //Utility.ApplicationLog(String.Format("AdiGlobal Online Specials product Crawling complete for {0}", taskDetail.TaskNameText));
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog(String.Format("AdiGlobal Online Specials product Crawling completed with errors for {0}", taskDetail.TaskNameText));
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

        #region [Hot Deals]
        public static void GetHotDeals(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            int pingRate = 1;//Settings.GetValue("PingRate");
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal Hot Deals product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);
                    AdiSpider.LoggedIn = !taskDetail.IncognitoMode;
                    List<AdiProduct> products = new List<AdiProduct>();
                    if (taskDetail.TaskType == Constants.ADICatagory)
                        products = AdiSpider.GetHotDealsCategoryProducts(taskDetail.TaskNameValue);
                    else
                        products = AdiSpider.GetHotDealsBrandProducts(taskDetail.TaskNameValue);

                    double totalProducts = products.Count;
                    for (int index = 0; index < products.Count; index++)
                    {
                        AdiProduct p = products[index];
                        //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        try
                        {
                            PingCheck();
                            AdiSpider.SaveProduct(p, mode);
                            productManager.SetHotDeals(p.AdiNumber);
                            AdiSpider.GetProductDetails(p, taskDetail.DownloadImages);
                            if (taskDetail.IncognitoMode == false)
                            {
                                double inv = AdiSpider.GetProductInventory(p, mode);
                                
                            }
                            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);
                        }
                        catch (Exception ex)
                        {
                            Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            String json = JsonConvert.SerializeObject(p, Formatting.None);
                            Utility.ErrorLog(ex, json);
                            if (Settings.GetValue("MailErrors") == true)
                            {
                                Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                            }
                            HasError = true;
                        }
                        taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                        taskDetail.TaskStatus = TaskDetailStatus.Processing;
                        taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                }
                //Utility.ApplicationLog(String.Format("AdiGlobal Hot Deals product Crawling complete for {0}", taskDetail.TaskNameText));
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog(String.Format("AdiGlobal Hot Deals product Crawling completed with errors for {0}", taskDetail.TaskNameText));
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

        #region [In Stock]
        public static void GetInStockItems(Object objItem)
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager productManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            int pingRate = 1;//Settings.GetValue("PingRate");
            TaskDetail taskDetail = (TaskDetail)objItem;
            //Utility.ApplicationLog(String.Format("AdiGlobal In Stock product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {

                if (!ReferenceEquals(taskDetail, null))
                {
                    taskDetail.TaskStatusText = Constants.PROCESSING_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Processing;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);

                    LoginCheck(taskDetail);
                    AdiSpider.LoggedIn = !taskDetail.IncognitoMode;
                    List<AdiProduct> products = new List<AdiProduct>();
                    if (taskDetail.TaskType == Constants.ADICatagory)
                        products = AdiSpider.GetInStockCategoryProducts(taskDetail.TaskNameValue);
                    else
                        products = AdiSpider.GetInStockBrandProducts(taskDetail.TaskNameValue);

                    double totalProducts = products.Count;
                    for (int index = 0; index < products.Count; index++)
                    {
                        
                        AdiProduct p = products[index];
                        //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        try
                        {
                            PingCheck();
                            AdiSpider.SaveProduct(p, mode);
                            productManager.SetStockAvailability(p.AdiNumber);
                            AdiSpider.GetProductDetails(p, taskDetail.DownloadImages);
                            if (taskDetail.IncognitoMode == false)
                            {
                                AdiSpider.GetProductInventory(p, mode);
                            }
                            exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);
                        }
                        catch (Exception ex)
                        {
                            Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                            String json = JsonConvert.SerializeObject(p, Formatting.None);
                            Utility.ErrorLog(ex, json);
                            if (Settings.GetValue("MailErrors") == true)
                            {
                                Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                            }
                            HasError = true;
                        }
                        taskDetail.TaskStatusText = String.Format("{0} - {1}%", Constants.PROCESSING_TEXT, (index / totalProducts * 100).ToString("0"));
                        taskDetail.TaskStatus = TaskDetailStatus.Processing;
                        taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                    }
                    taskDetail.TaskStatusText = Constants.COMPLETED_TEXT;
                    taskDetail.TaskStatus = TaskDetailStatus.Completed;
                    taskDetailManager.UpdateStatus(taskDetail.TaskID, taskDetail.TaskStatusText, taskDetail.TaskStatus);
                }
                //Utility.ApplicationLog(String.Format("AdiGlobal In Stock product Crawling complete for {0}", taskDetail.TaskNameText));
            }
            catch (TaskCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                Utility.ApplicationLog(String.Format("AdiGlobal In Stock product Crawling completed with errors for {0}", taskDetail.TaskNameText));
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

        #region [ Least Count Mail for Products ]
        public static void ProcessProductLeastCount()
        {
            TaskDetailManager taskDetailManager = new TaskDetailManager(Constants.ConnectionString);
            ADIProductManager adiProductManager = new ADIProductManager(Constants.ConnectionString);
            AdiMode mode = AdiMode.CRAWL;
            bool HasError = false;
            //Utility.ApplicationLog(String.Format("AdiGlobal product Crawling started for {0}, Ignito Mode - {1}, Download Images - {2}", taskDetail.TaskNameText, taskDetail.IgnitoMode, taskDetail.DownloadImages));
            try
            {
                LoginCheck(new TaskDetail() { IncognitoMode = false });

                List<AdiProduct> products = new List<AdiProduct>();
                products = adiProductManager.GetAllPriorityProducts();

                double totalProducts = products.Count;
                
                for (int index = 0; index < products.Count; index++)
                {
                    AdiProduct p = products[index];
                    //Utility.ApplicationLog(String.Format("Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                    try
                    {
                        PingCheck();
                        AdiSpider.SaveProduct(p, mode);
                        AdiSpider.GetProductDetails(p, false);
                        double totalInventory = AdiSpider.GetProductInventory(p, mode);
                        if (totalInventory <= p.LeastCount)
                        {
                            //Send Mail
                            Utility.SendAlertMail("Stock Alert", "Product- " + p.AdiNumber + " Live Stock Quantity- " + totalInventory.ToString());
                            //Utility.LogFile(fileName, "Mail Send-" + productsList[index].AdiNumber);
                        }
                        //exportManager.Insert(Constants.SiteName.ADIGLOBAL, Constants.ExportType.ADI_PRODUCT_CRAWL, p.AdiNumber);

                    }
                    catch (Exception ex)
                    {
                        //Utility.ApplicationLog(String.Format("Error Processing Adi Product {0} {1}", p.AdiNumber, p.ProductDescription));
                        String json = JsonConvert.SerializeObject(p, Formatting.None);
                        Utility.ErrorLog(ex, json);
                        if (Settings.GetValue("MailErrors") == true)
                        {
                            Utility.ApplicationLog(String.Format("{0} {1}", p.PartNumber, ex.Message), Constants.EmailErrorFile);
                        }
                        HasError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.ApplicationLog(String.Format("AdiGlobal product Crawling completed with errors for {0}", taskDetail.TaskNameText));
                String json = null;
                Utility.ErrorLog(ex, json);
                if (Settings.GetValue("MailErrors") == true)
                    Utility.ApplicationLog(String.Format("{0}", ex.Message), Constants.EmailErrorFile);
            }
        }
        #endregion
        #endregion

        #region [ Export ]
        public static Boolean PendingExports()
        {
            return (!ReferenceEquals(exportManager.GetTopBySite(Constants.SiteName.ADIGLOBAL), null));
        }

        public static void Export(ref ToolStripStatusLabel lbl)
        {
            //List<FinalExport> finalExportList = exportManager.GetBySite(Constants.SiteName.ADIGLOBAL);
            List<FinalExport> finalExportList = new List<FinalExport>();
            finalExportList.Add(exportManager.GetTopBySite(Constants.SiteName.ADIGLOBAL));
            for(int index = 0; index < finalExportList.Count; index++)
            {
                FinalExport finalExport = finalExportList[index];
                if (finalExport == null)
                {
                    lbl.Text = String.Empty;
                    return;
                }
                if (!ReferenceEquals(lbl, null))
                {
                    lbl.Text = String.Format("Saving {0} {1} {2}", "AdiGlobal", finalExport.ExportType, finalExport.ExportValue);
                }
                try
                {
                    if (finalExport.ExportType == Constants.ExportType.ADI_BRAND)
                    {
                        // BRAND EXPORT
                        //Utility.ApplicationLog("Exporting Brands");
                        ExportBrands();
                        //Utility.ApplicationLog("Brands exported sucessfully");
                    }
                    else if (finalExport.ExportType == Constants.ExportType.ADI_CATEGORY)
                    {
                        // CATEGORY EXPORT
                        //Utility.ApplicationLog("Exporting Categories");
                        ExportCategories();
                        //Utility.ApplicationLog("Categories exported sucessfully");
                    }
                    else if (finalExport.ExportType == Constants.ExportType.ADI_PRODUCT_CRAWL)
                    {
                        // PRODUCT CRAWLING
                        //Utility.ApplicationLog("Exproting product");
                        ExportProduct(finalExport.ExportValue);
                        //Utility.ApplicationLog("Product exported sucessfully");
                    }
                    else if (finalExport.ExportType == Constants.ExportType.ADI_PRODUCT_UPDATE)
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

        #region [ Export Update Table ]

        public static void ExportProductUpdate(String AID_PART)
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("FinalTable"));
            OleDbConnection OleConn = new OleDbConnection(ConnStr);
            UpdateFinalTableManager finalTableManager = new UpdateFinalTableManager(ConnStr);
            AdiChildManager adiChildManager = new AdiChildManager(ConnStr);
            String DateFormat = Settings.GetValue("DateFormat");

            List<AdiProduct> productList = adiProductManager.GetDataByPartNum(AID_PART);
            foreach (var p in productList)
                finalTableManager.SaveAdiPart(0, "", p.PartNumber, p.ProductDescription, p.ProductImagePath
                    , p.ProductID, p.AdiNumber, (decimal)(p.Price == null ? 0 : p.Price), p.BigImage, p.SmallImage, p.VendorName, "0", p.LastUpdateDatetime.ToString(DateFormat));


            //List<WebSpider.Objects.AdiGlobal.ADIChild> productSpecification = adiProductSpecificationManager.GetDataByPartNumber(AID_PART);
            //foreach (var ps in productSpecification)
            //{
            //    var child = new WebSpider.Objects.AdiExport.ADI_Child()
            //    {
            //        PART_NUM = ps.PART_NUM,
            //        PropertyName = ps.PropertyName,
            //        PropertyValue = ps.PropertyValue
            //    };

            //    adiChildManager.Save(child);
            //}

            #region [ Product Specification]
            adiProductSpecificationManager.Delete(AID_PART);

            #region [ Bulk Insert ]
            DataTable prdSpecs = adiProductSpecificationManager.GetDataTableByPartNumber(AID_PART);
            string SQLps = "SELECT * FROM ADIChild WHERE ID IS NULL";
            string INSERTps = "INSERT INTO ADIChild (PART_NUM, PropertyName, PropertyValue)"
                + " VALUES (@PART_NUM, @PropertyName, @PropertyValue)";


            OleDbDataAdapter OleAdpPs = new OleDbDataAdapter(SQLps, OleConn);
            DataTable dtPs = new DataTable();
            OleAdpPs.Fill(dtPs);
            foreach (DataRow dRow in prdSpecs.Rows)
            {
                dtPs.Rows.Add(dtPs.NewRow().ItemArray = dRow.ItemArray);
            }
            OleAdpPs.InsertCommand = new OleDbCommand(INSERTps);
            OleAdpPs.InsertCommand.Parameters.Add("@PART_NUM", OleDbType.VarChar, 255, "PART_NUM");
            OleAdpPs.InsertCommand.Parameters.Add("@PropertyName", OleDbType.VarChar, 255, "PropertyName");
            OleAdpPs.InsertCommand.Parameters.Add("@PropertyValue", OleDbType.VarChar, 255, "PropertyValue");
            OleAdpPs.InsertCommand.Connection = OleConn;
            OleAdpPs.InsertCommand.Connection.Open();
            OleAdpPs.Update(dtPs);
            OleAdpPs.InsertCommand.Connection.Close();
            #endregion
            #endregion

            //List<ADIInventoryDetails> inventoryDetails = adiInventoryDetailsManager.GetDataByPartNumber(AID_PART);


            //ADIInventoryExport inventoryExport = adiInventoryExportManager.GetDataByProduct(AID_PART);

        }
        #endregion


        #region [ Export Crawl Table ]
        public static void ExportCategories()
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));
            CategoryExportManager categoryExportManager = new CategoryExportManager(ConnStr);
            //List<AdiCategory> categories = adiCategoryManager.GetData();
            DataTable categories = adiCategoryManager.GetDataTable();
            categoryExportManager.DeleteAll();
            //foreach (AdiCategory c in categories)
            //{
            //    categoryExportManager.Save(c.Value, c.DisplayName, c.ParentValue, c.CategoryUrl, c.ClearanceZone, c.SaleCenter, c.OnlineSpecials, c.HotDeals);
            //}

            categories.Columns.Remove("InStock");
            string SQL = "SELECT * FROM ADICategory WHERE [Value] IS NULL";
            string INSERT = "INSERT INTO ADICategory ([Value], DisplayName, ParentValue, CategoryUrl, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals)"
                + " VALUES (@Value, @DisplayName, @ParentValue, @CategoryUrl, @ClearanceZone, @SaleCenter, @OnlineSpecials, @HotDeals)";

            OleDbConnection OleConn = new OleDbConnection(ConnStr);
            OleDbDataAdapter OleAdp = new OleDbDataAdapter(SQL, OleConn);
            DataTable dt = new DataTable();
            OleAdp.Fill(dt);
            foreach (DataRow dRow in categories.Rows)
            {
                dt.Rows.Add(dt.NewRow().ItemArray = dRow.ItemArray);
            }
            OleAdp.InsertCommand = new OleDbCommand(INSERT);
            OleAdp.InsertCommand.Parameters.Add("@Value", OleDbType.VarChar, 255, "Value");
            OleAdp.InsertCommand.Parameters.Add("@DisplayName", OleDbType.VarChar, 255, "DisplayName");
            OleAdp.InsertCommand.Parameters.Add("@ParentValue", OleDbType.VarChar, 255, "ParentValue");
            OleAdp.InsertCommand.Parameters.Add("@CategoryUrl", OleDbType.VarChar, 255, "CategoryUrl");
            OleAdp.InsertCommand.Parameters.Add("@ClearanceZone", OleDbType.Boolean, 8, "ClearanceZone");
            OleAdp.InsertCommand.Parameters.Add("@SaleCenter", OleDbType.Boolean, 8, "SaleCenter");
            OleAdp.InsertCommand.Parameters.Add("@OnlineSpecials", OleDbType.Boolean, 8, "OnlineSpecials"); 
            OleAdp.InsertCommand.Parameters.Add("@HotDeals", OleDbType.Boolean, 8, "HotDeals");
            OleAdp.InsertCommand.Connection = OleConn;
            OleAdp.InsertCommand.Connection.Open();
            OleAdp.Update(dt);
            OleAdp.InsertCommand.Connection.Close();
        }

        public static void ExportBrands()
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));
            BrandExportManager brandExportManager = new BrandExportManager(ConnStr);
            //List<AdiBrand> brands = adiBrandManager.GetData();
            brandExportManager.DeleteAll();
            //foreach (AdiBrand b in brands)
            //{
            //    brandExportManager.Save(b.Value, b.DisplayName, b.ClearanceZone, b.SaleCenter, b.OnlineSpecials, b.HotDeals);
            //}


            DataTable brands = adiBrandManager.GetDataTable();
            brands.Columns.Remove("InStock");
            string SQL = "SELECT * FROM AdiBrand WHERE [Value] IS NULL";
            string INSERT = "INSERT INTO AdiBrand ([Value], DisplayName, ClearanceZone, SaleCenter, OnlineSpecials, HotDeals)"
                + " VALUES (@Value, @DisplayName,@ClearanceZone, @SaleCenter, @OnlineSpecials, @HotDeals)";

            OleDbConnection OleConn = new OleDbConnection(ConnStr);
            OleDbDataAdapter OleAdp = new OleDbDataAdapter(SQL, OleConn);
            DataTable dt = new DataTable();
            OleAdp.Fill(dt);
            foreach (DataRow dRow in brands.Rows)
            {
                dt.Rows.Add(dt.NewRow().ItemArray = dRow.ItemArray);
            }
            OleAdp.InsertCommand = new OleDbCommand(INSERT);
            OleAdp.InsertCommand.Parameters.Add("@Value", OleDbType.VarChar, 255, "Value");
            OleAdp.InsertCommand.Parameters.Add("@DisplayName", OleDbType.VarChar, 255, "DisplayName");
            OleAdp.InsertCommand.Parameters.Add("@ClearanceZone", OleDbType.Boolean, 8, "ClearanceZone");
            OleAdp.InsertCommand.Parameters.Add("@SaleCenter", OleDbType.Boolean, 8, "SaleCenter");
            OleAdp.InsertCommand.Parameters.Add("@OnlineSpecials", OleDbType.Boolean, 8, "OnlineSpecials");
            OleAdp.InsertCommand.Parameters.Add("@HotDeals", OleDbType.Boolean, 8, "HotDeals");
            OleAdp.InsertCommand.Connection = OleConn;
            OleAdp.InsertCommand.Connection.Open();
            OleAdp.Update(dt);
            OleAdp.InsertCommand.Connection.Close();
        }

        public static void ExportProduct(String AID_PART)
        {
            String ConnStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True", Settings.GetValue("WebSpiderDB"));
            OleDbConnection OleConn = new OleDbConnection(ConnStr);

            FinalTableManager finalTableManager = new FinalTableManager(ConnStr);
            AdiChildManager adiChildManager = new AdiChildManager(ConnStr);
            AdiInventoryDetailsManager adiExportInventoryDetailsManager = new AdiInventoryDetailsManager(ConnStr);
            AdiInventoryManager adiInventoryManager = new AdiInventoryManager(ConnStr);

            String DateFormat = Settings.GetValue("DateFormat");

            



            #region [ Product Specification ]
            adiChildManager.Delete(AID_PART);
            //List<WebSpider.Objects.AdiGlobal.ADIChild> productSpecification = adiProductSpecificationManager.GetDataByPartNumber(AID_PART);
            //foreach (var ps in productSpecification)
            //{
            //    var child = new WebSpider.Objects.AdiExport.ADI_Child()
            //    {
            //        PART_NUM = ps.PART_NUM,
            //        PropertyName = ps.PropertyName,
            //        PropertyValue = ps.PropertyValue
            //    };

            //    adiChildManager.Save(child);
            //}
            #region [ Bulk Insert ]
            DataTable prdSpecs = adiProductSpecificationManager.GetDataTableByPartNumber(AID_PART);
            string SQLps = "SELECT * FROM ADIChild WHERE ID IS NULL";
            string INSERTps = "INSERT INTO ADIChild (PART_NUM, PropertyName, PropertyValue)"
                + " VALUES (@PART_NUM, @PropertyName, @PropertyValue)";

            
            OleDbDataAdapter OleAdpPs = new OleDbDataAdapter(SQLps, OleConn);
            DataTable dtPs = new DataTable();
            OleAdpPs.Fill(dtPs);
            foreach (DataRow dRow in prdSpecs.Rows)
            {
                dtPs.Rows.Add(dtPs.NewRow().ItemArray = dRow.ItemArray);
            }
            OleAdpPs.InsertCommand = new OleDbCommand(INSERTps);
            OleAdpPs.InsertCommand.Parameters.Add("@PART_NUM", OleDbType.VarChar, 255, "PART_NUM");
            OleAdpPs.InsertCommand.Parameters.Add("@PropertyName", OleDbType.VarChar, 255, "PropertyName");
            OleAdpPs.InsertCommand.Parameters.Add("@PropertyValue", OleDbType.VarChar, 4000, "PropertyValue");
            OleAdpPs.InsertCommand.Connection = OleConn;
            OleAdpPs.InsertCommand.Connection.Open();
            OleAdpPs.Update(dtPs);
            OleAdpPs.InsertCommand.Connection.Close();
            #endregion
            #endregion

            #region [ Inventory Details ]
            adiExportInventoryDetailsManager.Delete(AID_PART);
            //List<ADIInventoryDetails> inventoryDetails = adiInventoryDetailsManager.GetDataByPartNumber(AID_PART);
            //foreach (ADIInventoryDetails detail in inventoryDetails)
            //{
            //    adiExportInventoryDetailsManager.Save(AID_PART, detail.id, detail.dc, detail.region, detail.storeName
            //        , detail.address1, detail.address2, detail.address3, detail.country, detail.city, detail.state
            //        , detail.stateName, detail.zip, detail.phone, detail.fax, detail.lat, detail.lon, detail.inventory
            //        , detail.manager, detail.responseCode, detail.responseMessage, detail.IsHub, detail.LastUpdate);
            //}

            #region [ Bulk Insert ]
            DataTable inventoryDetails = adiInventoryDetailsManager.GetDataTableByPartNumber(AID_PART);
            string SQLid = "SELECT * FROM AdiInventoryDetails WHERE 1=0;";
            string INSERTid = "INSERT INTO AdiInventoryDetails (AdiNumber, id, dc, region, storeName, address1, address2, address3, country, city, state, stateName, zip, phone, fax, lat, lon, inventory, manager, responseCode, responseMessage, IsHub, LastUpdate)"
                + " VALUES (@AdiNumber, @id, @dc, @region, @storeName, @address1, @address2, @address3, @country, @city, @state, @stateName, @zip, @phone, @fax, @lat, @lon, @inventory, @manager, @responseCode, @responseMessage, @IsHub, @LastUpdate )";

            OleDbDataAdapter OleAdpId = new OleDbDataAdapter(SQLid, OleConn);
            DataTable dtId = new DataTable();
            OleAdpId.Fill(dtId);
            foreach (DataRow dRow in inventoryDetails.Rows)
            {
                dtId.Rows.Add(dtId.NewRow().ItemArray = dRow.ItemArray);
            }
            OleAdpId.InsertCommand = new OleDbCommand(INSERTid);
            OleAdpId.InsertCommand.Parameters.Add("@AdiNumber", OleDbType.VarChar, 255, "AdiNumber");
            OleAdpId.InsertCommand.Parameters.Add("@id", OleDbType.VarChar, 255, "id");
            OleAdpId.InsertCommand.Parameters.Add("@dc", OleDbType.VarChar, 255, "dc");
            OleAdpId.InsertCommand.Parameters.Add("@region", OleDbType.VarChar, 255, "region");
            OleAdpId.InsertCommand.Parameters.Add("@storeName", OleDbType.VarChar, 255, "storeName");
            OleAdpId.InsertCommand.Parameters.Add("@address1", OleDbType.VarChar, 255, "address1");
            OleAdpId.InsertCommand.Parameters.Add("@address2", OleDbType.VarChar, 255, "address2");
            OleAdpId.InsertCommand.Parameters.Add("@address3", OleDbType.VarChar, 255, "address3");
            OleAdpId.InsertCommand.Parameters.Add("@country", OleDbType.VarChar, 255, "country");
            OleAdpId.InsertCommand.Parameters.Add("@city", OleDbType.VarChar, 255, "city");
            OleAdpId.InsertCommand.Parameters.Add("@state", OleDbType.VarChar, 255, "state");
            OleAdpId.InsertCommand.Parameters.Add("@stateName", OleDbType.VarChar, 255, "stateName");
            OleAdpId.InsertCommand.Parameters.Add("@zip", OleDbType.VarChar, 255, "zip");
            OleAdpId.InsertCommand.Parameters.Add("@phone", OleDbType.VarChar, 255, "phone");
            OleAdpId.InsertCommand.Parameters.Add("@fax", OleDbType.VarChar, 255, "fax");
            OleAdpId.InsertCommand.Parameters.Add("@lat", OleDbType.Single, 8, "lat");
            OleAdpId.InsertCommand.Parameters.Add("@lon", OleDbType.Single, 8, "lon");
            OleAdpId.InsertCommand.Parameters.Add("@inventory", OleDbType.VarChar, 255, "inventory");
            OleAdpId.InsertCommand.Parameters.Add("@manager", OleDbType.VarChar, 255, "manager");
            OleAdpId.InsertCommand.Parameters.Add("@responseCode", OleDbType.VarChar, 255, "responseCode");
            OleAdpId.InsertCommand.Parameters.Add("@responseMessage", OleDbType.VarChar, 255, "responseMessage");
            OleAdpId.InsertCommand.Parameters.Add("@IsHub", OleDbType.Boolean, 8, "IsHub");
            OleAdpId.InsertCommand.Parameters.Add("@LastUpdate", OleDbType.VarChar, 255, "LastUpdate");
            OleAdpId.InsertCommand.Connection = OleConn;
            OleAdpId.InsertCommand.Connection.Open();
            OleAdpId.Update(dtId);
            OleAdpId.InsertCommand.Connection.Close();
            #endregion
            #endregion

            #region [ Inventory Export ]
            ADIInventoryExport inventoryExport = adiInventoryExportManager.GetDataByProduct(AID_PART);
            if (!ReferenceEquals(inventoryExport, null))
            {
                adiInventoryManager.Save(inventoryExport.ID, inventoryExport.PART_NUM, inventoryExport.TotalInventory, inventoryExport.Dallas
                    , inventoryExport.DC_AtlantaHub, inventoryExport.DC_Dallas_Hub, inventoryExport.DC_Elk_Grove_Hub, inventoryExport.DC_Feura_Bush
                    , inventoryExport.DC_Louisville_Hub, inventoryExport.DC_Reno_Hub, inventoryExport.DC_Richmond_Dist_Ctr, inventoryExport.Oklahama
                    , inventoryExport.RemainingBranches, inventoryExport.LastUpdate);
            }
            #endregion

            #region [ Product ]
            List<AdiProduct> productList = adiProductManager.GetDataByPartNum(AID_PART);
            foreach (var p in productList)
            {
                //finalTableManager.SaveAdiPart(0, "", p.AdiNumber, p.ProductDescription, p.ProductImagePath
                //    , p.ProductID, p.AdiNumber, (decimal)(p.Price == null ? 0 : p.Price), p.BigImage, p.SmallImage, p.VendorName, "0", p.LastUpdateDatetime.ToString(DateFormat));

                //finalTableManager.UpdateFlagsByAdiProduct(p.AdiNumber, p.ClearanceZone, p.HotDeals, p.OnlineSpecials, p.SaleCenter, p.InStock, p.LastUpdateDatetime.ToString(DateFormat));

                String inv = "0";
                if (!ReferenceEquals(inventoryExport, null) && inventoryExport.TotalInventory != null)
                {
                    //finalTableManager.UpdateInvBYPartNO(inventoryExport.TotalInventory.ToString(), DateTime.Now.ToString(Settings.GetValue("DateFormat")), inventoryExport.PART_NUM);
                    inv = inventoryExport.TotalInventory.ToString();
                }
                finalTableManager.SaveAdiPart(0, "", p.AdiNumber, p.ProductDescription, p.ProductImagePath
                    , p.ProductID, p.AdiNumber, (decimal)(p.Price == null ? 0 : p.Price), p.BigImage, p.SmallImage, p.VendorName, inv
                    , p.ClearanceZone, p.HotDeals, p.OnlineSpecials, p.SaleCenter, p.InStock, p.LastUpdateDatetime.ToString(DateFormat));

            }

            #endregion
        }
        #endregion
        #endregion

    }
}