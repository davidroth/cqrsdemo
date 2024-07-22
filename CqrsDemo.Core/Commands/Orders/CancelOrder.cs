using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Commands
{
    public class CancelOrder : ICommand
    {
        public int OrderId { get; set; }

        [Required]
        public required string Reason { get; set; }

        public class Handler(SalesContext context) : IRequestHandler<CancelOrder>
        {
            public async Task<Unit> Handle(CancelOrder request, CancellationToken cancellationToken)
            {
                var order = await context.Orders.FindRequiredAsync(request.OrderId, cancellationToken);
                order.CancelOrder();
                await context.SaveChangesAsync(cancellationToken);
                return default;
            }
        }
    }
}