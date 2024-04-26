using AspectCore.DynamicProxy;

namespace WWB.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RedisLockAttribute : AbstractInterceptorAttribute
    {
        public string Name { get; set; } = "DefaultLock";
        public int TimeoutSeconds { get; set; } = 10; //默认10秒

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var redisLock = RedisHelper.Lock(Name, TimeoutSeconds);
            if (redisLock != null)
            {
                using (redisLock)
                {
                    await next.Invoke(context);
                }
            }
            else
            {
                throw new Exception($"分布式锁获取失败 name={Name}");
            }
        }
    }
}