using System.Linq.Expressions;

namespace Valsy.Domain.Common.Abstractions
{
    public interface IDomainRepository<TAggregate> : IDomainRepository<TAggregate, int>
        where TAggregate : IAggregateRoot<int>
    {

    }

    public interface IDomainRepository<TAggregate, in TPrimaryKey> where TAggregate : IAggregateRoot<TPrimaryKey>
    {
        Task<TAggregate> GetOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<TAggregate> AddAsync(TAggregate aggregate);
        Task AddRangeAsync(IEnumerable<TAggregate> aggregates);
        Task UpdateAsync(TAggregate aggregate);
        Task DeleteAsync(TAggregate aggregate);
        Task DeleteAsync(TPrimaryKey aggregateKey);
        Task<TAggregate> GetAsync(TPrimaryKey aggregateKey);
        Task<TAggregate> GetAsyncOrDefault(TPrimaryKey aggregateKey);
        Task<IEnumerable<TAggregate>> GetAllAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<IEnumerable<TAggregate>> GetAllIncludingAsync(Expression<Func<TAggregate, bool>> predicate,
            Expression<Func<TAggregate, object>> includingPredicate);
        Task<IEnumerable<TAggregate>> GetAllIncludingListAsync(Expression<Func<TAggregate, bool>> predicate,
            List<Expression<Func<TAggregate, object>>> includingPredicate);
        Task SaveChangesAsync();
        Task GenerateKey(TAggregate aggregate);
        Task UpdateAsyncWithoutModifiedStatus(TAggregate aggregate);
        Task UpdateByPropertyAsync(TAggregate aggregate, string propertyName);
        Task<TAggregate> FirstOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate,
             List<Expression<Func<TAggregate, object>>> includingPredicates);
        Task<TAggregate> FirstOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<List<TAggregate>> ExcuteSqlQueryAsync(string sqlQuery, CancellationToken cancellationToken = default);
        Task ExcuteSqlCommandAsync(string sqlQuery, CancellationToken cancellationToken = default);

    }
}
