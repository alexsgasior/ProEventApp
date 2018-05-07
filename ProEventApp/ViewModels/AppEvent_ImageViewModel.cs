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
        public IEnumerable<EventProfessional> EventProfessionals { get; set; }

        public IEnumerable<EventComment> Comments{ get; set; }
        public EventComment EventComment { get; set; }
        public EventProfessional EventProfessional { get; set; }
    }
}