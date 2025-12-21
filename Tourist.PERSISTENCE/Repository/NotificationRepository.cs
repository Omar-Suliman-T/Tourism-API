using Microsoft.EntityFrameworkCore;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tourist.PERSISTENCE.Repository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetUnreadByUserAsync(string userId)
        {
            return await _context.Notifications
                                 .Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted)
                                 .ToListAsync();
        }

        public async Task SoftDeleteAsync(Notification entity)
        {
            entity.IsDeleted = true;
            Update(entity); 
            await Task.CompletedTask;
        }

       
        public async Task SoftDeleteRangeAsync(IEnumerable<Notification> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                Update(entity); 
            }
            await Task.CompletedTask;
        }
    }
}
