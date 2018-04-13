using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class EventProfessional
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double Importance { get; set; }
        public decimal Price { get; set; }
        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        public InvitationStatus InvitationStatus { get; set; }
        public int AppEventId { get; set; }
        public AppEvent AppEvent { get; set; }
        public int InvitationStatusId { get; set; }
        public string Rola { get; set; }
        
    }
}