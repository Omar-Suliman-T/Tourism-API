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
    public class TripRepository :  Repository<Trip>, ITripRepository
    {

        private readonly ApplicationDbContext _context;

        public TripRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }


        public async Task<Trip?> GetActiveTripByIdAsync(string userId)
        {
            return await _context.Trips.FirstOrDefaultAsync(
                x => x.UserId == userId &&
                   x.StartDate <= DateTime.Now &&
                   DateTime.Now <= x.EndDate
                );
        }

        
        public async Task SoftRmoveAsync(int tripId)
        {
            var trip =  await _context.Trips.FirstOrDefaultAsync(t => t.TripId == tripId);

            if (trip == null) throw new ArgumentNullException(nameof(trip));

            trip.IsDeleted = true;
        }

    }
}
