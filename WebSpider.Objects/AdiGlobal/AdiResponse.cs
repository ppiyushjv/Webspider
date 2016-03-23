using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Objects.AdiGlobal
{
    public class AdiResponseInformation
    {
        public String Copyright { get; set; }
        public String Message { get; set; }
        public String Url { get; set; }

        public AdiResponseInformation()
        {
            Copyright = String.Empty;
            Message = String.Empty;
            Url = String.Empty;
        }
    }

    public class AdiResponseStatus
    {
        public String Code { get; set; }
        public String Id { get; set; }
        public String MajorFunction { get; set; }
        public String Message { get; set; }
        public String SubFunction { get; set; }

        public AdiResponseStatus()
        {
            Code = String.Empty;
            Id = String.Empty;
            MajorFunction = String.Empty;
            Message = String.Empty;
            SubFunction = String.Empty;
        }
    }

    public class AdiResponseRefinerOption
    {
        public String DisplayName { get; set; }
        public String Id { get; set; }
        public Int32 RecordCount { get; set; }
        public Boolean Selected { get; set; }
        public String Value { get; set; }

        public AdiResponseRefinerOption()
        {
            DisplayName = String.Empty;
            Id = String.Empty;
            RecordCount = 0;
            Selected = false;
            Value = String.Empty;
        }
    }

    public class AdiResponseRefiners
    {
        public String DisplayName { get; set; }
        public List<AdiResponseRefinerOption> Options { get; set; }
        public String RefinerName { get; set; }
        public Int32 TotalRefinerCount { get; set; }

        public AdiResponseRefiners()
        {
            DisplayName = String.Empty;
            Options = new List<AdiResponseRefinerOption>();
            RefinerName = String.Empty;
            TotalRefinerCount = 0;
        }
    }

    public class AdiResponseProducts
    {
        public Int32 CurrentPage { get; set; }
        public Boolean IsAuthenticated { get; set; }
        public Int32 PageSize { get; set; }
        public List<AdiProduct> Products { get; set; }
        public String SortBy { get; set; }
        public Int32 TotalPages { get; set; }
        public Int32 TotalResults { get; set; }

        public AdiResponseProducts ()
        {
            CurrentPage = 0;
            IsAuthenticated = false;
            PageSize = 0;
            Products = new List<AdiProduct>();
            SortBy = String.Empty;
            TotalPages = 0;
            TotalResults = 0;
        }
    }

    public class AdiResponseResponse
    {
        public String CategoryParentageDescription { get; set; }
        public String CategoryParentageName { get; set; }
        public AdiResponseProducts Products { get; set; }
        public String RedirectURL { get; set; }
        public List<AdiResponseRefiners> Refiners { get; set; }

        public AdiResponseResponse ()
        {
            CategoryParentageDescription = String.Empty;
            CategoryParentageName = String.Empty;
            Products = new AdiResponseProducts();
            RedirectURL = String.Empty;
            Refiners = new List<AdiResponseRefiners>();
        }
    }

    public class AdiResponse
    {
        public AdiResponseInformation Information { get; set; }
        public AdiResponseResponse Response { get; set; }
        public AdiResponseStatus Status { get; set; }

        public AdiResponse()
        {
            Information = new AdiResponseInformation();
            Response = new AdiResponseResponse();
            Status = new AdiResponseStatus();
        }
    }
}
