using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.SecLock.Objects.Internal
{
    public class InProduct
    {
        public String Name { get; set; }
        public String Url { get; set; }
        public String Code { get; set; }
        public Decimal YourPrice { get; set; }
        public Decimal ListPrice { get; set; }
        public String ImageUrl1 { get; set; }
        public String ImageUrl2 { get; set; }
        public String Stock { get; set; }
        public String Description { get; set; }
        public String TechDoc { get; set; }

        public string ManufacturerCode { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerSeries { get; set; }

        public string CategoyCode { get; set; }
        public string CategoryName { get; set; }

        
    }
}
