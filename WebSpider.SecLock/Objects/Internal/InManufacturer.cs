using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.SecLock.Objects.Internal
{
    public class InManufacturer
    {
        public long ID { get; set; }
        public String ImagePath { get; set; }
        public String Url { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<InManufacturerSeries> SeriesList { get; set; }

        public InManufacturer()
        {
            SeriesList = new List<InManufacturerSeries>();
        }
    }
}
