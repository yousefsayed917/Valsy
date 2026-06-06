using Valsy.Domain.Common;
using Valsy.Domain.Common.Events;

namespace Valsy.Application.Common.Interfaces
{
    public interface IDomainEventsProvider
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();
        void ClearAllDomainEvents();
    }
}
