using System.Security.Claims;
using WebApi.Utils.Interfaces;

namespace WebApi.Utils
{
    public class UserNameService :IUserNameService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserNameService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetActualUsername()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

            if(claimsIdentity != null)
            {
                var uniqueNameClaim = claimsIdentity.Name.ToString();

                return uniqueNameClaim;
            }

            return null;
        }
    }
}
