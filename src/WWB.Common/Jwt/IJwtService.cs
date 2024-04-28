using System.Security.Claims;

namespace WWB.Common.Jwt
{
    /// <summary>
    /// JWT服务类
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// 客户端标识
        /// </summary>
        string Client { get; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        bool IsSupperAdmin { get; }

        /// <summary>
        /// 账号标识
        /// </summary>
        string AccountId { get; }

        /// <summary>
        /// 获取cliaim
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        string FindClaim(string claimType);

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="accountId">账号标识</param>
        /// <param name="client">jwt配置</param>
        /// <param name="identity">身份标识</param>
        /// <returns></returns>
        TokenResult CreateToken(string accountId, ClientConfig client, ClaimsIdentity identity = null);

        /// <summary>
        /// 验证refresh token
        /// </summary>
        /// <param name="refreshToken">RefreshToken</param>
        /// <param name="client">jwt配置</param>
        /// <param name="claimsPrincipal">身份标识</param>
        /// <returns></returns>
        bool ValidateRefreshToken(string refreshToken, ClientConfig client, out ClaimsPrincipal claimsPrincipal);
    }
}