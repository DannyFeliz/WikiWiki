
using Blog.Controllers.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WikiWiki.Controllers
{
    public class NotificacionEmail
    {
        private MailMessage email;
        private SmtpClient smtp;
        private RepositorioUsuario repositorio;
        private string salida = null;
        private string asunto = ""; // Asunto del mensaje
        private string cuerpo = ""; // Cuerpo del mensaje
        private string usuarioEmail = ""; // Email a enviar
        
        public NotificacionEmail(string asunto, string cuerpo, string usuarioEmail)
        {
            email = new MailMessage();
            smtp = new SmtpClient();
            repositorio = new RepositorioUsuario();

            this.asunto = asunto;
            this.cuerpo = cuerpo;
            this.usuarioEmail = usuarioEmail;

        }

        private void mensaje()
        {
            email.To.Add(new MailAddress(usuarioEmail));
            email.From = new MailAddress("wikiwikinformacion@gmail.com");
            email.Subject = asunto;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = cuerpo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
        }

        private void cliente()
        {
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("wikiwikinformacion@gmail.com", "infowikiwiki");
 
        }

        public string enviarEmail()
        {
            try{
                mensaje();
                cliente();
                smtp.Send(email);
                email.Dispose();
                //salida = "Corre electrónico fue enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                salida = "Error enviando correo electrónico: " + ex.Message;
            }

            return salida;
        }
    }
}