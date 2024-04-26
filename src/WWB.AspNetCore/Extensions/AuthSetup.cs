using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WWB.Common.Jwt;

namespace WWB.AspNetCore.Extensions
{
    public static class AuthSetup
    {
        public static IServiceCollection AddAuthenticationSetup(this IServiceCollection services, ClientConfig client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = client.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.ClientSecret)),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    x.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = (context) =>
                        {
                            var clientId = context.Principal?.GetClient();
                            if (clientId != client.ClientId)
                            {
                                context.Fail("强制退出");

                                return Task.CompletedTask;
                            }
                            var userId = context.Principal?.GetUserId();

                            return Task.CompletedTask;
                        },
                    };
                });

            return services;
        }
    }
}