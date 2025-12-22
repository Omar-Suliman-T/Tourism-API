using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface ITripRepository
    {
        Task AddAsync(Trip trip);
        Task<Trip?> GetActiveTripByIdAsync(string userId);
        Task<IEnumerable<Trip>> GetAllByIdAsync(string userId);
        void SoftRmoveAsync(int tripId);
    }
}
