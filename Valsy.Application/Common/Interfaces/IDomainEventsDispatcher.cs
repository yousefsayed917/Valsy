namespace Valsy.Application.Common.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
