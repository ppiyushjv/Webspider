using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Objects.General
{
    public class TaskHeader
    {
        public Int64 ScheduleID { get; set; }
        public String TaskName { get; set; }
        public String TaskDescription { get; set; }
        public String Site { get; set; }

        public DateTime ScheduleFrom { get; set; }
        public Boolean TaskRepeat { get; set; }
        public Int32 TaskRepeatInterval { get; set; }
        public String TaskRepeatUnit { get; set; }

        public Boolean Enabled { get; set; }
        public DateTime? LastRun { get; set; }
        public DateTime? NextRun { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
