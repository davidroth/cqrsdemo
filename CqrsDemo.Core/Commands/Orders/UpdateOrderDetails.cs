using CqrsDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Commands
{
    public class UpdateOrderDetails: ICommand
    {
        public required int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public required List<Position> Positions { get; set; } = [];

        public class Position
        {
            public required int Id { get; set; }

            [Required]
            public required string Name { get; set; }

            [Range(1, 100)]
            public required int Quantity { get; set; }
            public required bool Delete { get; set; }
        }

        public List<int> PositionsToDelete = [];

        public class Handler : IRequestHandler<UpdateOrderDetails>
        {
            private readonly SalesContext context;
            private readonly IOrderCalculator orderCalculator;

            public Handler(SalesContext context, IOrderCalculator orderCalculator)
            {
                this.context = context;
                this.orderCalculator = orderCalculator;
            }

            public async Task<Unit> Handle(UpdateOrderDetails request, CancellationToken cancellationToken)
            {
                var order = await context.Set<Order>()
                    .Include(x => x.Positions)
                    .SingleRequiredAsync(request.Id, cancellationToken);

                order.Name = request.Name;
                UpdatePositions(order, request);

                await order.Recalculate(orderCalculator);
                await context.SaveChangesAsync(cancellationToken);
                return default;
            }

            private void UpdatePositions(Order order, UpdateOrderDetails command)
            {
                var joined = (from commandPos in command.Positions
                              join orderPos in order.Positions on commandPos.Id equals orderPos.Id into j
                              from orderPos in j.DefaultIfEmpty()
                              select (orderPos ?? new OrderPosition(string.Empty), commandPos));

                foreach (var (domainPosition, commandPosition) in joined)
                {
                    domainPosition.Name = commandPosition.Name;
                    domainPosition.Quantity = commandPosition.Quantity;

                    if (!domainPosition.HasId())
                    {
                        order.AddPosition(domainPosition);
                    }
                    if (commandPosition.Delete)
                    {
                        order.RemovePosition(domainPosition);
                    }
                }
            }
        }
    }
}