using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class UserProfileViewModel
    {
        public AppUser AppUser { get; set; }
        public Profile Profile { get; set; }
    }
}