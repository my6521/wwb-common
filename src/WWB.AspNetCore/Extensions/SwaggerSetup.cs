using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WWB.AspNetCore.Extensions
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                foreach (FieldInfo fileld in typeof(ApiVersionInfo).GetFields())
                {
                    options.SwaggerDoc(fileld.Name, new OpenApiInfo
                    {
                        Version = fileld.Name,
                        Title = "api文档",
                        Description = $"api文档,{fileld.Name}版本"
                    });
                }

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
                {
                    c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
                }
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            return app;
        }
    }
}