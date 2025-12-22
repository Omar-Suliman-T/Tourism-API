using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class UpdateHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // ================= UPDATE =================
        public async Task<bool> ExcuteAsync(int id, Tourist.DOMAIN.model.Hotel updatedHotel)
        {
            var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == id);

            if (hotel == null)
                return false;

            hotel.Name = updatedHotel.Name;
            hotel.Description = updatedHotel.Description;
            hotel.PricePerNight = updatedHotel.PricePerNight;
            hotel.Stars = updatedHotel.Stars;
            hotel.Address = updatedHotel.Address;
            hotel.Phone = updatedHotel.Phone;
            hotel.ImageUrl = updatedHotel.ImageUrl;
            hotel.CityId = updatedHotel.CityId;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
