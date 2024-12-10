using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Filtros
{
    public class SoloHuesped : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("rol") != "Huesped")
            {
                context.Result = new RedirectResult($"/usuario/redireccionador?error={"NoEsHuesped"}");
                return;
            }
        }
    }

}
