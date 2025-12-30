using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Hotel
{
        public class CreateHotelUseCase
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateHotelUseCase(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            // ================= CREATE =================
            public async Task ExcuteAsync(Tourist.DOMAIN.model.Hotel hotel)
            {
                await _unitOfWork.Hotel.AddAsync(hotel);
                await _unitOfWork.SaveChangesAsync();
            }

            

            

          

            
        }
}


