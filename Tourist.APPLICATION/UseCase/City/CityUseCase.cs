using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.City
{
    public class CityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
