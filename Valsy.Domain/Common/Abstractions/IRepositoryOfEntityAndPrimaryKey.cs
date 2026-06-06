using System.Linq.Expressions;

namespace Valsy.Domain.Common.Abstractions
{
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Query
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        List<TEntity> GetAllList();

        Task<List<TEntity>> GetAllListAsync();

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TPrimaryKey id);

        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<TEntity> GetAsyncOrDefault(TPrimaryKey id);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
          List<Expression<Func<TEntity, object>>> includingPredicates);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> ExecuteSqlQueryAsync(string sqlQuery = "", CancellationToken cancellationToken = default);

        #endregion

        #region Insert

        TEntity Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        Task<TEntity> InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        TPrimaryKey InsertAndGetId(TEntity entity);

        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        TEntity InsertOrUpdate(TEntity entity);

        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

        #endregion

        #region Update

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        Task<TEntity> UpdateByPropertyAsync(TEntity entity, string propertyName);
        TEntity UpdateByProperty(TEntity entity, string propertyName);

        #endregion

        #region Delete

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(TPrimaryKey id);

        Task DeleteAsync(TPrimaryKey id);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> GenerateKey(TEntity entity);

        Task<int> ExecuteSqlCommand(string sqlCommand, CancellationToken cancellationToken = default);

        #endregion

        Task SaveChangesAsync();
        Task<TEntity> UpdateAsyncWithoutModifiedStatus(TEntity entity);
    }
}