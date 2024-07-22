namespace CqrsDemo.Core.Domain
{
    public class OrderRecalculatedEvent : INotification
    {
        public OrderRecalculatedEvent(int orderId, decimal totalAmount, decimal discountPercent)
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
            DiscountPercent = discountPercent;
        }

        public int OrderId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal DiscountPercent { get; private set; }
    }
}