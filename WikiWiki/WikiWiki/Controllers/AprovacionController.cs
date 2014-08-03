using Blog.Controllers.Repositorios;
using Blog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
   // [RoleAttribute(Roles="Administrador")]
    public class AprovacionController : Controller
    {
        private RepositorioAprovacion repositorio = new RepositorioAprovacion();
        private RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
       
        //
        // GET: /Aprovacion/

        public ActionResult Index()
        {

            return View(repositorio.porAprovar());
        }

        // Procesar los articulos
        public ActionResult Procesar(int id = 0){

            return View(repositorio.aprovacion(id));
        }

        // Aprovar
        public ActionResult Aprovar(int id = 0)
        {
            repositorio.aprovar(id, repositorioUsuario.getIdUsuario(User.Identity.Name));

           return RedirectToAction("Index", "Aprovacion");
        }

        // Aprovar
        public ActionResult Rechazar(int id = 0, string motivos = "")
        {

            return RedirectToAction("Index","Aprovacion");
        }
    }
}
