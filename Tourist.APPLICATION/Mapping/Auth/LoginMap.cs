using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Mapping.Auth
{
    public class LoginMap
    {
        public ApplicationUser ToLogin(LoginDTOs login)
        {
            var user = new ApplicationUser
            {
                Email = login.Email,
            };
            return user;
        }
    }
}
