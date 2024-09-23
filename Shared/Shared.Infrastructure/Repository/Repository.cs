using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Application.Abstractions;
using Shared.Application.Services;
using System.Linq.Expressions;

namespace Shared.Application.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, bool tracking = true, params Expression<Func<TEntity, object>>[] includeEntity)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.ToListAsync();
            }
            catch (System.Exception)
            {
                return default;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, bool tracking = true, params Expression<Func<TEntity, object>>[] includeEntity)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.SingleOrDefaultAsync();
            }
            catch (System.Exception)
            {
                return default;
            }
        }

        public async Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = predicate == null ? _dbSet : _dbSet.Where(predicate);
            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TEntity>(items, totalItems, pageIndex, pageSize);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
