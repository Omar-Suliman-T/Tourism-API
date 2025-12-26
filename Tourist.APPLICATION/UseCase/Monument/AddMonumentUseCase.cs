using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Monument;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Monument
{
    public class AddMonumentUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddMonumentUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DOMAIN.model.Monument> ExecuteAsync(MonumentDto dto)
        {
            var monument = new DOMAIN.model.Monument
            {
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                YearBuilt = dto.YearBuilt
            };
            await _unitOfWork.Monument.AddAsync(monument);
            await _unitOfWork.SaveChangesAsync();
            return monument;
        }
        
    }
}
