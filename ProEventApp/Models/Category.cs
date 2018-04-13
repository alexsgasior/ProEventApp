using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class Category
    {
        [Display(Name = "Category")]
        public int Id { get; set; }        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public ICollection<Profession> Professions { get; set; }

    }
}