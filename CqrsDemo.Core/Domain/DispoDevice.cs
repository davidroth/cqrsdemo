using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Domain
{
    public class DispoDevice : Entity, IAggregateRoot
    {
        public DispoDevice(string serialNumber)
        {
            SerialNumber = serialNumber;
        }

        [Required]
        public required string Name { get; set; }

        [Required]
        public string SerialNumber { get; private set; }

        public DateTime AvailabilityDate { get; private set; }

        public void UpdateAvailabilityDate(DateTime timestamp)
        {
            Events.Add(new DispoDeviceAvailabilityDateChangedEvent(Id, AvailabilityDate, timestamp));
            AvailabilityDate = timestamp;
        }
    }
}