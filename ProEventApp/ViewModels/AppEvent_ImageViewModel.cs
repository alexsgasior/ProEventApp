using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class AppEvent_ImageViewModel
    {
        public AppEvent Event { get; set; }
        public IEnumerable<EventImage> EventImages { get; set; }

    }
}