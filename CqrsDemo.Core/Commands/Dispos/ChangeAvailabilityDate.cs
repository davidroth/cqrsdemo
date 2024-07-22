using System.ComponentModel.DataAnnotations;
using CqrsDemo.Infrastructure;

namespace CqrsDemo.Core.Commands.Dispos
{
    public class ChangeAvailabilityDate : ICommand, IValidatableObject
    {
        public int DispoDeviceId { get; set; }

        public DateTime NewAvailability { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewAvailability < DateTime.UtcNow)
            {
                yield return new ValidationResult("Availability date must be > than now", [nameof(NewAvailability)]);
            }
        }
    }

    [RetryCommand]
    class ChangeAvailabilityDateHandler(SalesContext context) : IRequestHandler<ChangeAvailabilityDate>
    {
        public async Task<Unit> Handle(ChangeAvailabilityDate request, CancellationToken cancellationToken)
        {
            var dispoDevice = await context.DispoDevices.FindRequiredAsync(request.DispoDeviceId);

            dispoDevice.UpdateAvailabilityDate(request.NewAvailability);
            context.SaveChanges();
            return default;
        }
    }
}