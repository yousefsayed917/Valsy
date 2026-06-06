using MediatR;
namespace Valsy.Domain.Common.Events
{
    public interface IDomainEvent : INotification
    {
        object EventSource { get; set; }
    }
}
