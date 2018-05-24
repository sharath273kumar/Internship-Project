using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Project
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public int clientCompanyId { get; set; }
        public string status { get; set; }
        public DateTime deadline { get; set; }
        public int budget { get; set; }
    }
}