using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class AppEvent
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser{ get; set; }


        public int CityId { get; set; }
        public City City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

    }
}