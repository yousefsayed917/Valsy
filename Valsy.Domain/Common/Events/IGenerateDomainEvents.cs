namespace Valsy.Domain.Common.Events
{
    public interface IGenerateDomainEvents
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
