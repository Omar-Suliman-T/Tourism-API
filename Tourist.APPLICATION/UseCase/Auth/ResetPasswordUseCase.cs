using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class ResetPasswordUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            return await _unitOfWork.Auth.ResetPasswordAsync(resetPasswordDTO);
        }
    }
}
