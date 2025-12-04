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

        public async Task<(HttpStatusCode, string)> ChangePasswodAsync(ClaimsPrincipal claims, ChangePasswordRequestDTO request)
        {
            
            try
            {
                var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return (HttpStatusCode.BadRequest, "User not authenticated");
                }
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return (HttpStatusCode.BadRequest, "User Not ound");
                }
                var isChecked = await _userManager.CheckPasswordAsync(user!, request.CurrentPassword);
                if (!isChecked)
                {
                    return (HttpStatusCode.BadRequest, "Password is wrong");
                }
                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (result.Succeeded)
                {
                    return (HttpStatusCode.OK, "Password Changed successfully");

                }
                else
                {
                    return (HttpStatusCode.BadRequest, "something is wrong");
                }
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
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
