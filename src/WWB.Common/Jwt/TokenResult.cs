namespace WWB.Common.Jwt
{
    /// <summary>
    /// token实体类
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int Expire { get; set; }
    }
}