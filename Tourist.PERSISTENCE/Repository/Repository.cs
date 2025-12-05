using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tourist.PERSISTENCE;

namespace Tourist.APPLICATION.Interface
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;
        public Repository(ApplicationDbContext context)
        {
            _context=context;
            _db = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await _context.AddAsync(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true, string? includeProperities = null)
        {
            IQueryable<T> result;
            if (tracking)
            {
                result = _db.Where(filter);
            }
            else
            {
                result = _db.AsNoTracking().Where(filter);
            }
         
            if(string.IsNullOrEmpty(includeProperities))
            {
                foreach(var include in includeProperities.Split(',',StringSplitOptions.RemoveEmptyEntries))
                {
                    result=result.Include(include);
                }
            }

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> ?filter=null,string? includeProperities = null)
        {
            IQueryable<T> result;

            if (filter !=null)
            {
                result = _db.Where(filter);
            }
            else
            {
                result = _db;
            }
            if(string.IsNullOrEmpty(includeProperities))
            {
                foreach(var include in includeProperities.Split( ',' ,StringSplitOptions.RemoveEmptyEntries))
                {
                    result=result.Include(include);
                }
            }
            return result.ToList();
        }

        public async Task RemoveAsync(T Entity)
        {
           _db.Remove(Entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }
    }
}
