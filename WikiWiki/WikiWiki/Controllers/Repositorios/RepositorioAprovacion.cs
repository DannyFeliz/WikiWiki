using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WikiWiki.Models;

namespace Blog.Controllers.Repositorios
{
    public class RepositorioAprovacion
    {
        private UsersContext db = new UsersContext();

        // Publicaciones para aprobar
        public List<Por_aprovar> porAprovar()
        {
            return db.Por_aprovar.ToList();
        }

        // Publicacion por aprobar
        public Por_aprovar aprovacion(int id)
        {
            var aprovar = db.Por_aprovar.FirstOrDefault(m => m.publicacion_id == id);
            aprovar.estados = estados();

            return aprovar;
        }

        // Aprobacions de la publicacion
        public void aprovar(int id, int usuario)
        {
            var estado = 2;

            // Actualizar tabla de aprovaciones
            Aprovacion pendiente = db.Aprovaciones.FirstOrDefault(u => u.publicacion_id == id);
            pendiente.usuario_id = usuario;
            pendiente.estado_id = estado;

            // Actualizar la tabla de publicaciones
            db.publicaciones.FirstOrDefault(u => u.publicacion_id == id).estado_id = estado;

            // Update aplicaciones
            db.Entry(pendiente).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void rechazar(int id, int estado, int usuario, string motivo)
        {
            // Actualizar tabla de aprovaciones
            Aprovacion pendiente = db.Aprovaciones.FirstOrDefault(u => u.publicacion_id == id);
            pendiente.usuario_id = usuario;
            pendiente.estado_id = estado;
            pendiente.motivos = motivo;

            // Actualizar la tabla de publicaciones
            db.publicaciones.FirstOrDefault(u => u.publicacion_id == id).estado_id = estado;

            // Update aplicaciones
            db.Entry(pendiente).State = EntityState.Modified;

            db.SaveChanges();
        }

        public List<Estado> estados()
        {
            List<Estado> mostrar = new List<Estado>();

            foreach(var i in db.Estados){
                if(i.estado_id != 1 && i.estado_id != 2 && i.estado_id != 6){
                    mostrar.Add(new Estado { estado_id = i.estado_id, estado1 = i.estado1 });
                }
                
            }

            return mostrar;
        }
    }
}