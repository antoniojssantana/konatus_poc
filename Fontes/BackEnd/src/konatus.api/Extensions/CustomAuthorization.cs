using Microsoft.AspNetCore.Http;
using System.Linq;

namespace konatus.api.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidClaimsUser(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }
}