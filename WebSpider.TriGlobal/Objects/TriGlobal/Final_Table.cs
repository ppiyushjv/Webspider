//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSpider.TriGlobal.Objects.TriGlobal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Final_Table
    {
        public long ID { get; set; }
        public string UPC { get; set; }
        public string VDR_PART { get; set; }
        public string VDR_IT_DSC { get; set; }
        public string Image_Folder { get; set; }
        public string AID_SOURCE_ID { get; set; }
        public string AID_PART { get; set; }
        public decimal? AID_COST { get; set; }
        public string AID_IMG1 { get; set; }
        public string AID_IMG2 { get; set; }
        public string AID_VENDOR { get; set; }
        public string AID_INV { get; set; }
        public string AID_LastUpdate { get; set; }


        public bool Tri_ClearanceZone { get; set; }
        public bool Tri_HotDeals { get; set; }
        public bool Tri_OnlineSpecials { get; set; }
        public bool Tri_SaleCenter { get; set; }
        public bool Tri_InStock { get; set; }
    }
}
