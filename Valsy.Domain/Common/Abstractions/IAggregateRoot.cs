using Valsy.Domain.Common.Events;

namespace Valsy.Domain.Common.Abstractions;

public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }
    void ClearDomainEvents();
}
