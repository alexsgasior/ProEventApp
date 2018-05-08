using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class ProEventsListIndexViewModel
    {
        public IEnumerable<AppEvent> AppEvents { get; set; }
        public IEnumerable<AppEvent> ProCategoryList { get; set; }
        
    }
}