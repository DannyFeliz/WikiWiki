using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Registro
    {
        [Key]
        public int registro_id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nacimiento { get; set; }
        public System.DateTime hora_de_registro { get; set; }
        public string direccion_ip { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public string ocupacion { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
