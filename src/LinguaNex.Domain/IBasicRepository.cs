using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LinguaNex.Domain
{
    public interface IBasicRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task InsertManyAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateManyAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteManyAsync(List<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<List<TSelect>> SelectListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, CancellationToken cancellationToken = default);
        Task<List<TSelect>> SelectListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<(List<TSelect> items, long total)> SelectPageListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default);
        Task<(List<TSelect> items, long total)> SelectPageListAsync<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> selectPredicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<(List<TEntity> items, long total)> GetPageListAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default);
        Task<(List<TEntity> items, long total)> GetPageListAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, string orderby = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] propertySelectors);
        IQueryable<TEntity> GetQueryable(bool noTracking = true);

        IQueryable<TEntity> GetQueryableWithIncludes(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
        Expression<Func<TEntity, bool>> BuildPredicate(params (bool condition, Expression<Func<TEntity, bool>> predicate)[] conditionPredicates);
    }

    public interface IBasicRepository<TEntity> : IBasicRepository<TEntity, object> where TEntity : class
    {

    }
}
