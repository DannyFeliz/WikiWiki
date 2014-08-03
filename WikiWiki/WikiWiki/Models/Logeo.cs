using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Logeo
    {
        [Key]
        public int id { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public bool recordarme { get; set; }
    }
}