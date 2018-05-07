using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class UserProComment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public Professional Professional { get; set; }
        public int ProfessionalId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public string Who { get; set; }
    }
}