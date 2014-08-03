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

namespace Blog.Controllers
{
    
    public class HomeController : Controller
    {
        private UsersContext db = new UsersContext();

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
            publicaciones blogpost = db.publicaciones.Find(id);
            if (blogpost == null)
            {
                return HttpNotFound();
            }
            return View(blogpost);
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
        public ActionResult Create(publicaciones blogpost)
        {
            if (ModelState.IsValid)
            {
                /*blogpost.fecha_publicacion = DateTime.Now;
                blogpost.direccion_ip = "localhost";
                */
                db.publicaciones.Add(new publicaciones { titulo = blogpost.titulo, informacion = blogpost.informacion, categoria_id = blogpost.categoria_id, usuario_id = blogpost.usuario_id, fuente_de_informacion = blogpost.fuente_de_informacion, fecha_publicacion = DateTime.Now, estado_id = 1 });
                db.SaveChanges();

                //return Content("" + blogpost.Aprovacions + " " + blogpost.categoria + " " + blogpost.direccion_ip + " " + blogpost.estado_id + " " + blogpost.fecha_publicacion + " " + blogpost.fuente_de_informacion + " " + blogpost.informacion + " " + blogpost.publicacion_id + " " + blogpost.titulo +" " + blogpost.visitas + " " + blogpost.usuario_id);

                //return Content("" + blogpost.usuario_id);
                return RedirectToAction("Index");//Modificar esto
            }

            return View(blogpost);
        }

        //
        // GET: /BlogPost/Edit/5

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
        public ActionResult Edit(publicaciones blogpost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogpost);
        }

        //
        // GET: /BlogPost/Delete/5

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

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            publicaciones blogpost = db.publicaciones.Find(id);
            db.publicaciones.Remove(blogpost);
            db.SaveChanges();
            return RedirectToAction("Index");//Modicar esto
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