using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.TourDto;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Tour
{
    public class AddTourUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddTourUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DOMAIN.model.Tour> ExecuteAsync(TourDto dto)
        {
            var tour = new DOMAIN.model.Tour
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                DurationDays = dto.DurationDays
            };

           
            await _unitOfWork.Tour.AddAsync(tour);
            await _unitOfWork.SaveChangesAsync();
            return tour;
        }

    }
}
