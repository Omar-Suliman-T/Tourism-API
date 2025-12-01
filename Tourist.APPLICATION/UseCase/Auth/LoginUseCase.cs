using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.Mapping.Auth;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LoginMap _loginMap;

        public LoginUseCase(IUnitOfWork unitOfWork,LoginMap loginMap)
        {
            _unitOfWork = unitOfWork;
            _loginMap = loginMap;
        }

        public async Task<AuthDTOs> ExecuteAsync(LoginDTOs loginDto)
        {
            var result = await _unitOfWork.Auth.LoginAsync(loginDto);

            return result;
        }
    
    }
}
