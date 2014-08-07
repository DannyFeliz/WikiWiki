using Blog.Controllers.Repositorios;
using Blog.Filters;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WikiWiki.Controllers
{
    [RoleAttribute(Roles="Administrador,Editor")]
    public class PerfilController : Controller
    {
        private RepositorioUsuario repositorio = new RepositorioUsuario();

        // GET: Registro
        public ActionResult Index()
        {
            var registro = repositorio.getRegistro(repositorio.getIdUsuario(User.Identity.Name));

            return View(registro);
        }

        [HttpPost]
        public ActionResult Index(Registro datos)
        {
            repositorio.actualizar(datos);
            ViewBag.mensaje = "Datos actualizados";

            
            return View(datos);
        }
    }
}