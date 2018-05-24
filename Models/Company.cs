using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class Company
    {
        public byte[] image { get; set; }
        [Required(ErrorMessage = "The ID field is required")]
        public int id { get; set; }
        [Required(ErrorMessage = "The Name field is required")]
        public string companyName { get; set; }
        public string companyType { get; set; }
        public string ceoName { get; set; }
        public int totalProjects { get; set; }
        public int pendingProjects { get; set; }
        public string headquarters { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        public int phoneNumber { get; set; }
    }
}