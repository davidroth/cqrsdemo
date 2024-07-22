namespace CqrsDemo.Core.Domain
{
    public class PaymentDate : Entity
    {
        public PaymentDate()
        { }

        public Order? Order { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public PaymentDateStatus Status { get; set; }
    }
}