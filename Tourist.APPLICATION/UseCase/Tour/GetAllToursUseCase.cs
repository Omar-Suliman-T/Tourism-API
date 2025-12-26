using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Tour
{
    public class GetAllToursUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllToursUseCase(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Tourist.DOMAIN.model.Tour>> ExecuteAsync()
        {
            return  await _unitOfWork.Tour.GetAllAsync();
        }

    }
}
