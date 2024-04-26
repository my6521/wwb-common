using System.Security.Claims;

namespace WWB.Common.Jwt
{
    public interface IJwtService
    {
        string Client { get; }
        bool IsSupperAdmin { get; }
        string AccountId { get; }

        string FindClaim(string claimType);

        TokenResult CreateToken(string accountId, ClientConfig client, ClaimsIdentity identity = null);

        bool ValidateRefreshToken(string refreshToken, ClientConfig client, out ClaimsPrincipal claimsPrincipal);
    }
}