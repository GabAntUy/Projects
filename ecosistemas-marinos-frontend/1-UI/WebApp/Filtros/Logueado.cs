using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filtros
{
    public class LogueadoAttribute : Attribute, IAuthorizationFilter
    {
        private List<string> _roles;

        public LogueadoAttribute(params string[] roles)
        {
            _roles = roles.ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string rol = context.HttpContext.Session.GetString("rol");
            //if (!_roles.Contains(rol) && rol != null)
            //{
            //    context.Result = new RedirectResult("/usuario/Index");
            //    return;
            //}
            if (!_roles.Contains(rol))
            {
                context.Result = new RedirectResult("/usuario/login");
                return;
            }
        }
    }

}
