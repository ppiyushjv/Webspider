using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.SecLock.Objects.Internal
{
    public class InCategory
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<InProduct> Products;

        public InCategory()
        {
            Products = new List<InProduct>();
        }
    }
}
