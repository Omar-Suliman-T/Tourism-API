using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Auth
{
    public class GoogleAuthUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoogleAuthUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthDTOs> ExcuteAsync(GoogleLoginDTO googleLoginDTO)
        {
            return await _unitOfWork.Auth.GoogleAuth(googleLoginDTO);
        }
    }
}
