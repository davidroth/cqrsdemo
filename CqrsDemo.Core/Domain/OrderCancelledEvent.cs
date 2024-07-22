namespace CqrsDemo.Core.Domain
{
    public class OrderCancelledEvent : IDomainEvent, INotification
    {
        public OrderCancelledEvent(int orderId, OrderStatus oldStatus)
        {
            OrderId = orderId;
            OldStatus = oldStatus;
        }

        public int OrderId { get; private set; }
        public OrderStatus OldStatus { get; private set; }
    }
}