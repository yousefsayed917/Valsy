namespace Valsy.Domain
{
    public interface IRepository<T> where T : class
    {
        Task SaveChangesAsync();
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task Update(T entity);

        //void Delete(T entity);
    }
}
