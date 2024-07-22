using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core.Services
{
    public class OrderCalculator : IOrderCalculator
    {
        private readonly SalesContext context;

        public OrderCalculator(SalesContext context) => this.context = context;

        public async Task<OrderCalculationResult> Recalculate(Order order)
        {
            // load other calculation relevant information (discounts, surcharges etc)
            await Task.Yield();

            return new OrderCalculationResult
            {
                TotalAmount = 120_000,
                DiscountPercent = 0.7m,
            };
        }
    }
}