using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core.Services.Security
{
    public interface IPermissionService<T>  where T : Entity
    {
        Task<Permission> GetPermissionAsync(int id);
    }
}