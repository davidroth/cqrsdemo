using CqrsDemo.Core.Services.Security;
using CqrsDemo.Core.Domain;
using CqrsDemo.Infrastructure;

namespace CqrsDemo.Core.Services
{
    class OrderPermissionService : PermissionService<Order>
    {
        private readonly SalesContext context;

        public OrderPermissionService(SecurityContext securityContext, SalesContext context)
            : base(securityContext)
        {
            this.context = context;
        }

        public override async Task<Permission> GetPermissionAsync(int id)
        {
            var permission = SecurityContext.FromType<Order>();
            var order = await context.Orders.FindRequiredAsync(id);

            if (order.Status == OrderStatus.Closed)
            {
                permission = new TypePermission(typeof(Order), permission.Values & ~PermissionValues.Update & ~PermissionValues.Delete);
            }
            return permission;
        }
    }
}