using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.SecLock.Objects.External
{
    public class FinalTable
    {
        public String SLD_SOURCE_ID { get; set; }
        public Decimal? SLD_COST { get; set; }
        public String SLD_PART { get; set; }
        public String SLD_IMG1 { get; set; }
        public String SLD_IMG2 { get; set; }
        public String SLD_VENDOR { get; set; }
        public String SLD_INV { get; set; }
        public string SLD_DESC { get; set; }
        public string SLD_TECHDOC { get; set; }
        public String SLD_LastUpdate { get; set; }
    }
}
