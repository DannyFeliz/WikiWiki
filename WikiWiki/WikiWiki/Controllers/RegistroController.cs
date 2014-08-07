using Blog.Controllers.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WikiWiki.Controllers
{
    public class RegistroController : Controller
    {
        private RepositorioUsuario repositorio = new RepositorioUsuario();

        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }
    }
}