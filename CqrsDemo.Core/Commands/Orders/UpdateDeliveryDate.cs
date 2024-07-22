using CqrsDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Commands
{
    public class ChangeDeliveryDate : IRequest, IValidatableObject
    {
        public int OrderId { get; set; }

        public DateTime NewDeliveryDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewDeliveryDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Delivery date must be > than now", new[] { nameof(NewDeliveryDate) });
            }
        }

        [RetryCommand]
        public class Handler : IRequestHandler<ChangeDeliveryDate>
        {
            private readonly SalesContext context;
            public Handler(SalesContext context) => this.context = context;

            public async Task<Unit> Handle(ChangeDeliveryDate request, CancellationToken cancellationToken)
            {
                var order = await context.Orders
                            .Where(x => x.Id == request.OrderId)
                            .Include(x => x.PaymentDates)
                            .SingleAsync();

                order.UpdateDeliveryDate(request.NewDeliveryDate);
                await context.SaveChangesAsync();
                return default;
            }
        }
    }
}