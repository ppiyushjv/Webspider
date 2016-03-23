﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.TriGlobal.Objects.TriExport
{
    public class Tri_Category
    {
        //public String RootValue { get; set; }
        //public String RootDisplayName { get; set; }
        public String ParentValue { get; set; }
        //public String ParentDisplayName { get; set; }
        public String Value { get; set; }
        public String DisplayName { get; set; }
        public String CategoryUrl { get; set; }

        public bool ClearanceZone { get; set; }
        public bool SaleCenter { get; set; }
        public bool OnlineSpecials { get; set; }
        public bool HotDeals { get; set; }
    }
}
