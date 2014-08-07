using Blog.Controllers.Repositorios;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Index() {
            return RedirectToAction("Iniciar");
        }

        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Logeo model)
        {

            if(ModelState.IsValid){

                var validar = repositorioUsuario.validarUsuario(model.usuario, md5(model.clave));
                if (validar == "")
                {
                    ViewBag.usuario = model.usuario;
                    FormsAuthentication.SetAuthCookie(model.usuario, true);
                    //return Redirect(Request.ServerVariables["http_referer"]);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", validar);
                return View(model);
            }
            
            return View(model);
        }

        public ActionResult Registrar()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Registrar(Registro datosUsuario, HttpPostedFileBase foto = null)
        {
            if (ModelState.IsValid)
            {

                //Agregar información de usuario y guardar
                db.Registros.Add(new Registro
                {
                    nombre = datosUsuario.nombre,
                    apellido = datosUsuario.apellido,
                    usuario = datosUsuario.usuario,
                    clave = md5(datosUsuario.clave),
                    nacimiento = datosUsuario.nacimiento,
                    hora_de_registro = DateTime.Now,
                    direccion_ip = Request.ServerVariables["REMOTE_ADDR"],
                    email = datosUsuario.email,
                    direccion = datosUsuario.direccion,
                    ocupacion = datosUsuario.ocupacion
                });
                db.SaveChanges();


                //Guardar foto de usuario y guardar todos los datos.
                if (foto != null)
                {
                    var data = new byte[foto.ContentLength];
                    foto.InputStream.Read(data, 0, foto.ContentLength);
                    var path = ControllerContext.HttpContext.Server.MapPath("/Images/Usuarios/");
                    var filename = Path.Combine(path, datosUsuario.usuario + ".jpg");
                    System.IO.File.WriteAllBytes(Path.Combine(path, filename), data);
                    ViewBag.ImageUploaded = filename;
                    var idUsuario = repositorioUsuario.getIdUsuario(datosUsuario.usuario);
                    db.Userios.Find(idUsuario).foto = path + filename;
                }
                else {
                    if (datosUsuario.direccion.ToLower() == "femenino") { 
                        var path = ControllerContext.HttpContext.Server.MapPath("/Images/Usuarios/");
                        var filename = Path.Combine(path, "femeninoSysF.jpg");
                        var idUsuario = repositorioUsuario.getIdUsuario(datosUsuario.usuario);
                        db.Userios.Find(idUsuario).foto = path + filename;
                    }
                    else
                    {
                        var path = ControllerContext.HttpContext.Server.MapPath("/Images/Usuarios/");
                        var filename = Path.Combine(path, "masculinoSysF.jpg");
                        var idUsuario = repositorioUsuario.getIdUsuario(datosUsuario.usuario);
                        db.Userios.Find(idUsuario).foto = path + filename;
                    }
                }
                db.SaveChanges();

                // Validar login
                FormsAuthentication.SetAuthCookie(datosUsuario.usuario, true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //public ActionResult CompletarRegistro(int id = 0)
        //{
        //    return View(db.Registros.Find(id));
        //}
        
        //[HttpPost]
        //public ActionResult CompletarRegistro(Registro datosUsuario, string foto ,int id = 0)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var registro = db.Registros.Find(id);
        //        registro.ocupacion = datosUsuario.ocupacion;
        //        //db.SaveChanges();
                
        //        var use = db.Userios.Find(registro.registro_id);
        //        return Content(use.foto, use.usuario1);
        //        //db.Registros.Add(new Registro
        //        //{
        //        //    nombre = datosUsuario.nombre,
        //        //    apellido = datosUsuario.apellido,
        //        //    usuario = datosUsuario.usuario,
        //        //    clave = md5(datosUsuario.clave),
        //        //    nacimiento = datosUsuario.nacimiento,
        //        //    hora_de_registro = DateTime.Now,
        //        //    direccion_ip = Request.ServerVariables["REMOTE_ADDR"],
        //        //    email = datosUsuario.email,
        //        //});
        //        db.SaveChanges();
        //        if (repositorioUsuario.validarUsuario(datosUsuario.usuario, md5(datosUsuario.clave)))
        //        {
        //            ViewBag.usuario = datosUsuario.usuario;
        //            FormsAuthentication.SetAuthCookie(datosUsuario.usuario, true);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            return Content("No pudimos confirmar el inicio de seccion, intentalo mas tarde.");
        //        }
        //    }
        //    return View();

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
