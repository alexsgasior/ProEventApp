using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class ProfileImage
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        
    }
}