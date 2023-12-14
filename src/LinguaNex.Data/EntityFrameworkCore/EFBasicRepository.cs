using LinguaNex.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using IDbTransaction = System.Data.IDbTransaction;

namespace LinguaNex.EntityFrameworkCore
{
    public class EFBasicRepository<TEntity, TKey>(LinguaNexDbContext dbContext) : IBasicRepository<TEntity, TKey>
        where TEntity : class
    {
        private DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = (await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken)).Entity;
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            return savedEntity;
        }
        public async Task InsertManyAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = dbContext.Set<TEntity>().Update(entity).Entity;

            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            return savedEntity;
        }
        public async Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<TEntity>().Where(predicate).ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task UpdateManyAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            dbContext.Set<TEntity>().UpdateRange(entities);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
            if (entity != null)
                dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<TEntity>().Where(predicate).ExecuteDeleteAsync(cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task DeleteManyAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        }
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
        }
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return await GetQueryableWithIncludes(propertySelectors).Where(predicate).ToListAsync(cancellationToken);
        }
        public async Task<List<TSelect>> SelectListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return await GetQueryableWithIncludes(propertySelectors).Where(predicate).Select(selectPredicate).ToListAsync(cancellationToken);
        }
        public async Task<List<TSelect>> SelectListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(predicate).Select(selectPredicate).ToListAsync(cancellationToken);
        }
        public async Task<(List<TSelect> items, long total)> SelectPageListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default)
        {
            var query = GetQueryable().Where(predicate).Select(selectPredicate);
            var total = await query.LongCountAsync(cancellationToken);
            var items = await query.OrderBy(orderby)
                .Skip(skip).Take(take)
                .ToListAsync(cancellationToken);
            return (items, total);
        }
        public async Task<(List<TSelect> items, long total)> SelectPageListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryableWithIncludes(propertySelectors).Where(predicate).Select(selectPredicate);
            var total = await query.LongCountAsync(cancellationToken);
            var items = await query.OrderBy(orderby)
                .Skip(skip).Take(take)
                .ToListAsync(cancellationToken);
            return (items, total);
        }
        public async Task<(List<TEntity> items, long total)> GetPageListAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default)
        {
            var query = GetQueryable().Where(predicate);
            var total = await query.LongCountAsync(cancellationToken);
            var items = await query.OrderBy(orderby)
                .Skip(skip).Take(take)
                .ToListAsync(cancellationToken);
            return (items, total);
        }
        public async Task<(List<TEntity> items, long total)> GetPageListAsync(Expression<Func<TEntity, bool>> predicate,
            int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryableWithIncludes(propertySelectors).Where(predicate);
            var total = await query.LongCountAsync(cancellationToken);
            var items = await query.OrderBy(orderby)
                .Skip(skip).Take(take)
                .ToListAsync(cancellationToken);
            return (items, total);
        }

        public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return DbSet.AnyAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return DbSet.AnyAsync(predicate, cancellationToken);
        }
        public IQueryable<TEntity> GetQueryable(bool noTracking = true)
        {
            if (noTracking)
            {
                return dbContext.Set<TEntity>().AsNoTracking();
            }
            return dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> GetQueryableWithIncludes(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return Includes(GetQueryable(), propertySelectors);
        }

        public Expression<Func<TEntity, bool>> BuildPredicate(params (bool condition, Expression<Func<TEntity, bool>> predicate)[] conditionPredicates)
        {
            if (conditionPredicates == null || conditionPredicates.Length == 0)
            {
                throw new ArgumentNullException("conditionPredicates can not be null.");
            }
            Expression<Func<TEntity, bool>>? buildPredicate = null;
            foreach (var (condition, predicate) in conditionPredicates)
            {
                if (condition)
                {
                    if (buildPredicate == null)
                        buildPredicate = predicate;
                    else if (predicate != null)
                        buildPredicate = buildPredicate.And(predicate);
                }
            }
            if (buildPredicate == null)
            {
                buildPredicate = (o) => true;
            }
            return buildPredicate;
        }

        private static IQueryable<TEntity> Includes(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors != null && propertySelectors.Length > 0)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        protected DbSet<TEntity> GetDbSet()
        {
            return dbContext.Set<TEntity>();
        }

        protected IDbConnection GetDbConnection()
        {
            return dbContext.Database.GetDbConnection();
        }

        protected IDbTransaction? GetDbTransaction()
        {
            return dbContext.Database.CurrentTransaction?.GetDbTransaction();
        }

    }


    public class EFBasicRepository<TEntity>(LinguaNexDbContext dbContext) : EFBasicRepository<TEntity, object>(dbContext),
        IBasicRepository<TEntity>
        where TEntity : class;
}
