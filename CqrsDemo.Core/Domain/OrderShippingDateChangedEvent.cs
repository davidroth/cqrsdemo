namespace CqrsDemo.Core.Domain
{
    public class OrderShippingDateChangedEvent : INotification
    {
        public OrderShippingDateChangedEvent(int id, DateTime? from, DateTime to)
        {
            Id = id;
            From = from;
            To = to;
        }

        public int Id { get; private set; }
        public DateTime? From { get; private set; }
        public DateTime To { get; private set; }
    }
}