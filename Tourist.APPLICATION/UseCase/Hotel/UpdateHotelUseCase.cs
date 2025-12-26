using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Hotel;
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
        public async Task<(HttpStatusCode, string)> ExecuteAsync(UpdateHotelDTO hotelDto)
        {
            try
            {

                var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == hotelDto.HotelId && h.IsDeleted == false);
                if (hotel == null)
                {
                    return (HttpStatusCode.NotFound, "Hotel Not Found");
                }
                hotel.Name = hotelDto.Name;
                hotel.Description = hotelDto.Description;
                hotel.Stars = hotelDto.Stars;
                hotel.PricePerNight = hotelDto.PricePerNight;
                hotel.Address = hotelDto.Address;
                hotel.Phone = hotelDto.Phone;
                hotel.ImageUrl = hotelDto.ImageUrl;
                await _unitOfWork.SaveChangesAsync();
                return (HttpStatusCode.OK, "Hotel Updated Successfully");
            }
            catch (Exception ex)
            {
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
