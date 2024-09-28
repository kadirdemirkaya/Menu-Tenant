using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Application.Abstractions;
using Shared.Application.Services;
using Shared.Domain.BaseTypes;
using System.Linq.Expressions;
using Tenant.Infrastructure.Data;

namespace Tenant.Infrastructure.Repository
{
    public class Repository<T, TId> : IRepository<T, TId>
            where T : Entity<TId>
            where TId : ValueObject
    {
        private readonly MenuDbContext _dbContext;
        public DbSet<T> Table;

        public Repository(MenuDbContext dbContext)
        {
            _dbContext = dbContext;
            Table = _dbContext.Set<T>();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true, bool ignoreQueryFilter = false)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (ignoreQueryFilter)
                    query = query.IgnoreQueryFilters();

                if (expression is not null)
                    return await query.AnyAsync(expression);

                if (expression != null)
                    query = query.Where(expression);

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> AnyAsync(bool ignoreQueryFilter = false)
            => await AnyAsync(null, true, ignoreQueryFilter);

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, bool ignoreQueryFilter = false)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (ignoreQueryFilter)
                    query = query.IgnoreQueryFilters();


                if (expression is not null)
                    return await query.CountAsync(expression);

                if (expression != null)
                    query = query.Where(expression);

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }

        public async Task<int> CountAsync(bool ignoreQueryFilter = false)
            => await CountAsync(null, true, ignoreQueryFilter);

        public async Task<List<T>> GetAllAsync(bool tracking = true, bool ignoreQueryFilter = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            if (ignoreQueryFilter)
                query = query.IgnoreQueryFilters();

            return await Table.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            if (ignoreQueryFilter)
                query = query.IgnoreQueryFilters();

            if (includeEntity.Any())
                foreach (var include in includeEntity)
                    query = query.Include(include);

            if (expression != null)
                query = query.Where(expression);

            return await Table.ToListAsync();
        }

        public async Task<PaginatedList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, int pageIndex = 1,
    int pageSize = 10, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (ignoreQueryFilter)
                    query = query.IgnoreQueryFilters();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                var count = await query.CountAsync();

                var items = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return null;
            }
        }

        public async Task<PaginatedList<T>> GetAllAsync(bool tracking = true, int pageIndex = 1,
    int pageSize = 10, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (ignoreQueryFilter)
                    query = query.IgnoreQueryFilters();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);
                var count = await query.CountAsync();

                var items = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return null;
            }
        }

        public async Task<T> GetAsync(TId id) // ?
        {
            try
            {
                return await Table.Where(t => t.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return null;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, bool ignoreQueryFilter = false, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();

                if (!tracking)
                    query = query.AsNoTracking();

                if (ignoreQueryFilter)
                    query = query.IgnoreQueryFilters();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }
        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await Table.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                Table.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(T entityId)
        {
            try
            {
                Table.Remove(entityId);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                Table.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> SaveCahangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }
    }
}
