using AspectCore.DynamicProxy;

namespace WWB.Common.Attributes
{
    /// <summary>
    /// 分布式锁
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RedisLockAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 锁名称
        /// </summary>
        public string Name { get; set; } = "DefaultLock";

        /// <summary>
        /// 超时时间
        /// </summary>
        public int TimeoutSeconds { get; set; } = 10; //默认10秒

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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