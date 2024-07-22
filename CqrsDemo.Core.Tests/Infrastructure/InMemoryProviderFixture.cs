using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CqrsDemo.Core.Tests.Infrastructure
{
    public class InMemoryProviderFixture<TContext> : IDisposable
        where TContext : DbContext
    {
        private readonly Container container;
        public InMemoryProviderFixture()
        {
            container = InMemoryBootstrapper.Bootstrap();
            DbContextHelper = new DbContextTestHelper<TContext>(container);
        }

        public DbContextTestHelper<TContext> DbContextHelper { get; }

        public Container Container => container;

        public Scope BeginLifetimeScope()
        {
            return AsyncScopedLifestyle.BeginScope(container);
        }

        public void Dispose()
            => container.Dispose();
    }
}