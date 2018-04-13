using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class ListOfProInvsViewModel
    {
        public IEnumerable<EventProfessional> EventProfessionals { get; set; }
        public IEnumerable<InvitationStatus> InvitationStatuses { get; set; }
        public IEnumerable<EventImage> EventImages { get; set; }

    }
}