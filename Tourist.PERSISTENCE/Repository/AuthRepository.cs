using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> RegisterAsync(ApplicationUser user, string Password)
        {
            var result = await _userManager.CreateAsync(user,Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, "user");
                return "User registered successfully. Check you email to confirm";
            }
            else
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
