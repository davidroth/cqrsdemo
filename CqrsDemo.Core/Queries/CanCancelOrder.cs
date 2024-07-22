using CqrsDemo.Core.Domain;
using CqrsDemo.Core.Services.Security;
using CqrsDemo.Infrastructure;

namespace CqrsDemo.Core.Queries
{
    public class CanCancelOrder(int orderId) : IQuery<CanExecuteResult>, IProvideCachingKey
    {
        public int OrderId { get; set; } = orderId;

        string IProvideCachingKey.GetCacheKey() => OrderId.ToString();

        public class CanCancelOrderQueryHandler : IRequestHandler<CanCancelOrder, CanExecuteResult>
        {
            private readonly SalesContext context;
            private readonly IPermissionService<Order> orderPermissionService;

            public CanCancelOrderQueryHandler(SalesContext context, IPermissionService<Order> orderPermissionService)
            {
                this.context = context;
                this.orderPermissionService = orderPermissionService;
            }

            public async Task<CanExecuteResult> Handle(CanCancelOrder request, CancellationToken cancellationToken)
            {
                var permission = await orderPermissionService.GetPermissionAsync(request.OrderId);
                var canCancel = permission.CanUpdate;
                if (canCancel)
                {
                    var order = await context.Set<Order>().FindRequiredAsync(request.OrderId);
                    canCancel = order.Status == OrderStatus.Active;
                }
                return new CanExecuteResult() { CanExecute = canCancel };
            }
        }
    }
}