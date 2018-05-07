using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public static class RoleName
    {
        public const string Professional = "Professional";
        public const string AppUser = "AppUser";
        public const string Admin = "Admin";

    }

    public enum ScoringRange
    {
        VeryLow =1,
        Low = 2,
        Medium = 3, 
        High = 4 , 
        VeryHigh =5
    }

}