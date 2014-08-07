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
        [Display(Name="Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        public string apellido { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        public string nacimiento { get; set; }
        public System.DateTime hora_de_registro { get; set; }
        public string direccion_ip { get; set; }
        public string email { get; set; }
        [Display(Name = "Sexo")]
        public string direccion { get; set; }
        [Display(Name = "Ocupacion")]
        public string ocupacion { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        [NotMapped]
        public virtual int usuario_id { get; set; }


        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
