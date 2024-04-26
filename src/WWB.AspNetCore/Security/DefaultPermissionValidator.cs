namespace WWB.AspNetCore.Security
{
    public class DefaultPermissionValidator : IPermissionValidator
    {
        public PermissionValidResult Valid(string permission)
        {
            return new PermissionValidResult
            {
                IsSuccess = true,
                Error = "ok"
            };
        }
    }
}