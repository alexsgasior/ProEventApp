using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [Required]
        public string PreferedName { get; set; }
        [Required]
        public string Motto { get; set; }
        public string About { get; set; }
    }
}