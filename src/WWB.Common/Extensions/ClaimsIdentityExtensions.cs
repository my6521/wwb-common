using System.Security.Claims;

namespace WWB.Common.Extensions
{
    /// <summary>
    /// claims扩展类
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        public static Claim FindClaim(this ClaimsPrincipal principal, string claimType)
        {
            return principal?.Claims?.FirstOrDefault(c => c.Type == claimType);
        }

        public static Claim[] FindClaims(this ClaimsPrincipal principal, string claimType)
        {
            return principal?.Claims?.Where(c => c.Type == claimType).ToArray() ?? Array.Empty<Claim>();
        }
    }
}