using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class ProfileViewModel
    {
        public Professional Professional { get; set; }
        public Profile Profile { get; set; }
        public AppUser AppUser { get; set; }
        public IEnumerable<ProfileImage> ProfileImages { get; set; }
    }
}