using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class CityStateViewModel
    {
        public City City { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public State State { get; set; }
        public IEnumerable<State> States { get; set; }

    }
}