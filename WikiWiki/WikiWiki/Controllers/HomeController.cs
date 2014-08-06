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
using PagedList;

namespace Blog.Controllers
{
    
    public class HomeController : Controller
    {
        private UsersContext db = new UsersContext();
        private RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
        private RepositorioPublicacion repositorioPublicacion = new RepositorioPublicacion();

        //
        // GET: /BlogPost/

        public ActionResult Publicaciones(int pagina = 1, String busqueda = null)
        {
            if (busqueda == null)
            {
                ViewBag.busqueda = "";
                return View(repositorioPublicacion.getTodasLasPublicaciones().ToPagedList(pagina, 10));
            }
            else
            {
                ViewBag.busqueda = busqueda;
                return View(repositorioPublicacion.buscador(busqueda).ToPagedList(pagina, 10));
            }

        }

        // Publicaciones mas vistas
        public ActionResult PublicacionesMasVisitadas(int pagina = 1)
        {
            return View(repositorioPublicacion.masVisitas().ToPagedList(pagina, 10));
        }

        // Publicaciones por autor
        public ActionResult PublicacionesPorAutor(int autor, int pagina = 1)
        {
            ViewBag.autor = autor;
            ViewBag.autorUsuario = repositorioUsuario.getNombreDeUsuario(autor);
            return View(repositorioPublicacion.porAuthor(autor).ToPagedList(pagina, 10));
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

        // Mostrar informacion seleccionado
        public ActionResult Informacion(int id = 0)
        {
            if(id != 0){
                repositorioPublicacion.contadorDeVisitas(id);
            }
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
            blogpost.listaDeCategorias = repositorioPublicacion.listaDeCategoria();

            return View(blogpost);
        }

        //
        // POST: /BlogPost/Create

        [HttpPost]
        [RoleAttribute(Roles = "Administrador,Editor")]
        public ActionResult Create(publicaciones blogpost)
        {
            // verificacion del post al guardar
            var agregar = repositorioPublicacion.agregar(blogpost);
            if (ModelState.IsValid && agregar == "")
            {
                return RedirectToAction("publicaciones");
            }
            ViewBag.error = agregar;
            blogpost.listaDeCategorias = repositorioPublicacion.listaDeCategoria();
           
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