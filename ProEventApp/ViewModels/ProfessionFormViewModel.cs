using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class ProfessionFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Profession Profession { get; set; }
    }
}