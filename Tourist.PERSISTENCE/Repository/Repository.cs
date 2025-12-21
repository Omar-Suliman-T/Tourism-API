using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tourist.PERSISTENCE;

namespace Tourist.APPLICATION.Interface
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _db;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true, string? includeProperties = null)
        {
            IQueryable<T> query = tracking ? _db : _db.AsNoTracking();
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _db;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task SoftDeleteAsync(T entity)
        {
            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null)
            {
                property.SetValue(entity, true);
                Update(entity);
            }
            await Task.CompletedTask;
        }

        public async Task SoftDeleteRangeAsync(IEnumerable<T> entities)
        {
            var isDeletedProperty = typeof(T).GetProperty("IsDeleted");
            if (isDeletedProperty == null)
                throw new Exception("Entity does not have IsDeleted property");

            foreach (var entity in entities)
            {
                isDeletedProperty.SetValue(entity, true);
                Update(entity);
            }

            await Task.CompletedTask;
        }
    }
}
