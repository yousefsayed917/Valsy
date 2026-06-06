namespace Valsy.Domain.Common.Events
{
    public class DomainEventBase : IDomainEvent
    {
        public object EventSource { get; set; }
    }
}