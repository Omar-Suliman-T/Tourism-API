using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllWithDetailsAsync();
        Task<Review?> GetByIdWithDetailsAsync(int id);
    }
}
