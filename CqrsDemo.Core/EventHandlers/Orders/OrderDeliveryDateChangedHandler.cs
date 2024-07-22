using CqrsDemo.Core;
using CqrsDemo.Core.Domain;
using CqrsDemo.Infrastructure.Mailing;

namespace CqrsDemo.EventHandlers
{
    [OutOfBand]
    public class OrderDeliveryDateChangedHandler : INotificationHandler<OrderDeliveryDateChangedEvent>
    {
        private readonly IMailer mailer;
        private readonly SalesContext context;

        public OrderDeliveryDateChangedHandler(SalesContext context, IMailer mailer)
        {
            this.mailer = mailer;
            this.context = context;
        }

        public async Task Handle(OrderDeliveryDateChangedEvent notification, CancellationToken cancellationToken)
        {
            var order = await context.Orders.FindRequiredAsync(notification.Id);
            await mailer.SendMailAsync("david.roth@fusonic.net", $"Shipping date of order ({order.OrderNumber} ({order.Name})) has changed {notification.OldDate} => {notification.NewDate}");
        }
    }
}