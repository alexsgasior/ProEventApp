using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProEventApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        
        [DisplayName("Upload File")]
        public byte[] Bytes { get; set; }

        public string ContentBase64 { get; set; }
        public string Comment { get; set; }
        public string WhoAdded { get; set; }


    }
}