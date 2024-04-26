using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WWB.Common.DI;
using WWB.Common.Extensions;

namespace WWB.Common.Jwt
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly ClaimsPrincipal _user;

        public JwtService(IHttpContextAccessor context)
        {
            _user = context.HttpContext?.User;
        }

        public string Client => _user.GetClient();

        public bool IsSupperAdmin => _user.IsSupperAdmin();

        public string AccountId => _user.GetUserId();

        public TokenResult CreateToken(string accountId, ClientConfig client, ClaimsIdentity identity = null)
        {
            if (identity == null) identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimsConst.UserId, accountId));
            identity.AddClaim(new Claim(ClaimsConst.Client, client.ClientId));

            var result = new TokenResult()
            {
                AccessToken = CreateToken(accountId, client, identity, false),
                RefreshToken = CreateToken(accountId, client, identity, true),
                Expire = client.AccessTokenLifetime
            };

            return result;
        }

        public string FindClaim(string claimType)
        {
            return _user.FindClaim(claimType)?.Value;
        }

        public bool ValidateRefreshToken(string refreshToken, ClientConfig client, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = client.Issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.ClientSecret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out securityToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string CreateToken(string accountId, ClientConfig client, ClaimsIdentity identity, bool isRefreh = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var lifetime = isRefreh ? 60 * 24 * 200 : client.AccessTokenLifetime;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = client.Issuer,
                Audience = accountId,
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.ClientSecret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}