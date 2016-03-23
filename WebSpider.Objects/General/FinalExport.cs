using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Objects.General
{
    public partial class FinalExport
    {
        public String ExportSite { get; set; }
        public String ExportType { get; set; }
        public String ExportValue { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
