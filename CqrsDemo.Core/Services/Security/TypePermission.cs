namespace CqrsDemo.Core.Services.Security
{
    public class TypePermission : Permission
    {
        public TypePermission(Type type, PermissionValues values)
            : base(values)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}