using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IAuth
    {
        Task<string> RegisterAsync(ApplicationUser user, string Password);
        Task<AuthDTOs> LoginAsync(LoginDTOs loginDTOs);
    }
}
