using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class DevOpsRequest
    {
        public string Message { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public int TimeToLifeSec { get; set; }
    }

    public class DevOpsResponse
    {
        public string Message { get; set; }
    }
}
