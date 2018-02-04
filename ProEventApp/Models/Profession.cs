using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class Profession
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Professional> Professionals { get; set; }

    }
}