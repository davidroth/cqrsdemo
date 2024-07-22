using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using Xunit;

namespace CqrsDemo.Core.Tests.Infrastructure
{
    public abstract class TestBase<TFixture, TDbContext> : IClassFixture<TFixture>, IDisposable
        where TFixture : InMemoryProviderFixture<TDbContext>, new()
        where TDbContext : DbContext
    {
        private readonly Scope scope;

        protected TestBase(TFixture fixture)
        {
            Fixture = fixture;
            scope = Fixture.BeginLifetimeScope();
            Fixture.DbContextHelper.DeleteAndCreateDataStore();
        }

        protected TFixture Fixture { get; }

        protected virtual T GetInstance<T>() where T : class
            => Fixture.Container.GetInstance<T>();

        public IDisposable CreateScope() => Fixture.BeginLifetimeScope();

        public void Dispose() => scope.Dispose();
    }
}