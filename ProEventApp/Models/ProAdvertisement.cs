using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class ProAdvertisement
    {
        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}