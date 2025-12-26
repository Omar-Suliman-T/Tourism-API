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
    public class AddHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, string)> ExecuteAsync(AddHotelDTO hotelDto)
        {
            try
            {
                var hotel = new DOMAIN.model.Hotel()
                {
                    Name = hotelDto.Name,
                    Description = hotelDto.Description,
                    Stars = hotelDto.Stars,
                    PricePerNight = hotelDto.PricePerNight,
                    Address = hotelDto.Address,
                    Phone = hotelDto.Phone,
                    ImageUrl = hotelDto.ImageUrl,
                    CityId = hotelDto.CityId
                };

                await _unitOfWork.Hotel.AddAsync(hotel);
                await _unitOfWork.SaveChangesAsync();
                return (HttpStatusCode.OK, "Hotel Added Successfully");
            }
            catch (Exception ex)
            {

                return (HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}
