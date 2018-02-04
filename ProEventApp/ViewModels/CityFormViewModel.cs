using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class CityFormViewModel
    {
        public City City { get; set; }
        public IEnumerable<State> States { get; set; }
    }
}