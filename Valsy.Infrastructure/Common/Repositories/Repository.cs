using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Common.Extensions;

namespace Valsy.Infrastructure.Common.Repositories
{
    public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity<int>
    {
        public Repository(DbContext dbContext, IHilo _hilo) : base(dbContext, _hilo)
        {
        }
    }

    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext Context;
        protected readonly IHilo _hilo;
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public Repository(DbContext dbContext, IHilo hilo)
        {
            Context = dbContext;
            _hilo = hilo;
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(entity => entity.Id.As<TPrimaryKey>().Equals(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return GetAll().FirstOrDefaultAsync(entity => entity.Id.As<TPrimaryKey>().Equals(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefaultAsync(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            List<Expression<Func<TEntity, object>>> includingPredicates)
        {
            IQueryable<TEntity> query = GetAll();
            includingPredicates?.ForEach(x => query = query.Include(x));

            return query.FirstOrDefaultAsync(predicate);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        => GetAll().AnyAsync(predicate);

        public TEntity Get(TPrimaryKey id)
        {
            TEntity entity = FirstOrDefault(id);
            if (entity == null)
                throw new ArgumentException($"Not exits entity with Id {id}");

            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            TEntity entity = await FirstOrDefaultAsync(id);
            if (entity == null)
                throw new ArgumentException($"Not exits entity with Id {id}");

            return entity;
        }

        public virtual async Task<TEntity> GetAsyncOrDefault(TPrimaryKey id)
        {
            return await FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }
        public virtual async Task<List<TEntity>> ExecuteSqlQueryAsync(string sqlQuery, CancellationToken cancellationToken = default)
        {
            List<TEntity> data = await Context.Set<TEntity>()
                .FromSqlRaw(sqlQuery)
                .ToListAsync();

            return data;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity is IHiloEntity
                && !entity.IsIdFetched()
                )
            {
                int id = await GenerateKey(entity);
                entity.SetIntId(id);
            }
            return (await Table.AddAsync(entity)).Entity;
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                if (entity is IHiloEntity
              && !entity.IsIdFetched()
              )
                {
                    int id = await GenerateKey(entity);
                    entity.SetIntId(id);
                }
            }
            await Table.AddRangeAsync(entities);
        }

        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }
        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public virtual TEntity UpdateByProperty(TEntity entity, string propertyName)
        {
            AttachIfNot(entity);
            Context.Entry(entity).Property(propertyName).IsModified = true;
            return entity;
        }
        public virtual Task<TEntity> UpdateByPropertyAsync(TEntity entity, string propertyName)
        {
            return Task.FromResult(UpdateByProperty(entity, propertyName));
        }

        public virtual TEntity UpdateWithoutModifiedStatus(TEntity entity)
        {
            AttachIfNot(entity);
            return entity;
        }

        public virtual Task<TEntity> UpdateAsyncWithoutModifiedStatus(TEntity entity)
        {
            return Task.FromResult(UpdateWithoutModifiedStatus(entity));
        }


        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }


        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            TEntity entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            TEntity entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public virtual void Delete(TPrimaryKey id)
        {
            TEntity entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> entities = GetAllList(predicate);

            foreach (TEntity entity in entities)
                Delete(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> entities = await GetAllListAsync(predicate);

            foreach (TEntity entity in entities)
            {
                await DeleteAsync(entity);
            }
        }

        private void AttachIfNot(TEntity entity)
        {
            EntityEntry entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            EntityEntry entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }

        public async Task<int> GenerateKey(TEntity entity)
        {
            entity.FetchId();
            return await _hilo.GenerateIntId(typeof(TEntity), Context);
        }
        public Task<int> ExecuteSqlCommand(string sqlCommand, CancellationToken cancellationToken = default)
        {
            //TODO MAKE TIMEOUT parameter
            DatabaseFacade database = Context.Database;
            // SET 50 MIN TIMEOUT
            database.SetCommandTimeout((int)TimeSpan.FromMinutes(50).TotalSeconds);
            int data = database.ExecuteSqlRaw(sqlCommand);
            // set it back to 5 min
            database.SetCommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
            return Task.FromResult(data);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}

