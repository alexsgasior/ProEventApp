using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace ProEventApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(25)]
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

    }
}