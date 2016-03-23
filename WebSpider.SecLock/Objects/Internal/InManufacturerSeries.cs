using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.SecLock.Objects.Internal
{
    public class InManufacturerSeries
    {
        public long ID { get; set; }
        public String Name { get; set; }

        public List<InProduct> Products { get; set; }

        public InManufacturerSeries()
        {
            Products = new List<InProduct>();
        }
    }
}
