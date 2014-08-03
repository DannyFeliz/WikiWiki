using Blog.Controllers.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Blog.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if(!httpContext.Request.IsAuthenticated){
                return false;
            }

            RepositorioUsuario usuario = new RepositorioUsuario();
            var rolDeUsuario = usuario.getRolUsuario(httpContext.User.Identity.Name);

            foreach (string rol in this.Roles.Split(','))
            {

                if (rol.Equals(rolDeUsuario))
                {
                    return true;
                }
            }

            return false;
            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Logeo/");
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}