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
        [Required(ErrorMessage = "Maximal length of name must be smaller than 50")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Maximal length of description must be smaller than 800")]
        [StringLength(800)]
        public string Description { get; set; }
        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Professional> Professionals { get; set; }
        
    }
}