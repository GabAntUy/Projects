using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filtros
{
    public class SoloOperador : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("rol") != "Operador")
            {
                context.Result = new RedirectResult($"/usuario/redireccionador?error={$"NoEsOperador"}");
                return;
            }
        }
    }
}
