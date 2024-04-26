using CSRedis;
using Microsoft.Extensions.DependencyInjection;

namespace WWB.AspNetCore.Extensions
{
    public static class RedisSetup
    {
        public static IServiceCollection AddRedisSetup(this IServiceCollection services, string connectionStr)
        {
            if (string.IsNullOrWhiteSpace(connectionStr)) throw new ArgumentNullException(nameof(connectionStr));

            var csRedis = new CSRedisClient(connectionStr);

            RedisHelper.Initialization(csRedis);

            return services;
        }
    }
}