using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common.Events;

namespace Valsy.Infrastructure.Common.DomainEventsDispatching
{
    public class DomainEventsProvider : IDomainEventsProvider
    {
        private readonly DbContext _meetingsContext;

        public DomainEventsProvider(DbContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            List<EntityEntry<IGenerateDomainEvents>> domainEntities = this._meetingsContext.ChangeTracker
                .Entries<IGenerateDomainEvents>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(entityEntry =>
                {
                    foreach (IDomainEvent entityDomainEvent in entityEntry.Entity.DomainEvents)
                        entityDomainEvent.EventSource = entityEntry.Entity;

                    return entityEntry.Entity.DomainEvents;
                }).ToList();
        }

        public void ClearAllDomainEvents()
        {
            List<EntityEntry<IGenerateDomainEvents>> domainEntities = this._meetingsContext.ChangeTracker
                .Entries<IGenerateDomainEvents>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entry => entry.Entity.ClearDomainEvents());
        }
    }
}
