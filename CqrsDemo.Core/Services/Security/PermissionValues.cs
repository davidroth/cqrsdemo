namespace CqrsDemo.Core.Services.Security
{

    [Flags]
    public enum PermissionValues
    {
        Restricted = 0,
        Read = 1,
        Update = 2,
        Create = 4,
        Delete = 8,
        All = Read | Update | Create | Delete
    }
}