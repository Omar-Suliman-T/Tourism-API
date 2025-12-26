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
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Review>> GetAllWithDetailsAsync()
        {
            return await _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Trip)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
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
