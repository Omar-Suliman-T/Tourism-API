using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.Mapping.Auth;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class RegisterUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RegisterMap _registerMap;

        public RegisterUseCase(IUnitOfWork unitOfWork,
            RegisterMap registerMap)
        {
            _unitOfWork = unitOfWork;
            _registerMap = registerMap;
        }
        public async Task<string> ExecuteAsync(RegisterDTOs registerDTOs)
        {
            var user = _registerMap.ToRegister(registerDTOs);
            return await _unitOfWork.Auth.RegisterAsync(user, registerDTOs.Password);
             
        }

    }
}
