using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.AdiGlobal.Objects.AdiGlobal
{
    public class AdiProduct
    {
        public String ABCCode { get; set; }
        public String AdiNumber { get; set; }
        public String AllowedToBuy { get; set; }
        public String DangerousGoodsMessage { get; set; }
        public String InventoryMessage { get; set; }
        public String MarketingMessage { get; set; }
        public decimal? MinQty { get; set; }
        public String ModelNumber { get; set; }
        public decimal? Price { get; set; }
        public String ProductDescription { get; set; }
        public String ProductImagePath { get; set; }
        public String RecycleFee { get; set; }
        public String SaleMessageIndicator { get; set; }
        public String SaleType { get; set; }
        public String VendorName { get; set; }
        public String VendorNumber { get; set; }
        public String ST { get; set; }
        public String SMI { get; set; }
        public String InventoryMessageCode { get; set; }
        public String CatagoryID { get; set; }
       

        public String SmallImage { get; set; }
        public String BigImage { get; set; }
        public String Name { get; set; }
        public String VendorModel { get; set; }
        public String PartNumber { get; set; }

        public String Url { get; set; }

        public DateTime LastUpdateDatetime { get; set; }
        public int LeastCount { get; set; }

        public List<String> ProductFeatures { get; set; }

        public AdiProduct()
        {
            ProductFeatures = new List<string>();
            Specification = new List<ProductSpeficiation>();
        }


        public string ProductID { get; set; }
        public List<ProductSpeficiation> Specification { get; set; }
        public long ID { get; set; }
        public Boolean PriorityProduct { get; set; }
        public bool IsUpdating { get; set; }
        public int UpdateInterval { get; set; }

        public bool ClearanceZone { get; set; }
        public bool HotDeals { get; set; }
        public bool OnlineSpecials { get; set; }
        public bool SaleCenter { get; set; }
        public bool InStock { get; set; }
    }

    public class ProductSpeficiation
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
