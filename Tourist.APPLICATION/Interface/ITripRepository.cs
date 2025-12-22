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
        Task<Trip?> GetActiveTripByIdAsync(string userId);
        Task SoftRmoveAsync(int tripId);
    }
}
