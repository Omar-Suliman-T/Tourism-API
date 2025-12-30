using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Trip
{
    public class RemoveTripUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTripUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int tripId)
        {
            await _unitOfWork.Trip.SoftRmoveAsync(tripId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
