using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.TriGlobal.Objects.TriGlobal
{
    public class TriSearchCriteria
    {
        public String BooleanOperator { get; set; }
        public String Property { get; set; }
        public String PropertyOperator { get; set; }
        public String Type { get; set; }
        public String Value { get; set; }

        public TriSearchCriteria()
        {
            BooleanOperator = "1";
            Property = String.Empty;
            PropertyOperator = "2";
            Type = "1";
            Value = String.Empty;
        }
    }

    public class AdsRequerstCriteria
    {
        public List<TriSearchCriteria> SearchCriterias { get; set; }
        public String CategoryName { get; set; }
        public String SortBy { get; set; }
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }
        public Boolean ReturnRefinersOnly { get; set; }
        public int PageCode { get; set; }
        public String ExcludedRefiners { get; set; }
        public String SearchTerm { get; set; }

        public AdsRequerstCriteria()
        {
            SearchCriterias = new List<TriSearchCriteria>();
            CategoryName = String.Empty;
            SortBy = "b";
            PageNumber = 1;
            ResultsPerPage = 100;
            ReturnRefinersOnly = false;
            PageCode = 6;
            ExcludedRefiners = String.Empty;
            SearchTerm = String.Empty;
        }
    }

    public class AdsRequest
    {
        public String Rcat { get; set; }
        public String FirstParentRcat { get; set; }
        public String VendorId { get; set; }
        public String Mode { get; set; }
        public String PromoOption { get; set; }
        public String SearchTerm { get; set; }

        public AdsRequest()
        {
            Rcat = "0000";
            FirstParentRcat = null;
            VendorId = "";
            Mode = "b";
            PromoOption = null;
            SearchTerm = null;
        }
    }


    #region [ Tri Request ]
    public class TriRequest
    {
        public AdsRequerstCriteria request { get; set; }
        public AdsRequest Adsrequest { get; set; }
        public TriRequest()
        {
            request = new AdsRequerstCriteria();
            Adsrequest = new AdsRequest();
        }
    }
    #endregion
    
}
