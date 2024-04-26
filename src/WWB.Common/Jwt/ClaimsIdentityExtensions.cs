using System.Security.Claims;
using WWB.Common.Extensions;

namespace WWB.Common.Jwt
{
    public static class ClaimsIdentityExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindClaim(ClaimsConst.UserId);
            return claim?.Value;
        }

        public static bool IsSupperAdmin(this ClaimsPrincipal principal)
        {
            var claim = principal.FindClaim(ClaimsConst.IsSupperAdmin);
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return false;
            }

            return claim.Value.ToLower().Equals("1") || claim.Value.ToLower().Equals("true");
        }

        public static string GetClient(this ClaimsPrincipal principal)
        {
            return principal.FindClaim(ClaimsConst.Client)?.Value;
        }
    }
}