using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Por_aprovar
    {
        [Key]
        public int publicacion_id { get; set; }
        public int usuario_id { get; set; }
        [Display(Name = "Titulo")]
        public string titulo { get; set; }
        [Display(Name = "Informacion")]
        public string informacion { get; set; }
        [Display(Name = "Usuario")]
        public string usuario { get; set; }
        public int estado_id { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }
        public string foto { get; set; }
        [Display(Name = "Fecha de publicacion")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime fecha_publicacion { get; set; }
        [Display(Name = "Categoria")]
        public string categoria { get; set; }
        [NotMapped]
        public virtual List<Estado> estados { get; set; }
    }
}
