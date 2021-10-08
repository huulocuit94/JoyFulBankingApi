using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Web.Core.Infrastructures
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> GetEntities()
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return _dbSet.Where(x => !((ISoftDelete)x).IsDeleted);
            }
            return _dbSet;
        }

        public void ChangeTable(string table)
        {
            if (_dbContext.Model.FindEntityType(typeof(TEntity)) is IConventionEntityType relational)
            {
                relational.SetTableName(table);
            }
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = GetEntities();
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = GetEntities();
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstAsync(predicate);
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = GetEntities();
            if (predicate == null)
            {
                return await query.CountAsync();
            }
            else
            {
                return await query.CountAsync(predicate);
            }
        }
        public async Task<int> CountAsync(string filter)
        {
            var query = GetEntities();
            if (!string.IsNullOrEmpty(filter))
            {
                return query.Where(filter).Count();
            }
            return await query.CountAsync();
        }
        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbSet.LongCountAsync();
            }
            else
            {
                return await _dbSet.LongCountAsync(predicate);
            }
        }

        public T Max<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return _dbSet.Max(selector);
            else
                return _dbSet.Where(predicate).Max(selector);
        }

        public async Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return await _dbSet.MaxAsync(selector);
            else
                return await _dbSet.Where(predicate).MaxAsync(selector);
        }

        public T Min<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return _dbSet.Min(selector);
            else
                return _dbSet.Where(predicate).Min(selector);
        }

        public async Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return await _dbSet.MinAsync(selector);
            else
                return await _dbSet.Where(predicate).MinAsync(selector);
        }

        public async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
        {
            if (predicate == null)
                return await _dbSet.AverageAsync(selector);
            else
                return await _dbSet.Where(predicate).AverageAsync(selector);
        }

        public async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
        {
            if (predicate == null)
                return await _dbSet.SumAsync(selector);
            else
                return await _dbSet.Where(predicate).SumAsync(selector);
        }

        public bool Any(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
            {
                return _dbSet.Any();
            }
            else
            {
                return _dbSet.Any(selector);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
            {
                return await _dbSet.AnyAsync();
            }
            else
            {
                return await _dbSet.AnyAsync(selector);
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            if (typeof(IAuditedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditedEntity)entity).CreatedDate = DateTimeOffset.UtcNow;
                ((IAuditedEntity)entity).ModifiedDate = DateTimeOffset.UtcNow;
            }
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<TEntity> entities) => await _dbSet.AddRangeAsync(entities);

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<int> UpdateBatchAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return await _dbSet.AsNoTracking().Where(where).UpdateAsync(updateFactory);
        }

        public void UpdateRange(IList<TEntity> entities) => _dbSet.UpdateRange(entities);

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        public void RemoveRange(IList<TEntity> entities) => _dbSet.RemoveRange(entities);

        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = GetEntities();
            try
            {
                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (include != null)
                {
                    query = include(query);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query;
            }
            catch (Exception ex)
            {
                return new TEntity[] { }.AsQueryable();
            }
        }
        public async Task<IQueryable<TEntity>> Query(string filter, string order, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            try
            {
                IQueryable<TEntity> query = GetEntities();
                if (include != null)
                {
                    query = include(query);
                }
                if (query.Any() && !string.IsNullOrEmpty(filter))
                {
                    return query.Where(filter).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                return query.OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            catch (Exception ex)
            {
                return new TEntity[] { }.AsQueryable();
            }

        }

        public void ChangeEntityState(TEntity entity, EntityState state)
        {
            _dbContext.Entry(entity).State = state;
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = GetEntities();
            if (include != null)
            {
                query = include(query);
            }
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = GetEntities();
            if (include != null)
            {
                query = include(query);
            }
            return await query.SingleOrDefaultAsync(predicate);
        }
    }
}