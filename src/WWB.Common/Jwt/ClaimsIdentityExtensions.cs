using System.Security.Claims;
using WWB.Common.Extensions;

namespace WWB.Common.Jwt
{
    /// <summary>
    /// claims扩展类
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// 获取账号标识
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindClaim(ClaimsConst.UserId);
            return claim?.Value;
        }

        /// <summary>
        /// 是否超级会员
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static bool IsSupperAdmin(this ClaimsPrincipal principal)
        {
            var claim = principal.FindClaim(ClaimsConst.IsSupperAdmin);
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return false;
            }

            return claim.Value.ToLower().Equals("1") || claim.Value.ToLower().Equals("true");
        }

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetClient(this ClaimsPrincipal principal)
        {
            return principal.FindClaim(ClaimsConst.Client)?.Value;
        }
    }
}