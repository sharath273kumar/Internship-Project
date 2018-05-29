using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Tracking
    {
        public int trackId { get; set; }
        public string projectName { get; set; }
        public int projectId { get; set; }
        public string issue { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string remark { get; set; }
    }
}