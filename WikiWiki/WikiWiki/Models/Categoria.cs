using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Blog.Models
{
    public class Categoria
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string categoria { get; set; }
    }
}