using CqrsDemo.Core.Domain;
using CqrsDemo.Infrastructure;
using CqrsDemo.Infrastructure.Mailing;

namespace CqrsDemo.Core.Commands
{
    public class SendOrderCancelledNotification : IRequest
    {
        public int OrderId { get; set; }

        [OutOfBand]
        public class Handler(SalesContext context, IMailer mailer) : IRequestHandler<SendOrderCancelledNotification>
        {
            public async Task<Unit> Handle(SendOrderCancelledNotification request, CancellationToken cancellationToken)
            {
                var order = await context.Orders.FindRequiredAsync(request.OrderId);
                var receiver = "email@cqrsdemo"; // Call logic to retrieve receivers
                var message = $"Order {request.OrderId} has been cancelled"; // format template

                await mailer.SendMailAsync(receiver, message);
                return default;
            }
        }
    }
}