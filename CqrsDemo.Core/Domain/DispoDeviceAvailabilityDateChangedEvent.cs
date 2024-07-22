namespace CqrsDemo.Core.Domain
{
    public class DispoDeviceAvailabilityDateChangedEvent : IDomainEvent, INotification
    {
        public DispoDeviceAvailabilityDateChangedEvent(int id, DateTime oldDate, DateTime newDate)
        {
            DispoDeviceId = id;
            OldDate = oldDate;
            NewDate = newDate;
        }

        public int DispoDeviceId { get; }

        public DateTime OldDate { get; }

        public DateTime NewDate { get; }
    }
}