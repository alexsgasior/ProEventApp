using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    [Table("Professional")]
    public class Professional: Person
    {
        [Required(ErrorMessage = "Maximal length of Company Name must be smaller than 150")]
        [StringLength(150)]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(13)]
        public string NIP { get; set; }
        public string CompanyWWW { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }
        public string CurrentUserId { get; set; }
        
    }
}