using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;
using WWB.Common.DI;

namespace WWB.AspNetCore.Extensions
{
    public static class ServicesAutoSetup
    {
        public static IServiceCollection AddServicesFromAllAssembly(this IServiceCollection services)
        {
            string[] filters =
            {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };

            return services.Scan(scan => scan.FromApplicationDependencies(assembly => !filters.Any(x => assembly.FullName.StartsWith(x))).AddClass());
        }

        public static IServiceCollection AddServicesFromTypes(this IServiceCollection services, params Type[] types)
        {
            return services.Scan(scan => scan.FromAssembliesOf(types).AddClass());
        }

        public static IServiceCollection AddServicesFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.Scan(scan => scan.FromAssemblies(assemblies).AddClass());
        }

        public static IServiceCollection AddServicesFromSelector(this IServiceCollection services, Action<ITypeSourceSelector> action)
        {
            return services.Scan(action);
        }

        private static void AddClass(this IImplementationTypeSelector select)
        {
            select
                .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
                      .AsImplementedInterfaces()
                      .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime()
                .AddClasses(classes => classes.AssignableTo<IScopedDependencyOnlySelf>())
                    .AsSelf()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ITransientDependencyOnlySelf>())
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonDependencyOnlySelf>())
                    .AsSelf()
                    .WithSingletonLifetime();
        }
    }
}