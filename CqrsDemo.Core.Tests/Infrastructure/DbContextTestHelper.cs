using System.Linq.Expressions;
using CqrsDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CqrsDemo.Core.Tests.Infrastructure
{
    public sealed class DbContextTestHelper<TContext>
        where TContext : DbContext
    {
        private Container container;

        public DbContextTestHelper(Container container)
        {
            this.container = container;
        }

        public void DeleteAndCreateDataStore()
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                using (var context = container.GetInstance<TContext>())
                {
                    context.Database.EnsureDeleted();
                }
            }
        }

        public TEntity GetEntity<TEntity>(int id, Func<DbSet<TEntity>, IQueryable<TEntity>> queryManipulation = null)
            where TEntity : Entity
            => GetEntity<TEntity>(x => x.Id == id, queryManipulation);

        public TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<DbSet<TEntity>, IQueryable<TEntity>> queryManipulation)
            where TEntity : Entity
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                using var context = container.GetInstance<TContext>();
                var query = context.Set<TEntity>();
                if (queryManipulation != null)
                {
                    return queryManipulation(query).Single(predicate);
                }
                return query.Single(predicate);
            }
        }

        public void SaveNewEntity<TEntity>(TEntity order)
           where TEntity : Entity
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                using (var context = container.GetInstance<TContext>())
                {
                    context.Set<TEntity>().Add(order);
                    context.SaveChanges();
                }
            }
        }
    }
}