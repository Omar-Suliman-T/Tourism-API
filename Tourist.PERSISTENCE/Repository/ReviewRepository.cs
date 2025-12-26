using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }


        public async Task SoftRmoveAsync(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(t => t.ReviewId == reviewId);

            if (review == null) throw new ArgumentNullException(nameof(review));

            review.IsDeleted = true;
        }
    }
}
