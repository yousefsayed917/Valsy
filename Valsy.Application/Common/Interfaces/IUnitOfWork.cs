namespace Valsy.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginAsync();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
        Task SaveCurrentChangesAsync();
        void StopRollBack();
    }
}
