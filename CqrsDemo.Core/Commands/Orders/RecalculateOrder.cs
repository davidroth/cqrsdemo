using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core.Commands
{
    public class RecalculateOrder : IRequest
    {
        public int OrderId { get; set; }

        [OutOfBand]
        public class Handler(SalesContext context, IOrderCalculator orderCalculator) : IRequestHandler<RecalculateOrder>
        {
            public async Task<Unit> Handle(RecalculateOrder request, CancellationToken cancellationToken)
            {
                var order = await context.Set<Order>().FindRequiredAsync(request.OrderId, cancellationToken);
                await order.Recalculate(orderCalculator);
                await context.SaveChangesAsync(cancellationToken);
                return default;
            }
        }
    }
}