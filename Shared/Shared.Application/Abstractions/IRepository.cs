using Shared.Application.Services;
using Shared.Domain.BaseTypes;
using System.Linq.Expressions;

namespace Shared.Application.Abstractions
{
    public interface IRepository<T, TId>
      where T : Entity<TId>
      where TId : ValueObject
    {
        Task<PaginatedList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, int pageIndex = 1,
    int pageSize = 10, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity);
        Task<PaginatedList<T>> GetAllAsync(bool tracking = true, int pageIndex = 1,
    int pageSize = 10, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity);

        Task<List<T>> GetAllAsync(bool tracking = true, bool ignoreQueryFilter = false);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true, bool ignoreQueryFilter = false);
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, bool ignoreQueryFilter = false);
        Task<bool> AnyAsync(bool ignoreQueryFilter = false);
        Task<int> CountAsync(bool ignoreQueryFilter = false);


        public Task<bool> CreateAsync(T entity);
        public bool Delete(T entity);
        public Task<bool> DeleteByIdAsync(T entityId);
        public bool Update(T entity);

        Task<bool> SaveCahangesAsync();
    }
}
