namespace CqrsDemo.Core.Services.Security
{
    public class SecurityContext
    {
        public TypePermission FromType<T>()
        {
            return new TypePermission(typeof(T), PermissionValues.All);
        }
    }
}