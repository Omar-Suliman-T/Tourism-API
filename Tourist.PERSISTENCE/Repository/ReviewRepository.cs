using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Review> _repository;
        public ReviewRepository(ApplicationDbContext context, IRepository<Review> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task AddAsync(Review review)
        {
            await _repository.AddAsync(review);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync(string userId)
        {
            return await _repository.GetAllAsync(x => x.UserId == userId);
        }


        public void SoftRmoveAsync(int reviewId)
        {
            var review = _context.Reviews.FirstOrDefault(t => t.ReviewId == reviewId);

            if (review == null) throw new ArgumentNullException(nameof(review));

            review.IsDeleted = true;
        }
    }
}
