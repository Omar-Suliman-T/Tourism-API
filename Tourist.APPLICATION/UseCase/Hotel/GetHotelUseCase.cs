using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class GetHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // ================= GET ALL =================
        public async Task<IEnumerable<Tourist.DOMAIN.model.Hotel>> ExcuteAsync()
        {
            return await _unitOfWork.Hotel.GetAllAsync(c=>true,includeProperities: "City");
        }
    }
}
 