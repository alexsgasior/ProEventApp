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
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser{ get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public City City { get; set; }
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(25)]
        public string HouseNumber { get; set; }
        public DateTime Date { get; set; }
        public string Findid { get; set; }
        public bool Done { get; set; }
        public string Tags { get; set; }

    }
}