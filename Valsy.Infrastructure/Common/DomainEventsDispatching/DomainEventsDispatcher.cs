using MediatR;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common.Events;

namespace Valsy.Infrastructure.Common.DomainEventsDispatching
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        private readonly IDomainEventsProvider _domainEventsProvider;

        public DomainEventsDispatcher(
            IMediator mediator,
            IDomainEventsProvider domainEventsProvider)
        {
            _mediator = mediator;
            _domainEventsProvider = domainEventsProvider;
        }

        public async Task DispatchEventsAsync()
        {
            IReadOnlyCollection<IDomainEvent> domainEvents = _domainEventsProvider.GetAllDomainEvents();
            _domainEventsProvider.ClearAllDomainEvents();
            foreach (IDomainEvent domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}
