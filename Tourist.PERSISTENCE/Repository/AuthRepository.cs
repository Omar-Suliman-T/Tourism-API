using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.Service.EmailService;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthRepository(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
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

        public async Task<string> ForgetPasswordAsync(ForgetPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email!);

            if (user is null)
                throw new Exception("There is no user with this Email");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var param = new Dictionary<string, string?>()
            {
                { "token", encodedToken },
                { "email", dto.Email! }
            };

             var callBack = QueryHelpers.AddQueryString(dto.ClientUri!,param);

            var html = $@"
            <!DOCTYPE html>
            <html>
            <body style='font-family: Arial; background:#f8f9fa; padding:20px;'>

                <div style='max-width:600px; margin:auto; background:white; padding:20px; border-radius:10px;'>

                    <h2 style='color:#333;'>Reset Your Password</h2>

                    <p style='font-size:16px;'>Click the button below to reset your password:</p>

                    <a href='{callBack}' 
                        style='display:inline-block;
                               margin-top:20px;
                               padding:12px 25px;
                               background:#007bff;
                               color:white;
                               text-decoration:none;
                               border-radius:5px;
                               font-size:16px;'>
                         Reset Password
                    </a>

                    <p style='margin-top:30px; color:#666;'>
                        If you didn't request this email, you can ignore it.
                    </p>

                    <hr>
                    <p style='font-size:14px; color:#999;'>© 2025 Tourist App</p>

                </div>
            </body>
            </html>";


            var message = new Message([dto.Email!], "Reset your Password", html);

            try
            {
                await _emailSender.SendEmailAsync(message);
            }
            catch (Exception ex)
            {

                throw new Exception("Email Couldn't be sent",ex);
            }
            return "Email was sent successfully";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email!);

            if(user is null)
                throw new Exception("There is no user with this Email");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDTO.Token!));
            
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDTO.Password!);

            if (result.Succeeded)
            {
                return "Password has been reset successfully";
            }
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}
