using Microsoft.AspNetCore.Mvc;

namespace konatus.api.Extensions
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
        {
            Arguments = new object[] { new ClaimsAuthorizeAttribute(claimName, claimValue) };
        }
    }
}