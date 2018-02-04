using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class EventImage
    {
        public int Id { get; set; }
        public int AppEventId { get; set; }
        public AppEvent AppEvent { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public string Description { get; set; }
    }
}