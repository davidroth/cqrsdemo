namespace CqrsDemo.Core.Services.Security
{
    public abstract class Permission
    {
        public Permission(PermissionValues values)
        {
            Values = values;
        }

        public PermissionValues Values { get; }

        public bool CanRead => Values.HasFlag(PermissionValues.Read);

        public bool CanUpdate => Values.HasFlag(PermissionValues.Update);

        public bool CanCreate => Values.HasFlag(PermissionValues.Create);

        public bool CanDelete => Values.HasFlag(PermissionValues.Delete);
    }
}