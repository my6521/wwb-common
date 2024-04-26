using Microsoft.AspNetCore.Mvc;

namespace WWB.AspNetCore.Security
{
    public class PermissionAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionAuthorizeAttribute(string permission) : base(typeof(RequirementPermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}