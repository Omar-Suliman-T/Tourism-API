using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class GetByIdHotelUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        public GetByIdHotelUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // ================= GET BY ID =================
        public async Task<Tourist.DOMAIN.model.Hotel?> ExcuteAsync(int id)
        {
            return await unitOfWork.Hotel
                .GetAsync(h => h.HotelId == id, includeProperities: "City");
        }
    }
}
