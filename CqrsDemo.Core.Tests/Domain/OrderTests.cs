using CqrsDemo.Core.Domain;
using Xunit;

namespace CqrsDemo.Core.Tests.Domain
{
    public class OrderTests
    {
        [Fact]
        public void CancelOrder_StatusChangesToCancelled()
        {
            var order = new Order("Sample order", "0001", 1, 1);

            order.CancelOrder();
            Assert.Equal(OrderStatus.Canceled, order.Status);

            Assert.Contains(order.Events, x =>
            {
                return x is OrderCancelledEvent e
                    && e.OldStatus == OrderStatus.Active
                    && e.OrderId == order.Id;
            });

            Assert.Throws<InvalidOperationException>(() => order.CancelOrder());
        }
    }
}