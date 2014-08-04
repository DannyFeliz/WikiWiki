using Blog.Controllers.Repositorios;
using Blog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
     [RoleAttribute(Roles = "Administrador")]
    
    public class AdministracionController : Controller
    {
        RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
        //
        // GET: /Administracion/

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Usuarios(){

            var user = repositorioUsuario.getUsuarios();
            
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
        
    }
}
