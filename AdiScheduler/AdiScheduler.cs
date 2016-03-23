using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized;
using System.IO;
using System.Configuration;
using WebSpider.Objects;
using WebSpider.Core;
using WebSpider;

namespace WebSpiderScheduler
{
    public class AdiScheduler
    {
        private const string fileName="Log.txt";
        public static void Start()
        {

        }

        public static void Stop()
        {
        }

        public static void Pause()
        {

        }

        private static bool processing;
        public static void Process()
        {
            Utility.LogFile(fileName, "Process Start");
            if (!processing)
            {
                processing = true;
                AdiSpider adiSpider = new AdiSpider();
                
                try
                {
                    //AdiSpider AdiSpider = new AdiSpider();
                    if (adiSpider.Login())
                    {
                        Utility.LogFile(fileName, "Login Done");
                        List<AdiProduct> productsList = adiSpider.GetAllPriorityProducts();
                        Utility.LogFile(fileName, "Product Found-" + productsList.Count.ToString());
                        for (int index = 0; index < productsList.Count; index++)
                        {
                            //if (productsList[index].LastUpdateDatetime <= System.DateTime.Now.AddDays(-1))
                            //{
                                AdiProduct product = productsList[index];

                                //product = oSpider.GetProduct(product);
                                //oSpider.SaveProduct(product, false);
                                //oSpider.GetProductDetails(product, true);
                                double total = adiSpider.GetProductInventory(product, false);
                                if (total < productsList[index].LeastCount)
                                {
                                    //Send Mail
                                    Utility.SendAlertMail("Stock Alert", "Product- " + productsList[index].AdiNumber + " Stock Quantity- " + total.ToString());
                                    Utility.LogFile(fileName, "Mail Send-" + productsList[index].AdiNumber);
                                }
                            //}
                        }
                    }
                    else
                    {
                        Utility.SendAlertMail("Login Failed", "Login Failed for http://adiglobal.us");
                    }
                    Utility.LogFile(fileName, "Process End");
                }
                catch(Exception ex) {
                    Utility.ErrorLog(ex, null);
                }
                processing = false;
            }
        }
    }
}
