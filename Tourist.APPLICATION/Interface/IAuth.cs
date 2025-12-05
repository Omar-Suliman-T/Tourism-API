using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IAuth
    {
        Task<string> RegisterAsync(ApplicationUser user, string Password);

        Task<(HttpStatusCode, string)> ChangePasswordAsync(ClaimsPrincipal User, ChangePasswordRequestDTO request);


        Task<string> ForgetPasswordAsync(ForgetPasswordDTO forgetPasswordDTO);
        Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);

        Task<AuthDTOs> LoginAsync(LoginDTOs loginDTOs);


    }
}
