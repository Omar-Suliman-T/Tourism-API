using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class DeleteHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // ================= DELETE =================
        public async Task<bool> ExcuteAsync(int id)
        {
            var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == id);

            if (hotel == null)
                return false;

            await _unitOfWork.Hotel.RemoveAsync(hotel);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
