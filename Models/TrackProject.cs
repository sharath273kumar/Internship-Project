using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagement.Models;
namespace ProjectManagement.Models
{
    public class TrackProject
    {
        public IEnumerable<Project> project {get;set;}
        public IEnumerable<Tracking> tracking { get; set; }
    }
}