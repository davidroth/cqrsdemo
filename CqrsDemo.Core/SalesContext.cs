using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CqrsDemo.Core.Domain;

namespace CqrsDemo.Core
{
    public class SalesContext : DbContextBase
    {
        [DebuggerStepThrough]
        public SalesContext(IMediator mediator, DbContextOptions options)
            : base(mediator, options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                 .HasMany(x => x.Positions)
                 .WithOne(x => x.Order);

            modelBuilder.Entity<Order>()
                .Metadata
                .FindNavigation(nameof(Order.Positions))
                ?.SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.PaymentDates)
                .WithOne(x => x.Order);
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<DispoDevice> DispoDevices => Set<DispoDevice>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
    }
}