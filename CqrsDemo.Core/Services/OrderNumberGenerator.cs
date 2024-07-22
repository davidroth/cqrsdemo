using CqrsDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Core.Services
{
    public class OrderNumberGenerator : IOrderNumberGenerator
    {
        private readonly SalesContext context;

        public OrderNumberGenerator(SalesContext context) => this.context = context;

        public async Task<string> GetNextOrderNumberAsync()
        {
            // Todo: switch to new logic via sequence
            var nextOrderNumber = await (from o in context.Orders
                                         where o.OrderDate.Year == DateTime.UtcNow.Year
                                         select o)
                                         .CountAsync();

            var orderNumber = DateTime.UtcNow.Year + "-" + (nextOrderNumber + 1).ToString().PadLeft(6, '0');
            return orderNumber;
        }
    }
}