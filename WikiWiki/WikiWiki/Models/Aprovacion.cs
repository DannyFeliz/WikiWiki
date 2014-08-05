using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Aprovacion
    {
        public Aprovacion()
        {
            this.Estado = new List<Estado>();
            this.publicacione = new List<publicaciones>();
            this.Usuario = new List<Usuario>();
        }
        [Key]
        public int aprovacion_id { get; set; }
        public int usuario_id { get; set; }
        [Display(Name="Fecha de rechazo")]
        public DateTime fecha_de_aprovacion { get; set; }
        public int estado_id { get; set; }
        public int publicacion_id { get; set; }
        public string motivos { get; set; }
        [NotMapped]
        public virtual string usuarioA { get; set; }
        [NotMapped]
        public virtual string usuarioPublicacion { get; set; }
        [NotMapped]
        public virtual string usuarioAprobacion { get; set; }

        public virtual ICollection<Estado> Estado { get; set; }
        public virtual ICollection<publicaciones> publicacione { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
