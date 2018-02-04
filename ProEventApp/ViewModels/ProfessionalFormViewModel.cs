using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class ProfessionalFormViewModel
    {
        public Professional Professional { get; set; }
        public IEnumerable<Profession> Professions { get; set; }
    }
}