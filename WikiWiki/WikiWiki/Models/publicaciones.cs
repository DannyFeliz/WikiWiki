using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public partial class publicaciones
    {
        public publicaciones()
        {
            this.Aprovacions = new List<Aprovacion>();
            this.Visita_de_publicacion = new List<Visita_de_publicacion>();
            this.Estado = new List<Estado>();
            this.Usuario = new List<Usuario>();
        }

        [Key]
        public int publicacion_id { get; set; }
        public int usuario_id { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Required]
        public string informacion { get; set; }
        public DateTime fecha_publicacion { get; set; }
        [Required]
        public string titulo { get; set; }
        public int visitas { get; set; }
        public string direccion_ip { get; set; }
        [Required]
        public int estado_id { get; set; }
        [Required]
        public string fuente_de_informacion { get; set; }
        public int categoria_id { get; set; }

        public virtual ICollection<Aprovacion> Aprovacions { get; set; }
        public virtual ICollection<Estado> Estado { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Visita_de_publicacion> Visita_de_publicacion { get; set; }
    }
}
