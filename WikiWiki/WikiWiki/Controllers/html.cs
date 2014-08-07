using Blog.Controllers.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WikiWiki.Controllers.Repositorios;

namespace WikiWiki.Controllers
{
    
    public class Html
    {
        RepositorioUsuario repositorioUsuario = new RepositorioUsuario();
        RepositorioPublicacion repositorioPublicacion = new RepositorioPublicacion();
        NotificacionEmail notificacionEmail = null;
        public void registro(string name, string lastName, string user, string email) {
           
            var pagina = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><title>Registro</title><style>section{margin: 5px;}section h3,p{text-align: center;}</style></head><body><section><h3>Estimado/a " + name + " " + lastName + "</h3><p>Gracias por registrarte en <strong>WikiWiki</strong> y formar parte de nuestra gran familia, tu nombre de usuario es <strong>" + user + "</strong>. <br><br>Te esperamos pronto en nuestro sitio.<br> Saludos.</p></section></body></html>";
            notificacionEmail = new NotificacionEmail("Registro en WikiWiki", pagina, email);

            notificacionEmail.enviarEmail();

        }
        
        public void publicacion(string user, int idPublicacion)
        {

            var titulo = repositorioPublicacion.getPublicacion(idPublicacion).titulo;
            var email  = repositorioUsuario.getEmailUsuario(user);

            var pagina = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><title>Registro</title><style>section{margin: 5px;}section h3,p{text-align: center;}</style></head><body><section><h3>Estimado/a " + user + "</h3><p>Tu post " + titulo + " ya fue aprobado haz click <a href=\"   http://localhost:53988/Home/Informacion/  \" "+idPublicacion + ">Aquí<a/> <strong>WikiWiki</strong> y formar parte de nuestra gran familia, tu nombre de usuario es <strong>" + user + "</strong>. <br><br>Te esperamos pronto en nuestro sitio.<br> Saludos.</p></section></body></html>";
            notificacionEmail = new NotificacionEmail("Aprobacion de post", pagina, email);
            notificacionEmail.enviarEmail();
        }

        public void contacto(string asunto, string email, string contenido) {

            var pagina = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><title>Registro</title><style>section{margin: 5px;}section h3,p{text-align: center;}</style></head><body><section><h3>Nuevo mensaje de:  " + email + "</h3><p>" + contenido + "</p></section></body></html>";
            notificacionEmail = new NotificacionEmail("Contacto: "+asunto, pagina, email);

            notificacionEmail.enviarEmail();
        }


        public void registro()
        {
            throw new NotImplementedException();
        }
    }
}