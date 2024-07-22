using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Domain
{
    public class Order : Entity, IAggregateRoot, IPublishCreatedEvent, IValidatableObject
    {
        private readonly List<OrderPosition> positions = [];
        private readonly List<PaymentDate> paymentDates = [];

        private bool recalcRequired;

        public Order(string name, string orderNumber, int productId, int customerId)
        {
            OrderDate = DateTime.UtcNow;
            Name = name;
            OrderNumber = orderNumber;
            ProductId = productId;
            CustomerId = customerId;
        }

        // Note: IPublishCreatedEvent exists because we cannot publish the created event within the constructor (Id is database generated and does not exist when constructing a new order)
        void IPublishCreatedEvent.AddCreatedEvent()
        {
            Events.Add(new OrderCreatedEvent(Id));
        }

        public int CustomerId { get; private set; }

        public int ProductId { get; private set; }

        // Note: When considering strict DDD best practices, this navigation property is an anti pattern because the Product does not belong to the Order aggregate.
        // Only the ProductId property shall be used here => So we can communicate that a Product cannot be modified by the order aggregate (Its just a reference)
        // Why is it here then ? =>  For this demo app joining via Navigation properties is more convenient than joining explicitly.
        public Product? Product { get; private set; }

        public int? DispoDeviceId { get; private set; }
        public DispoDevice? DispoDevice { get; private set; }


        [Required]
        public string OrderNumber { get; private set; }

        [Required]
        public string Name { get; set; }

        public OrderStatus Status { get; private set; }

        public DateTime OrderDate { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public DateTime? ShippingDate { get; private set; }

        public DateTime? LastRecalculationDate { get; private set; }

        public IReadOnlyCollection<OrderPosition> Positions => positions.AsReadOnly(); // Note: Positions can only be added via the AddPosition(...) method on the order aggreagte.
        public IReadOnlyCollection<PaymentDate> PaymentDates => paymentDates.AsReadOnly(); // Note: PaymentDates can only be added via the AddPaymentDate(...) method on the order aggreagte.

        public decimal TotalAmount { get; private set; }
        public decimal DiscountPercent { get; private set; }

        public void AddPosition(OrderPosition position)
        {
            if (positions.Contains(position))
                throw new InvalidOperationException("Cannot add position because it already exists.");

            recalcRequired = true;
            positions.Add(position);
        }

        public void RemovePosition(OrderPosition position)
        {
            if (!positions.Contains(position))
                throw new InvalidOperationException("Cannot remove position because it does not exist.");

            recalcRequired = true;
            positions.Remove(position);
        }

        public async Task Recalculate(IOrderCalculator calculator)
        {
            if (Status == OrderStatus.Closed)
                throw new InvalidOperationException("Recalculating closed or cancelled orders is not allowed.");

            var calculationResult = await calculator.Recalculate(this);

            TotalAmount = calculationResult.TotalAmount;
            DiscountPercent = calculationResult.DiscountPercent;

            Events.Add(new OrderRecalculatedEvent(Id, TotalAmount, DiscountPercent));

            recalcRequired = false;
            LastRecalculationDate = DateTime.UtcNow;
        }

        public void CancelOrder()
        {
            if (Status != OrderStatus.Active)
            {
                throw new InvalidOperationException($"Cannot cancel order because the current status is {Status}");
            }
            Status = OrderStatus.Canceled;
            Events.Add(new OrderCancelledEvent(Id, OrderStatus.Active));
        }

        public void AssignDispoDevice(int dispoDeviceId)
        {
            DispoDeviceId = dispoDeviceId;
        }

        public void UpdateDeliveryDate(DateTime timestamp)
        {
            if (DeliveryDate != timestamp)
            {
                var deliveryEvent = new OrderDeliveryDateChangedEvent(Id, DeliveryDate, timestamp);
                Events.Add(deliveryEvent);
                DeliveryDate = timestamp;

                ShiftOpenPayments(deliveryEvent);
            }
        }

        private void ShiftOpenPayments(OrderDeliveryDateChangedEvent deliveryEvent)
        {
            var shift = TimeSpan.FromDays(20);
            if (deliveryEvent.OldDate != null)
            {
                shift = deliveryEvent.NewDate - deliveryEvent.OldDate.Value;
            }
            foreach (var payment in PaymentDates.Where(x => x.Status == PaymentDateStatus.Open))
            {
                payment.DueDate = payment.DueDate.AddDays(shift.TotalDays);
            }
        }

        public void UpdateShippingDate(DateTime timestamp)
        {
            if (ShippingDate != timestamp)
            {
                Events.Add(new OrderShippingDateChangedEvent(Id, ShippingDate, timestamp));
                ShippingDate = timestamp;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (recalcRequired)
            {
                yield return new ValidationResult("Order has changed and must be recalculated");
            }
        }
    }
}