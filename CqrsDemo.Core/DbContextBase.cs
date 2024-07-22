using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CqrsDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Core
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
                    && (entry.Entity.Events.Any() || entry.Entity is IPublishCreatedEvent)
                 select (entry.Entity, entry.State)).ToList();

            foreach (var entity in ChangeTracker.Entries<Entity>().Select(x => x.Entity).OfType<IValidatableObject>())
            {
                var validationContext = new ValidationContext(entity);
                var results = entity.Validate(validationContext);
                if (results.Any())
                    throw new EntityValidationException($"Failed storing entity '{entity.GetType().Name}' with the following validation error(s): {string.Join("\n", results.Select(x => x.ErrorMessage))}");
            }

            var value = await base.SaveChangesAsync(cancellationToken);

            foreach (var (entity, state) in changedEntities)
            {
                if (state == EntityState.Added && entity is IPublishCreatedEvent createEvent)
                {
                    createEvent.AddCreatedEvent();
                }

                foreach (var evt in entity.Events.ToList())
                {
                    await Mediator.Publish(evt, cancellationToken);
                    entity.Events.Remove(evt);
                }
            }
            return value;
        }
    }
}