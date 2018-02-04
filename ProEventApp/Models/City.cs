using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        public State State { get; set; }
        public IEnumerable<Person> Persons { get; set; }

    }
}