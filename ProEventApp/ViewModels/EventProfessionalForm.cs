using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProEventApp.Models;

namespace ProEventApp.ViewModels
{
    public class EventProfessionalForm
    {
        public IEnumerable<InvitationStatus> InvitationStatuses { get; set; }
        public EventProfessional EventProfessional { get; set; }

    }
}