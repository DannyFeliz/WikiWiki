﻿using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WikiWiki.Models;

namespace WikiWiki.Controllers.Repositorios
{
    public class RepositorioPublicacion
    {
        private UsersContext db = new UsersContext();

        // Traer todas las publicaciones del propio usuario
        public List<publicaciones> getPublicacionesDelUsuario(int id)
        {
            return db.publicaciones.Where(u => u.usuario_id == id && u.estado_id != 5).ToList();
        }

        // Actualizar publicacion del mismo usuario
        public void actualizacion(publicaciones blogpost)
        {
            var estado = 1;

            db.Aprovaciones.FirstOrDefault(u => u.publicacion_id == blogpost.publicacion_id).estado_id = estado;

            var post = db.publicaciones.FirstOrDefault(u => u.publicacion_id == blogpost.publicacion_id);
            post.informacion = blogpost.informacion;
            post.titulo = blogpost.titulo;
            post.fuente_de_informacion = blogpost.fuente_de_informacion;
            post.estado_id = estado;

            db.SaveChanges();
        }

        // Eliminacion de usuario (cambio de estado)
        public void eliminar(int id)
        {
            var estado = 5;
            db.Aprovaciones.FirstOrDefault(u => u.publicacion_id == id).estado_id = estado;
            db.publicaciones.FirstOrDefault(u => u.publicacion_id == id).estado_id = estado;

            db.SaveChanges();
        }

        // Mostrar publicacion segun su id
        public publicaciones getPublicacion(int id)
        {
            var r = db.publicaciones.Join(db.Userios, p => p.usuario_id, u => u.usuario_id, (p, u) => new { publicacion_id = p.publicacion_id, fecha_publicacion = p.fecha_publicacion, titulo = p.titulo, fuente_de_informacion = p.fuente_de_informacion, categoria_id = p.categoria_id, usuarioP = u.usuario1, visitas = p.visitas })
                .Join(db.categoria, p => p.categoria_id, c => c.id, (p, c) => new { publicacion_id = p.publicacion_id, fecha_publicacion = p.fecha_publicacion, titulo = p.titulo, fuente_de_informacion = p.fuente_de_informacion, usuarioP = p.usuarioP, categoriaP = c.categoria, visitas = p.visitas }).FirstOrDefault(u => u.publicacion_id == id);

            var publicacion = new publicaciones { publicacion_id = r.publicacion_id, fecha_publicacion = r.fecha_publicacion, titulo = r.titulo, fuente_de_informacion = r.fuente_de_informacion, usuarioP = r.usuarioP, categoriaP = r.categoriaP, visitas = r.visitas };

            return publicacion;
        }
    }
}