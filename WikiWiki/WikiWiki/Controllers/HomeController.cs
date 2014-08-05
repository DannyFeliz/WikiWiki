using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using System.Globalization;
using Blog.Filters;
using Blog.Controllers.Repositorios;
using System.Web.Security;
using WikiWiki.Models;
using WikiWiki.Controllers.Repositorios;

namespace Blog.Controllers
{
    
    public class HomeController : Controller
    {
        private UsersContext db = new UsersContext();
        private RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
        private RepositorioPublicacion repositorioPublicacion = new RepositorioPublicacion();

        //
        // GET: /BlogPost/

        public ActionResult Publicaciones(String busqueda = null)
        {
            //p => p.estado_id == 2 &&
            //(p => busqueda == null || p.titulo.StartsWith(busqueda) || p.t)
            if (busqueda == null)
            {
                var model = db.publicaciones
               .Where(p => p.estado_id == 2)
              .OrderBy(r => r.fecha_publicacion)
              .Take(20).ToList();
                return View(model);
            }
            else if (busqueda != null)
            {
                var model = db.publicaciones
                    .Where(p => p.estado_id == 2 && p.titulo.Contains(busqueda) || p.informacion.Contains(busqueda))
                   .OrderBy(r => r.fecha_publicacion)
                   .Take(20).ToList();
                if (model.Count() > 0)
                {
                    return View(model);
                }
                else {
                  

                    return View(model);
                }

            }
            else {
                var mensaje = String.Format("No se encontró ningun resultado");

                ViewBag.Message = mensaje;

                return View(mensaje);
            }

        }

        public ActionResult Acerca() {

            return View();
        }

        public ActionResult Inicio()
        {

            var model = db.publicaciones
                .Where(p => p.estado_id == 2)
                .OrderByDescending(r => r.fecha_publicacion)
                .Take(6).ToList();
            
            //            return View(db.BlogPosts.ToList());

            var texto = "Hola soy un texto";
            texto.Substring(0, 5);
            return View(model);
            
        }
        public ActionResult Contact() {
            return View();
        }
      
        public ActionResult Index()
        {

            return RedirectToAction("Inicio");
        }

        //
        // GET: /BlogPost/Details/5

        public ActionResult Informacion(int id = 0)
        {

            return View(repositorioPublicacion.getPublicacion(id));
        }

        //
        // GET: /BlogPost/Create
        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Create()
        {
            publicaciones blogpost = new publicaciones();
            RepositorioUsuario id = new RepositorioUsuario();


            blogpost.usuario_id = id.getIdUsuario(User.Identity.Name);
            blogpost.categoria_id = 1;

            return View(blogpost);
        }

        //
        // POST: /BlogPost/Create

        [HttpPost]
        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Create(publicaciones blogpost)
        {
            if (ModelState.IsValid)
            {
                db.publicaciones.Add(new publicaciones { titulo = blogpost.titulo, informacion = blogpost.informacion, categoria_id = blogpost.categoria_id, usuario_id = blogpost.usuario_id, fuente_de_informacion = blogpost.fuente_de_informacion, fecha_publicacion = DateTime.Now, estado_id = 1 });
                db.SaveChanges();
                
                return RedirectToAction("publicaciones");
            }

            return View(blogpost);
        }

        // Obtener publicaciones de los propios usuarios
        public ActionResult MisPublicaciones(int pagina = 1)
        {
            var id = repositorioUsuario.getIdUsuario(User.Identity.Name);
            var publicaciones = repositorioPublicacion.getPublicacionesDelUsuario(id);

            return View(publicaciones);
        }
        //
        // GET: /BlogPost/Edit/5

        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Edit(int id = 0)
        {
            publicaciones blogpost = db.publicaciones.Find(id);
            if (blogpost == null)
            {
                return HttpNotFound();
            }
            return View(blogpost);
        }

        //
        // POST: /BlogPost/Edit/5

        [HttpPost]
        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Edit(publicaciones blogpost)
        {
            if (ModelState.IsValid)
            {
                repositorioPublicacion.actualizacion(blogpost);

                return RedirectToAction("MisPublicaciones");
            }
            return View(blogpost);
        }

        //
        // GET: /BlogPost/Delete/5
        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Delete(int id = 0)
        {
            publicaciones blogpost = db.publicaciones.Find(id);
            if (blogpost == null)
            {
                return HttpNotFound();
            }
            return View(blogpost);
        }

        //
        // POST: /BlogPost/Delete/5

        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Eliminar(int id)
        {
            repositorioPublicacion.eliminar(id);

            return RedirectToAction("MisPublicaciones");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ViewResult About()
        {
            throw new NotImplementedException();
        }
    }
}