namespace CqrsDemo.Core.Domain
{
    public class OrderDeliveryDateChangedEvent : IDomainEvent, INotification
    {
        public OrderDeliveryDateChangedEvent(int id, DateTime? oldDate, DateTime newDate)
        {
            Id = id;
            OldDate = oldDate;
            NewDate = newDate;
        }

        public int Id { get; private set; }

        public DateTime? OldDate { get; private set; }

        public DateTime NewDate { get; private set; }
    }
}