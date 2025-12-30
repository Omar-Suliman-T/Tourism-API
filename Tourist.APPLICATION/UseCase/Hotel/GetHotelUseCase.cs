using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class GetHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, DOMAIN.model.Hotel)> ExecuteAsync(int HotelId)
        {

            var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == HotelId && h.IsDeleted == false);
            if (hotel == null)
            {
                return (HttpStatusCode.NotFound, new DOMAIN.model.Hotel());
            }
            return (HttpStatusCode.OK, hotel);
        }
    }
}
