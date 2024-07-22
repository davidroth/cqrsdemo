using Microsoft.EntityFrameworkCore;
using CqrsDemo.Core.Domain;
using System.Diagnostics;

namespace CqrsDemo.Infrastructure
{
    [DebuggerStepThrough]
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase(IMediator mediator, DbContextOptions options)
           : base(options) => Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        protected IMediator Mediator { get; }

        public override int SaveChanges() => SaveChangesAsync().Result;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities =
                (from entry in ChangeTracker.Entries<Entity>()
                 where (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                 && entry.Entity.Events.Any()
                 select entry.Entity).ToList();

            var value = await base.SaveChangesAsync(cancellationToken);

            foreach (var changed in changedEntities)
            {
                foreach (var evt in changed.Events.ToList())
                {
                    await Mediator.Publish(evt, cancellationToken);
                    changed.Events.Remove(evt);
                }
            }
            return value;
        }
    }
}