using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSql;
using SmartSql.DyRepository;
using System.Reflection;

namespace WWB.AspNetCore.Extensions
{
    public static class SmartSqlSetup
    {
        public static IServiceCollection AddSmartSqlSetup(this IServiceCollection services, IConfiguration configuration, IList<Assembly> assemblies)
        {
            var builder = services.AddSmartSql((sp, b) => { b.UseProperties(configuration); });

            //注册仓库工厂
            builder.AddRepositoryFactory();

            var baseType = typeof(IRepository);
            //查找所有仓库类型
            var repositoryTypes = assemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(type => type.IsInterface && baseType.IsAssignableFrom(type) && type.Namespace != "SmartSql.DyRepository")
                .ToList();

            //注册
            foreach (Type repositoryType in repositoryTypes)
            {
                builder.Services.AddSingleton(repositoryType, sp =>
                {
                    ISqlMapper sqlMapper = sp.GetRequiredService<ISqlMapper>();
                    var factory = sp.GetRequiredService<IRepositoryFactory>();
                    var scope = string.Empty;
                    return factory.CreateInstance(repositoryType, sqlMapper, scope);
                });
            }

            return services;
        }
    }
}