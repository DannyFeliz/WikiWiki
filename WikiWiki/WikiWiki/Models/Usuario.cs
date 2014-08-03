using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Usuario
    {
        [Display(Name = "ID")]
        [Key]
        public int usuario_id { get; set; }
        public string clave { get; set; }
        public int registro_id { get; set; }
        public string foto { get; set; }
        [Display(Name = "Usuario")]
        public string usuario1 { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Estado")]
        public int estado { get; set; }
        [NotMapped]
        [Display(Name = "Direccion")]
        public virtual string direccion { get; set; }
        [NotMapped]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual string nacimiento { get; set; }
        [NotMapped]
        [Display(Name = "Ocupacion")]
        public virtual string ocupacion { get; set; }
        [NotMapped]
        public virtual int rol_id { get; set; }
        [NotMapped]
        [Display(Name = "Rol")]
        public virtual string rol { get; set; }
        [NotMapped]
        [Display(Name = "Nombre completo")]
        public virtual string nombreCompleto { get; set; }
    }
}
