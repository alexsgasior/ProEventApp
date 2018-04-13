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
        [Required(ErrorMessage = "You have to have your prefered name!")]
        public string PreferedName { get; set; }
        [Required(ErrorMessage = "Please type your life motto")]
        public string Motto { get; set; }
        public string About { get; set; }
        public string WhoCreated { get; set; }
    }
}