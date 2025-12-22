using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IReviewRepository    
    {
        Task AddAsync(Review review);
        Task<IEnumerable<Review>> GetAllReviewsAsync(string userId);
        void SoftRmoveAsync(int reviewId);
    }
}
