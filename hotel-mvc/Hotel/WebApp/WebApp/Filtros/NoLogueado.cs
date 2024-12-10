using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Filtros
{
    public class NoLogueado : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("email") != null)
            {
                context.Result = new RedirectResult($"/usuario/redireccionador?error={"Logueado"}");
                return;
            }
        }
    }
}
