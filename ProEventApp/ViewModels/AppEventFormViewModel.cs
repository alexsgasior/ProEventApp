using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class AppEventFormViewModel
    {
        //public AppUser AppUser { get; set; }


        public IEnumerable<Status> Statuses { get; set; }
        //public Status Status { get; set; }


        public AppEvent AppEvent { get; set; }


        //public EventImage EventImage { get; set; }
        //public IEnumerable<EventImage> EventImages { get; set; }


        public IEnumerable<City> Cities { get; set; }
        //public City City { get; set; }


        //public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public AppUser AppUser { get; set; }
        public Image Image { get; set; }
        
        
    }
}