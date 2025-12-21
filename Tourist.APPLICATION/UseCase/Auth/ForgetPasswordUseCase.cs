using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class ForgetPasswordUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForgetPasswordUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        

        public async Task<string> ExecuteAsync(ForgetPasswordDTO dto)
        {
            return await _unitOfWork.Auth.ForgetPasswordAsync(dto);
        }
    }
}
