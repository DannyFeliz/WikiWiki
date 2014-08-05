using Blog.Controllers.Repositorios;
using Blog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Blog.Controllers
{
   // [RoleAttribute(Roles="Administrador")]
    public class AprovacionController : Controller
    {
        private RepositorioAprovacion repositorio = new RepositorioAprovacion();
        private RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
       
        //
        // GET: /Aprovacion/

        public ActionResult Index(int pagina = 1)
        {

            return View(repositorio.porAprovar().ToPagedList(pagina, 6));
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

        // Rechazar publicaciones
        public ActionResult Rechazar(int id = 0, int estado = 0, string motivos = "")
        {

            int usuario = repositorioUsuario.getIdUsuario(User.Identity.Name);

            if (id != 0 && usuario != 0 && estado != 0)
            {
                repositorio.rechazar(id, estado, usuario, motivos);
            }

            return RedirectToAction("Index", "Aprovacion");
        }

        // Rechazadas
        public ActionResult Rechazadas(int pagina = 1)
        {

            return View(repositorio.rechazadas().ToPagedList(pagina, 6));
        }

        // Aprobadas
        public ActionResult Aprobadas(int pagina = 1)
        {

            return View(repositorio.aprobadas().ToPagedList(pagina, 6));
        }

        // Historial
        public ActionResult Historial(int pagina = 1)
        {
            return View(repositorio.historial().ToPagedList(pagina, 6));
        }

        public ActionResult MotivoDeRechazo(int id = 0)
        {
            return View(repositorio.motivo(id));
        }
    }
}
