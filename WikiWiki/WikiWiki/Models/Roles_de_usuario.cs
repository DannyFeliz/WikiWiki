using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Roles_de_usuario
    {
        [Key]
        public int id { get; set; }
        public int rol_id { get; set; }
        public int usuario_id { get; set; }
        public virtual Role Role { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
