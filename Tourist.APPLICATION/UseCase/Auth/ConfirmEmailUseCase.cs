using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class ConfirmEmailUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmEmailUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ExecuteAsync(ConfirmEmailDTO confirmEmailDTO)
        {
            return await _unitOfWork.Auth.ConfirmEmailAsync(confirmEmailDTO);
        }
    }
}
