using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core.Services.Security
{
    abstract class PermissionService<T> : IPermissionService<T>
         where T : Entity
    {
        protected PermissionService(SecurityContext securityContext)
        {
            SecurityContext = securityContext;
        }

        public abstract Task<Permission> GetPermissionAsync(int id);

        protected SecurityContext SecurityContext { get; }
    }
}