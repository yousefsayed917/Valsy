namespace Valsy.Domain;

public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }
    void ClearDomainEvents();
}
