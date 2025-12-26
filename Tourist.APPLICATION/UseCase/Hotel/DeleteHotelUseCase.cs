using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<(HttpStatusCode, string)> ExecuteAsync(int HotelId)
        {
            try
            {
                var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == HotelId && h.IsDeleted == false);
                if (hotel == null)
                {
                    return (HttpStatusCode.NotFound, "Hotel Not Found");
                }
                hotel.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return (HttpStatusCode.OK, "Hotel Deleted Successfully");
            }
            catch (Exception ex)
            {
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
