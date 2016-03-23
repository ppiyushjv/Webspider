using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.AdiGlobal.Objects.AdiGlobal
{
    public class AdiBrand
    {
        public string Value { get; set; }
        public string DisplayName { get; set; }

        public bool ClearanceZone { get; set; }
        public bool HotDeals { get; set; }
        public bool OnlineSpecials { get; set; }
        public bool SaleCenter { get; set; }
        public bool InStock { get; set; }
    }
}
