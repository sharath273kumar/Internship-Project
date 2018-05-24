using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class ClientProject
    {
        public IEnumerable<Client> client { get; set; }
        public IEnumerable<Project> project { get; set; }
    }
}