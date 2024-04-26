using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WWB.AspNetCore.Extensions
{
    public static class MapsterSetup
    {
        public static IServiceCollection AddMapsterSetup(this IServiceCollection services, params Assembly[] assemblies)
        {
            //自动映射
            // 获取全局映射配置
            var config = TypeAdapterConfig.GlobalSettings;

            // 扫描所有继承  IRegister 接口的对象映射配置
            if (assemblies != null && assemblies.Length > 0) config.Scan(assemblies);

            // 配置默认全局映射（支持覆盖）
            config.Default
                  .NameMatchingStrategy(NameMatchingStrategy.Flexible)
                  .PreserveReference(true);

            // 配置默认全局映射（忽略大小写敏感）
            config.Default
                  .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                  .PreserveReference(true);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}