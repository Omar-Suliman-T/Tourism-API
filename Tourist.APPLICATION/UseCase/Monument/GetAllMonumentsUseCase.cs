using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Monument
{
    public class GetAllMonumentsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllMonumentsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DOMAIN.model.Monument>> ExecuteAsync()
        {
            return await _unitOfWork.Monument.GetAllAsync();
        }



    }
}
