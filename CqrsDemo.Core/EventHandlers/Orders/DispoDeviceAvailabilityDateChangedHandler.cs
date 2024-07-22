using CqrsDemo.Core.Commands;
using CqrsDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Core.EventHandlers.Orders
{
    public class DispoDeviceAvailabilityDateChangedHandler : INotificationHandler<DispoDeviceAvailabilityDateChangedEvent>
    {
        private readonly SalesContext context;
        private readonly IRequestHandler<ChangeDeliveryDate, Unit> changeOrderDeliveryDate;

        public DispoDeviceAvailabilityDateChangedHandler(SalesContext context, IRequestHandler<ChangeDeliveryDate, Unit> changeOrderDeliveryDate)
        {
            this.context = context;
            this.changeOrderDeliveryDate = changeOrderDeliveryDate;
        }

        public async Task Handle(DispoDeviceAvailabilityDateChangedEvent notification, CancellationToken cancellationToken)
        {
            var affectedOrder = await context.Orders.SingleOrDefaultAsync(x => x.DispoDeviceId == notification.DispoDeviceId);
            if (affectedOrder != null)
            {
                await changeOrderDeliveryDate.Handle(new ChangeDeliveryDate()
                {
                    OrderId = affectedOrder.Id,
                    NewDeliveryDate = notification.NewDate.AddDays(10),
                }, default);
            }
        }
    }
}