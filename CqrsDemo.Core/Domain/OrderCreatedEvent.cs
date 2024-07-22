namespace CqrsDemo.Core.Domain
{
    public class OrderCreatedEvent : INotification
    {
        public OrderCreatedEvent(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; private set; }
    }
}