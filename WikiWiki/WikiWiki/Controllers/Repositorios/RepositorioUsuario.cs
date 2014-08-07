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
        public string validarUsuario(string usuario, string clave)
        {
            string mensaje = "";

            var validar = db.Userios.FirstOrDefault(r => r.usuario1.ToLower() == usuario.ToLower());

            if(validar != null){
                validar = db.Userios.FirstOrDefault(r => r.usuario1.ToLower() == usuario.ToLower() && r.clave == clave);
                if (validar != null)
                {
                    mensaje = "";
                }
                else
                {
                    mensaje = "La contraseña es incorrecta. Intente nuevamente.";
                }
            }
            else
            {
                mensaje = "El usuario no existe.";
            }

            if(mensaje == ""){
                if(validar.estado != 6){
                    mensaje = "Lo sentimos el usuario está desactivado";
                }
            }

            return mensaje;
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
                .Join(db.Roles_de_usuario, u => u.usuario_id, r => r.usuario_id, (u, r) => new { usuario_id = u.usuario_id, usuario1 = u.usuario1, nombreCompleto = u.nombreCompleto, direccion = u.direccion, ocupacion = u.ocupacion, nacimiento = u.nacimiento, email = u.email, rol_id = r.rol_id, foto = u.foto, estado = u.estado })
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
            var id = db.Userios.FirstOrDefault(u => u.usuario1.ToLower() == usuario.ToLower()).usuario_id;

            return id;
        }

        // Get nombre de usuario por id
        public string getNombreDeUsuario(int id)
        {
            return db.Userios.FirstOrDefault(u => u.usuario_id == id).usuario1;
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
            var estado = 4;

            var user = db.Userios.FirstOrDefault(u => u.usuario_id == id);
            if(user.estado == 4){
                estado = 6;
            }

            db.Userios.FirstOrDefault(u => u.usuario_id == id)
                .estado = estado;
            db.SaveChanges();
        }

        public string getEmailUsuario(string usuario)
        {
            var email = db.Userios.FirstOrDefault(u => u.usuario1 == usuario).email;
            if(email == null || email == ""){
                email = "";
            }

            return email;
        }

        public string existe(string usuario, string email)
        {
            var validar = "";
            try
            {
                validar = db.Userios.FirstOrDefault(u => u.usuario1.ToLower() == usuario.ToLower() || u.email.ToLower() == email.ToLower()).usuario1;

                if (validar != null || validar != "")
                {
                    validar = "El usuario ya existe";
                }
                else
                {
                    validar = "";
                }
            }
            catch
            {
                validar = "";
            }

            return validar;
        }

        // Obtener registro del mismo usuario
        public Registro getRegistro(int id)
        {
            var registro = db.Registros
                .Join(db.Userios, r => r.registro_id, u => u.registro_id, (r, u) => new { nombre = r.nombre, apellido = r.apellido });
            return null;
        }

        // Encriptacion MD5
        public string md5(string itemToHash)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
        }
    }
}