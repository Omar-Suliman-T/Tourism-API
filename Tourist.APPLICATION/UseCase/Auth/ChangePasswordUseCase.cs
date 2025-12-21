using Azure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class ChangePasswordUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChangePasswordUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode,string)> ExecuteAsync(ClaimsPrincipal claims,ChangePasswordRequestDTO changePasswordRequestDTO)
        {
             (HttpStatusCode response,string result) = await _unitOfWork.Auth.ChangePasswordAsync(claims,changePasswordRequestDTO);
            
            return (response,result);
        }
    }
}
