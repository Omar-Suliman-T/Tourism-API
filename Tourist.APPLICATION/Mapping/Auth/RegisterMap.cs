using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Mapping.Auth
{
    public class RegisterMap
    {
        public ApplicationUser ToRegister(RegisterDTOs register)
        {
            var user = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
            };
            return user;
        }
    }
}
