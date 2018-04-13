using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class State
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Maximal length of state's name must be smaller than 50")]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}