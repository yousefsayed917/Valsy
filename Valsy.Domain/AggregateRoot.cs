using System.ComponentModel.DataAnnotations;

namespace Valsy.Domain;

public abstract class AggregateRoot : AggregateRoot<Guid>
{
}

public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    [ConcurrencyCheck]
    public byte[] RowVersion { get; protected set; } = Array.Empty<byte>();

    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected void MarkCreated(TPrimaryKey id, string createdBy)
    {
        Id = id;
        SetCreationAudit(createdBy);
    }

    protected void MarkModified(string modifiedBy)
    {
        SetModificationAudit(modifiedBy);
    }
}
