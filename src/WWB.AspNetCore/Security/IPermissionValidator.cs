namespace WWB.AspNetCore.Security
{
    public interface IPermissionValidator
    {
        PermissionValidResult Valid(string permission);
    }
}