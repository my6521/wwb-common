namespace WWB.Common.Jwt
{
    public class ClientConfig
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 发行方
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 有效时间，单位分钟
        /// </summary>
        public int AccessTokenLifetime { get; set; }
    }
}