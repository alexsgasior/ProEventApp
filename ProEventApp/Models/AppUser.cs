using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    [Table("AppUser")]
    public class AppUser: Person
    {
        public string Nickname { get; set; }
        public IEnumerable<AppEvent> Events { get; set; }
        public string CurrentUserId { get; set; }


    }
}