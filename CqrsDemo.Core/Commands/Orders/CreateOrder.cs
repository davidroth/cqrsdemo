using CqrsDemo.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Commands
{
    public class CreateOrder : ICommand
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required int ProductId { get; set; }

        [Required]
        public required int CustomerId { get; set; }

        public class Handler(SalesContext context, IOrderNumberGenerator orderNumberGenerator) : IRequestHandler<CreateOrder>
        {
            public async Task<Unit> Handle(CreateOrder request, CancellationToken cancellationToken)
            {
                var orderNumber = await orderNumberGenerator.GetNextOrderNumberAsync();
                var order = new Order(request.Name, orderNumber, request.ProductId, request.CustomerId);

                context.Add(order);
                await context.SaveChangesAsync();
                return default;
            }
        }
    }
}