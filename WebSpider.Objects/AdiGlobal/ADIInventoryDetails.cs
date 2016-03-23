using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Objects.AdiGlobal
{
  public  class ADIInventoryDetails
    {
        public String id { get; set; }
        public String dc { get; set; }
        public String region { get; set; }
        public String storeName { get; set; }
        public String address1 { get; set; }
        public String address2 { get; set; }
        public String address3 { get; set; }
        public String country { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String stateName { get; set; }
        public String zip { get; set; }
        public String phone { get; set; }
        public String fax { get; set; }
        public float? lat { get; set; }
        public float? lon { get; set; }
        public String inventory { get; set; }
        public String manager { get; set; }
        public String responseCode { get; set; }
        public String responseMessage { get; set; }
        public Boolean IsHub { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
