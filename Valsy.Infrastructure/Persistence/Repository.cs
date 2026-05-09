using Microsoft.EntityFrameworkCore;
using Valsy.Domain;

namespace Valsy.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ValsyDbContext _context;

        public Repository(ValsyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task Update(T entity)
        {
            return Task.FromResult(_context.Set<T>().Update(entity));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        //public void Delete(T entity)
        //{
        //    _context.Set<T>().Remove(entity);
        //}
    }
}
