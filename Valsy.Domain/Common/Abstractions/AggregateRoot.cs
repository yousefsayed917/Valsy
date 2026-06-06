using System.ComponentModel.DataAnnotations;

namespace Valsy.Domain;

public abstract class AggregateRoot : AggregateRoot<int>
{
}

public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
{
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    private List<IDomainEvent>? _domainEvents;

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }
}
