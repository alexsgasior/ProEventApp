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
        [Required]
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public string CompanyWWW { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }
        public string CurrentUserId { get; set; }

    }
}