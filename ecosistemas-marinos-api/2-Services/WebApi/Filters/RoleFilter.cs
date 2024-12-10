using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class RoleFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _rolRequerido;

        public RoleFilter(string rolRequerido)
        {
            _rolRequerido = rolRequerido;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var usuario = context.HttpContext.User;
            if (!usuario.Identity.IsAuthenticated || !usuario.IsInRole(_rolRequerido))
            {
                context.Result = new JsonResult(new { message = "No tiene acceso a esta funcionalidad" })
                {
                    StatusCode = 403
                };
            }
        }
    }
}
