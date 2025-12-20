using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class TripRepository : ITripRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IRepository<Trip> _repository;

        public TripRepository(
            ApplicationDbContext context, 
            IRepository<Trip> repository) 
        {
            _context = context;
            _repository = repository;
        }

        public async Task AddAsync(Trip trip)
        {
            await _repository.AddAsync(trip);
        }

        public async Task<Trip?> GetActiveTripByIdAsync(string userId)
        {
            return await _repository.GetAsync(
                x => x.UserId == userId &&
                   x.StartDate <= DateTime.Now &&
                   DateTime.Now <= x.EndDate
                );
        }

        public async Task<IEnumerable<Trip>> GetAllByIdAsync(string userId)
        {
            return await _repository.GetAllAsync(x => x.UserId == userId);
        }

        public async Task SoftRmoveAsync(int tripId)
        {
            var trip =  _context.Trips.FirstOrDefault(t => t.TripId == tripId);

            if (trip == null) throw new ArgumentNullException(nameof(trip));

            trip.IsDeleted = true;
        }
    }
}
