using Blog.Controllers.Repositorios;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WikiWiki.Models;

namespace Blog.Controllers
{
    [AllowAnonymous]
    public class LogeoController : Controller
    {
        //
        // GET: /Logeo/

        private UsersContext db = new UsersContext();
        RepositorioUsuario repositorioUsuario = new RepositorioUsuario();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Logeo model)
        {

            if(ModelState.IsValid){

                if (repositorioUsuario.validarUsuario(model.usuario, md5(model.clave)))
                {
                    ViewBag.usuario = model.usuario;
                    FormsAuthentication.SetAuthCookie(model.usuario, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","Usuario o contrasena incorrecta");
                    return View(model);
                }
            }
            
            return View(model);
        }

        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public static string md5(string itemToHash)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
        }
    }

}
