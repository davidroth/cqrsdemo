using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core
{
    public class SampleDataGenerator
    {
        private readonly SalesContext context;
        private readonly IOrderCalculator calculator;

        public SampleDataGenerator(SalesContext context, IOrderCalculator calculator)
        {
            this.context = context;
            this.calculator = calculator;
        }

        public async Task GenerateSampleData()
        {
            await context.Database.EnsureCreatedAsync();
            await CreateCustomers();
            await CreateDispos();
            await CreateOrders();
        }

        private async Task CreateCustomers()
        {
            var rand = new Random();
            context.DispoDevices.AddRange(Enumerable.Range(1, 10)
                .Select(i =>
                {
                    var dispo = new DispoDevice(rand.Next(10000, 20000).ToString())
                    {
                        Name = $"Dispo{i}",
                    };
                    dispo.GetType().GetProperty(nameof(DispoDevice.AvailabilityDate))!.SetValue(dispo, DateTime.UtcNow.AddDays(rand.Next(1000)));
                    return dispo;
                }));

            await context.SaveChangesAsync();
        }

        private async Task CreateDispos()
        {
            var rand = new Random();
            context.Customers.AddRange(Enumerable.Range(1, 11)
                .Select(i =>
                {
                    var customer = new Customer($"Customer {i}");
                    return customer;
                }));

            await context.SaveChangesAsync();
        }

        private async Task CreateOrders()
        {
            var rand = new Random();

            context.Products.AddRange(Enumerable.Range(0, 11)
                .Select(i =>
                {
                    var product = new Product($"LHM 60{i}");
                    return product;
                }));

            await context.SaveChangesAsync();

            var orderTasks = Enumerable.Range(1, 10)
                .Select(async i =>
                {
                    var order = new Order(
                        nameof(Order) + i.ToString(),
                        DateTime.UtcNow.Year + "-" + i.ToString().PadLeft(6, '0'),
                        context.ChangeTracker.Entries<Product>().ElementAt(i).Entity.Id,
                        context.ChangeTracker.Entries<Customer>().ElementAt(i).Entity.Id);

                    order.AddPosition(new OrderPosition("Position 1"));
                    order.AddPosition(new OrderPosition("Position 2"));
                    order.AddPosition(new OrderPosition("Position 3"));

                    order.GetType().GetProperty(nameof(Order.DeliveryDate))!.SetValue(order, DateTime.UtcNow.AddDays(rand.Next(1000)));

                    if (i % 2 == 0 && i < 10)
                    {
                        order.GetType().GetProperty(nameof(Order.DispoDeviceId))!.SetValue(order, i);
                    }

                    await order.Recalculate(calculator);
                    return order;
                });

            var orders = await Task.WhenAll(orderTasks);
            context.Orders.AddRange(orders);
            
            await context.SaveChangesAsync();
        }
    }
}