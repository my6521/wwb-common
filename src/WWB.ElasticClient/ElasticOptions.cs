namespace WWB.ElasticClient
{
    /// <summary>
    /// Elastc配置
    /// </summary>
    public class ElasticOptions
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 索引前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}