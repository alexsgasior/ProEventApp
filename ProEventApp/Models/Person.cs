﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Maximal length of name must be smaller than 50")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Maximal length of surname must be smaller than 50")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]        
        public long Pesel { get; set; }
        public string Phone { get; set; }
        //public int AddressId { get; set; }
        //public Address Address { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
        
    }
}