using CqrsDemo.Core.Domain;
using CqrsDemo.Core.Tests.Infrastructure;
using Xunit;
using CqrsDemo.Core.Commands;
using Fusonic.Extensions.Mediator;

namespace CqrsDemo.Core.Tests.Commands
{
    public class CancelOrderTests : TestBase<InMemoryProviderFixture<SalesContext>, SalesContext>
    {
        public CancelOrderTests(InMemoryProviderFixture<SalesContext> fixture)
            : base(fixture)
        { }

        [Fact]
        public async Task OrderStatusIsSetToCancelled()
        {
            var order = new Order("Book", "123", 1, 1);
            Fixture.DbContextHelper.SaveNewEntity(order);

            var cancelOrderHandler = GetInstance<IRequestHandler<CancelOrder, Unit>>();
            await cancelOrderHandler.Handle(new CancelOrder()
            {
                OrderId = order.Id,
                Reason = "Sample reason 123",
            }, default);

            order = Fixture.DbContextHelper.GetEntity<Order>(order.Id);
            Assert.Equal(OrderStatus.Canceled, order.Status);
        }

        [Fact]
        public async Task ThrowsException_IfOrderIsAlreadyCancelled()
        {
            var order = new Order("Book", "123", 1, 1);
            using (CreateScope())
            {
                Fixture.DbContextHelper.SaveNewEntity(order);

                var cancelOrderHandler = GetInstance<IRequestHandler<CancelOrder, Unit>>();
                await cancelOrderHandler.Handle(new CancelOrder()
                {
                    OrderId = order.Id,
                    Reason = "Sample reason 123",
                }, default);
            }

            using (CreateScope())
            {
                var cancelOrderHandler = GetInstance<IRequestHandler<CancelOrder, Unit>>();
                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await cancelOrderHandler.Handle(new CancelOrder()
                    {
                        OrderId = order.Id,
                        Reason = "Sample reason 123",
                    }, default));
            }
        }
    }
}