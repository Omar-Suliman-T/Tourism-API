using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.TourDto;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Tour
{
    public class UpdateTourUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTourUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode,DOMAIN.model.Tour)> ExeucuteAsync(int id, TourDto dto)
        {
            var tour = await _unitOfWork.Tour.GetAsync(t => t.Id == id, includeProperities: "TourMonuments");

            if (tour == null) return (HttpStatusCode.NotFound,null!);

            tour.Title = dto.Title;
            tour.Description = dto.Description;
            tour.Price = dto.Price;
            tour.DurationDays = dto.DurationDays;

            tour.TourMonuments.Clear();
            if (dto.MonumentIds != null)
            {
                foreach (var monumentId in dto.MonumentIds)
                    tour.TourMonuments.Add(new TourMonument { MonumentId = monumentId, TourId = id });
            }

            await _unitOfWork.SaveChangesAsync();
            return(HttpStatusCode.OK, tour);
        }
    }
}
