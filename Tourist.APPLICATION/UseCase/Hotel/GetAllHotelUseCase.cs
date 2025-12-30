using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class GetAllHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, List<DOMAIN.model.Hotel>)> ExecuteAsync()
        {

            var hotels = await _unitOfWork.Hotel.GetAllAsync(h => h.IsDeleted == false);

            return (HttpStatusCode.OK, hotels.ToList());
        }
    }
}
