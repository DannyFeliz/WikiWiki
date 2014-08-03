using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WikiWiki.Models;

namespace Blog.Controllers.Repositorios
{
    public class RepositorioUsuario
    {
        private UsersContext db = new  UsersContext ();

        // Validar usuario
        public bool validarUsuario(string usuario, string clave)
        {
            //bool valido = false;

            var user = db.Userios.FirstOrDefault(r => r.usuario1.ToLower() == usuario.ToLower() && r.clave == clave);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }

            //return valido;
        }

        // Saber el rol del usuario
        public string getRolUsuario(string usuarios)
        {
            var user = from u in db.Userios
                       join rol in db.Roles_de_usuario
                       on u.usuario_id equals rol.usuario_id
                       join tipo in db.Roles
                       on rol.rol_id equals tipo.rol_id
                       where u.usuario1 == usuarios
                       select new {tipo.tipo};

            if(user != null){
                foreach(var u in user){
                    return u.tipo;
                }
            }

           return "Visitante";
        }

        // Lista de usuarios
        public List<Usuario> getUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            var user = db.Userios
                .Join(db.Registros, u => u.registro_id, r => r.registro_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = r.nombre + " " + r.apellido, direccion = r.direccion, ocupacion = r.ocupacion, nacimiento = r.nacimiento, email = u.email, foto = u.foto, estado = u.estado })
                .Join(db.Roles_de_usuario, u => u.usuario_id, r => r.usuario_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = r.rol_id, foto = u.foto, estado = u.estado })
                .Join(db.Roles, u => u.rol_id, r => r.rol_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = r.rol_id, rol = r.tipo, foto = u.foto, estado = u.estado });

            foreach(var u in user){
                usuarios.Add(new Usuario { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = u.rol_id, rol = u.rol, foto = u.foto, estado = u.estado });
            }

            return usuarios;
        }

        // informacion completa del usuario
        public Usuario getDetalles(int id)
        {
            Usuario detalle = null;

            var user = db.Userios.Join(db.Registros, u => u.registro_id, r => r.registro_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = r.nombre + " " + r.apellido, direccion = r.direccion, ocupacion = r.ocupacion, nacimiento = r.nacimiento, email = u.email, foto = u.foto, estado = u.estado })
                .Join(db.Roles_de_usuario, u => u.usuario_id, r => r.rol_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = r.rol_id, foto = u.foto, estado = u.estado })
                .Join(db.Roles, u => u.rol_id, r => r.rol_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = r.rol_id, rol = r.tipo, foto = u.foto, estado = u.estado })
                .Where(u => u.usuario_id == id);

            foreach(var u in user)
            {
                detalle = new Usuario { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = u.rol_id, rol = u.rol, foto = u.foto, estado = u.estado };
            }

            return detalle;
        }

        // Lista de los roles
        public List<Role> getRoles()
        {
            var roles = db.Roles;

            return roles.ToList();
        }

        // Get ID del usaurio
        public int getIdUsuario(String usuario)
        {
            var id = db.Userios.FirstOrDefault(u => u.usuario1 == usuario).usuario_id;

            return id;
        }

        // Cambiar rol del usuario
        public void CambiarRol(int id, int nuevoRol)
        {
            db.Roles_de_usuario.FirstOrDefault(u => u.usuario_id == id)
                .rol_id = nuevoRol;
            db.SaveChanges();
        }

        // Cambio de estados del usuario
        public void CambiarEstado(int id)
        {
            var estado = 0;

            var user = db.Userios.FirstOrDefault(u => u.usuario_id == id);
            if(user.estado == 0){
                estado = 1;
            }

            db.Userios.FirstOrDefault(u => u.usuario_id == id)
                .estado = estado;
            db.SaveChanges();
        }

        // Encriptacion MD5
        public string md5(string itemToHash)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
        }
    }
}