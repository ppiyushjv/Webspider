using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSpider.Objects.General
{
    public class TaskDetailStatus
    {
        public const int Open = 1;
        public const int Pending = 2;
        public const int Processing = 3;
        public const int Completed = 4;
        public const int CompletedWithError = 5;
    }
}
