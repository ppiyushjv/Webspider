using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.TriGlobal.Objects.TriGlobal
{
    public class TriResponseInformation
    {
        public String Copyright { get; set; }
        public String Message { get; set; }
        public String Url { get; set; }

        public TriResponseInformation()
        {
            Copyright = String.Empty;
            Message = String.Empty;
            Url = String.Empty;
        }
    }

    public class TriResponseStatus
    {
        public String Code { get; set; }
        public String Id { get; set; }
        public String MajorFunction { get; set; }
        public String Message { get; set; }
        public String SubFunction { get; set; }

        public TriResponseStatus()
        {
            Code = String.Empty;
            Id = String.Empty;
            MajorFunction = String.Empty;
            Message = String.Empty;
            SubFunction = String.Empty;
        }
    }

    public class TriResponseRefinerOption
    {
        public String DisplayName { get; set; }
        public String Id { get; set; }
        public Int32 RecordCount { get; set; }
        public Boolean Selected { get; set; }
        public String Value { get; set; }

        public TriResponseRefinerOption()
        {
            DisplayName = String.Empty;
            Id = String.Empty;
            RecordCount = 0;
            Selected = false;
            Value = String.Empty;
        }
    }

    public class TriResponseRefiners
    {
        public String DisplayName { get; set; }
        public List<TriResponseRefinerOption> Options { get; set; }
        public String RefinerName { get; set; }
        public Int32 TotalRefinerCount { get; set; }

        public TriResponseRefiners()
        {
            DisplayName = String.Empty;
            Options = new List<TriResponseRefinerOption>();
            RefinerName = String.Empty;
            TotalRefinerCount = 0;
        }
    }

    public class TriResponseProducts
    {
        public Int32 CurrentPage { get; set; }
        public Boolean IsAuthenticated { get; set; }
        public Int32 PageSize { get; set; }
        public List<TriProduct> Products { get; set; }
        public String SortBy { get; set; }
        public Int32 TotalPages { get; set; }
        public Int32 TotalResults { get; set; }

        public TriResponseProducts ()
        {
            CurrentPage = 0;
            IsAuthenticated = false;
            PageSize = 0;
            Products = new List<TriProduct>();
            SortBy = String.Empty;
            TotalPages = 0;
            TotalResults = 0;
        }
    }

    public class TriResponseResponse
    {
        public String CategoryParentageDescription { get; set; }
        public String CategoryParentageName { get; set; }
        public TriResponseProducts Products { get; set; }
        public String RedirectURL { get; set; }
        public List<TriResponseRefiners> Refiners { get; set; }

        public TriResponseResponse ()
        {
            CategoryParentageDescription = String.Empty;
            CategoryParentageName = String.Empty;
            Products = new TriResponseProducts();
            RedirectURL = String.Empty;
            Refiners = new List<TriResponseRefiners>();
        }
    }

    public class TriResponse
    {
        public TriResponseInformation Information { get; set; }
        public TriResponseResponse Response { get; set; }
        public TriResponseStatus Status { get; set; }

        public TriResponse()
        {
            Information = new TriResponseInformation();
            Response = new TriResponseResponse();
            Status = new TriResponseStatus();
        }
    }
}
