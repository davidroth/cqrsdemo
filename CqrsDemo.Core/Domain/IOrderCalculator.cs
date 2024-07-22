namespace CqrsDemo.Core.Domain
{
    public interface IOrderCalculator
    {
        Task<OrderCalculationResult> Recalculate(Order order);
    }

    public class OrderCalculationResult
    {
        public decimal TotalAmount { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}