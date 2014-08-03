using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Visita_de_publicacion
    {
        [Key]
        public int visitas_id { get; set; }
        public string direccion_ip { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public int publicacion_id { get; set; }
        public Nullable<int> usuario_id { get; set; }
        public virtual publicaciones publicacione { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
