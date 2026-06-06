using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Valsy.Domain.Common.Abstractions;

namespace BuildingBlocks.Infrastructure.Repositories
{
    public class BaseDomainRepository<TAggregate> : BaseDomainRepository<TAggregate, int>, IDomainRepository<TAggregate>
        where TAggregate : class, IAggregateRoot<int>
    {
        public BaseDomainRepository(IRepository<TAggregate, int> genericRepository) : base(genericRepository)
        {
        }
    }

    public class BaseDomainRepository<TAggregate, TPrimaryKey> : IDomainRepository<TAggregate, TPrimaryKey>
        where TAggregate : class, IAggregateRoot<TPrimaryKey>
    {
        protected readonly IRepository<TAggregate, TPrimaryKey> _genericRepository;

        public BaseDomainRepository(IRepository<TAggregate, TPrimaryKey> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public virtual async Task<TAggregate> AddAsync(TAggregate aggregate)
        {
            return await _genericRepository.InsertAsync(aggregate);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TAggregate> aggregates)
        {
            await _genericRepository.InsertRangeAsync(aggregates);
        }

        public virtual async Task UpdateAsync(TAggregate aggregate)
        {
            await _genericRepository.UpdateAsync(aggregate);
        }
        public virtual async Task UpdateByPropertyAsync(TAggregate aggregate, string propertyName)
        {
            await _genericRepository.UpdateByPropertyAsync(aggregate, propertyName);
        }

        public virtual async Task DeleteAsync(TAggregate aggregate)
        {
            await _genericRepository.DeleteAsync(aggregate);
        }

        public virtual async Task DeleteAsync(TPrimaryKey aggregateKey)
        {
            TAggregate aggregate = await GetAsync(aggregateKey);
            await DeleteAsync(aggregate);
        }

        public virtual async Task<TAggregate> GetAsync(TPrimaryKey aggregateKey)
        {
            return await _genericRepository.GetAsync(aggregateKey);
        }

        public virtual async Task<TAggregate> GetAsyncOrDefault(TPrimaryKey aggregateKey)
        {
            return await _genericRepository.FirstOrDefaultAsync(entity => entity.Id.Equals(aggregateKey));
        }

        public virtual async Task<IEnumerable<TAggregate>> GetAllAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _genericRepository.GetAll().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TAggregate>> GetAllIncludingAsync(
            Expression<Func<TAggregate, bool>> predicate, Expression<Func<TAggregate, object>> includingPredicate)
        {
            return await _genericRepository.GetAllIncluding().Where(predicate).Include(includingPredicate)
                .ToListAsync();
        }
        public virtual async Task<IEnumerable<TAggregate>> GetAllIncludingListAsync(
           Expression<Func<TAggregate, bool>> predicate,
            List<Expression<Func<TAggregate, object>>> includingPredicates)
        {
            IQueryable<TAggregate> query = _genericRepository.GetAllIncluding().Where(predicate);
            foreach (Expression<Func<TAggregate, object>> includingPredicate in includingPredicates)
            {
                query = query.Include(includingPredicate);
            }
            return await query.ToListAsync();
        }

        public async Task<TAggregate> GetOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _genericRepository.FirstOrDefaultAsync(predicate);
        }

        public async Task GenerateKey(TAggregate aggregate)
        {
            if (!(aggregate is IHiloEntity))
                throw new Exception("Aggregate must implement IHiloEntity first see Product.cs for example");
            int id = await _genericRepository.GenerateKey(aggregate);
            aggregate.SetIntId(id);
        }
        public virtual async Task<TAggregate> FirstOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate,
           List<Expression<Func<TAggregate, object>>> includingPredicates) =>
           await _genericRepository.FirstOrDefaultAsync(predicate, includingPredicates);

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }
        public virtual async Task UpdateAsyncWithoutModifiedStatus(TAggregate aggregate)
        {
            await _genericRepository.UpdateAsyncWithoutModifiedStatus(aggregate);
        }
        public virtual async Task<TAggregate> FirstOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate) =>
            await _genericRepository.GetAllIncluding().FirstOrDefaultAsync(predicate);
        public virtual async Task<bool> ExistsAsync(Expression<Func<TAggregate, bool>> predicate) =>
            await _genericRepository.AnyAsync(predicate);

        public virtual async Task<List<TAggregate>> ExcuteSqlQueryAsync(string sqlQuery, CancellationToken cancellationToken = default)
        {
            List<TAggregate> data = await _genericRepository.ExecuteSqlQueryAsync(sqlQuery, cancellationToken);
            return data;
        }
        public virtual async Task ExcuteSqlCommandAsync(string sqlQuery, CancellationToken cancellationToken = default)
        {
            await _genericRepository.ExecuteSqlCommand(sqlQuery, cancellationToken);
        }
    }
}
