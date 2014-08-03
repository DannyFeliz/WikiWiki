using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Blog.Models
{
    public partial class Permiso
    {
        [Key]
        public int permiso_id { get; set; }
        public string acceso { get; set; }
    }
}
