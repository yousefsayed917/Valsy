using System.ComponentModel.DataAnnotations;
using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Common.Events;

namespace Valsy.Domain.Common;

public abstract class AggregateRoot : AggregateRoot<int>
{

}
public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
{
    [ConcurrencyCheck]
    public byte[] RowVersion { get; set; }

    private List<IDomainEvent> _domainEvents;

    /// <summary>
    /// Domain events occurred.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();

        this._domainEvents.Add(domainEvent);
    }
}
