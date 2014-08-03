using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public partial class Role
    {
        public Role()
        {
            this.Roles_de_usuario = new List<Roles_de_usuario>();
        }

        [Key]
        public int rol_id { get; set; }
        public string tipo { get; set; }
        public virtual ICollection<Roles_de_usuario> Roles_de_usuario { get; set; }
    }
}
