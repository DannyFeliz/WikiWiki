using Blog.Controllers.Repositorios;
using Blog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WikiWiki.Models;
using Blog.Models;

namespace Blog.Controllers
{
     [RoleAttribute(Roles = "Administrador")]
    
    public class AdministracionController : Controller
    {
        RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
        UsersContext db = new UsersContext();
        //
        // GET: /Administracion/

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Usuarios(int pagina = 1){

            var user = repositorioUsuario.getUsuarios().ToPagedList(pagina, 10);
            
            return View(user);
        }

        public ActionResult Detalles(int id = 0)
        {
            var detalles = repositorioUsuario.getDetalles(id);

            return View(detalles);
        }

        public ActionResult CambiarRol(int id = 0)
        {
            ViewBag.usuarioID = id;

            var roles = repositorioUsuario.getRoles();

            return View(roles);
        }
        
        [HttpPost]
        public ActionResult CambiarRol(int usuarioID, int asignacion)
        {
            repositorioUsuario.CambiarRol(usuarioID, asignacion);

            ViewBag.mensaje = "Actualizacion saticfactoria";

            return RedirectToAction("Usuarios", "Administracion");
        }

        public ActionResult CambiarEstado(int id)
        {
            repositorioUsuario.CambiarEstado(id);
            return RedirectToAction("Usuarios", "Administracion");
        }

        public ActionResult Categoria() {
            return View();
        }

        [HttpPost]
        public ActionResult Categoria(Categoria datosCategoria) {
            if(ModelState.IsValid){
                db.categoria.Add(new Categoria { 
                    categoria = datosCategoria.categoria
                });
                db.SaveChanges();
                return RedirectToAction("ListaCategoria"); ;
            }
            return View();
        }

        public ActionResult ListaCategoria()
        {
            return View(db.categoria.ToList());
        }

        public ActionResult EditarCategoria(int id = 0) { 
            var model = db.categoria.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarCategoria(int id = 0, string categoria = null)
        {
            db.categoria.Find(id).categoria = categoria;
            db.SaveChanges();
            return RedirectToAction("ListaCategoria");
        }

        public ActionResult EliminarCategoria(int id = 0)
        {
            db.categoria.Remove(db.categoria.Find(id));
            db.SaveChanges();
            return RedirectToAction("ListaCategoria", "Administracion");
        }

    }
}
