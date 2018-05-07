﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string Unique { get; set; }
        public string Who { get; set; }

    }
}