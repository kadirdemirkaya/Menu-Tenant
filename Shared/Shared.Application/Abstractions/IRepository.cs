using Shared.Application.Services;
using System.Linq.Expressions;

namespace Shared.Application.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, bool tracking = true, params Expression<Func<TEntity, object>>[] includeEntity);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, bool tracking = true, params Expression<Func<TEntity, object>>[] includeEntity);

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

        Task<bool> SaveChangesAsync();

        Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate = null);
    }
}
