using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Models
{
    public class ServiceToken :IServiceToken

    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceToken(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetToken()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");
            return token;
        }
    }
}
