using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class Client
    {
        public int clientId { get; set; }
        public string clientName { get; set; }
        public int partnerCompanyId { set; get; }
        public int totalProject { get; set; }
        public int pendingProject { get; set; }
        public DateTime submissionDate{get;set;}
        public string email { get; set; }
        public int phoneNumber { get; set; }
        public string partnerCompanyName { get; set; }
    }
}