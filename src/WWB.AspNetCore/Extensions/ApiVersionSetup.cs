using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace WWB.AspNetCore.Extensions
{
    public static class ApiVersionSetup
    {
        public static IServiceCollection AddApiVersionSetup(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(), new HeaderApiVersionReader("api-version"));
            })
            .AddApiExplorer(config =>
            {
                config.SubstituteApiVersionInUrl = true;
                config.GroupNameFormat = "'v'VVV";
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }
    }
}