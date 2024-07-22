using CqrsDemo.Core.Commands;
using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core.EventHandlers
{
    public class OrderCancelledEventHandler : INotificationHandler<OrderCancelledEvent>
    {
        private readonly IRequestHandler<SendOrderCancelledNotification, Unit> sendOrderCancelled;

        public OrderCancelledEventHandler(IRequestHandler<SendOrderCancelledNotification, Unit> sendOrderCancelled)
        {
            this.sendOrderCancelled = sendOrderCancelled;
        }


        public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
        {
            await sendOrderCancelled.Handle(new SendOrderCancelledNotification()
            {
                OrderId = notification.OrderId,
            }, cancellationToken);
        }
    }
}