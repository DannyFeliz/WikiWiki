using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Blog.Models
{
    public partial class Estado
    {
        public Estado()
        {
            this.Aprovacions = new List<Aprovacion>();
            this.publicaciones = new List<publicaciones>();
        }

        [Key]
        public int estado_id { get; set; }
        public string estado1 { get; set; }

        public virtual ICollection<Aprovacion> Aprovacions { get; set; }
        public virtual ICollection<publicaciones> publicaciones { get; set; }
    }
}
