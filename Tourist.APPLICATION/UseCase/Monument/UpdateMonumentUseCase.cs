using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Monument;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Monument
{
    public class UpdateMonumentUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateMonumentUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode,DOMAIN.model.Monument)> ExecuteAsync(int id, MonumentDto dto)
        {
            var monument = await _unitOfWork.Monument.GetAsync(m=>m.Id==id);
            if (monument == null) return (HttpStatusCode.NotFound,null!);

            monument.Name = dto.Name;
            monument.Description = dto.Description;
            monument.Location = dto.Location;
            monument.YearBuilt = dto.YearBuilt;

            await _unitOfWork.SaveChangesAsync();
            return (HttpStatusCode.OK,monument);
        }
    }
}
