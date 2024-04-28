using System.Security.Claims;

namespace WWB.Common.Jwt
{
    /// <summary>
    /// 常量
    /// </summary>
    public class ClaimsConst
    {
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string UserName = ClaimTypes.Name;
        public const string IsSupperAdmin = "IsSupperAdmin";
        public const string Client = "client";
    }
}