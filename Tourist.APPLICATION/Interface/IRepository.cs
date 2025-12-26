using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.Interface
{
    public interface IRepository<T> where T:class
    {
        Task<T> GetAsync(Expression<Func<T,bool>>filter, bool tracking=true,string?includeProperities = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> ?filter=null,string? includeProperities = null);
        Task RemoveAsync(T Entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task AddAsync(T entity);
    }
}
