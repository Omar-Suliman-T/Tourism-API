using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<IEnumerable<Review>> GetAllWithDetailsAsync(Expression<Func<Review, bool>>? filter = null)
        {
            IQueryable<Review> query = _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Trip)
                .Where(r => !r.IsDeleted);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<Review?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(r => r.ReviewId == id && !r.IsDeleted);
        }
    }
}
