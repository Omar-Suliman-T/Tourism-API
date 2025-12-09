using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        private readonly JWTDTOs _jwt;
        private readonly IEmailSender _emailSender;

        public AuthRepository(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IOptions<JWTDTOs> jwt)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _jwt = jwt?.Value ?? throw new ArgumentNullException(nameof(jwt));
        }
        public async Task<(HttpStatusCode, string)> ChangePasswodAsync(ClaimsPrincipal claims, ChangePasswordRequestDTO request)
        {
            //var transaction = _context.Database.BeginTransaction();
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

        // ---------------- Register ----------------
        public async Task<string> RegisterAsync(ApplicationUser user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password is required", nameof(password));

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return "User registered successfully. Check your email to confirm.";
            }

            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        // ---------------- Change Password ----------------
        public async Task<(HttpStatusCode, string)> ChangePasswordAsync(ClaimsPrincipal claims, ChangePasswordRequestDTO request)
        {

            try
            {
                if (claims == null) return (HttpStatusCode.BadRequest, "Invalid user claims");
                if (request == null) return (HttpStatusCode.BadRequest, "Invalid request");

                var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return (HttpStatusCode.BadRequest, "User not authenticated");

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return (HttpStatusCode.BadRequest, "User not found");

                var isChecked = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
                if (!isChecked)
                    return (HttpStatusCode.BadRequest, "Current password is incorrect");

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (result.Succeeded)
                    return (HttpStatusCode.OK, "Password changed successfully");

                return (HttpStatusCode.BadRequest, string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // ---------------- Forget Password ----------------
        public async Task<string> ForgetPasswordAsync(ForgetPasswordDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email is required", nameof(dto.Email));
            if (string.IsNullOrWhiteSpace(dto.ClientUri)) throw new ArgumentException("ClientUri is required", nameof(dto.ClientUri));

            var user = await _userManager.FindByEmailAsync(dto.Email!);
            if (user is null)
                throw new Exception("There is no user with this Email");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var param = new Dictionary<string, string?>
            {
                { "token", encodedToken },
                { "email", dto.Email! }
            };

            var callBack = QueryHelpers.AddQueryString(dto.ClientUri!, param);

            // You can replace the html below with your real template
            var html = $@"<html>
                                <body>
                                    <p>Reset your password by clicking the link below:</p>
                                    <a href=""{callBack}"">Reset Password</a>
                                </body>
                            </html>";

            var message = new Message(new[] { dto.Email! }, "Reset your Password", html);

            await _emailSender.SendEmailAsync(message);

            return "Email was sent successfully";
        }

        // ---------------- Reset Password ----------------
        public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            if (resetPasswordDTO == null) throw new ArgumentNullException(nameof(resetPasswordDTO));
            if (string.IsNullOrWhiteSpace(resetPasswordDTO.Email)) throw new ArgumentException("Email is required", nameof(resetPasswordDTO.Email));
            if (string.IsNullOrWhiteSpace(resetPasswordDTO.Token)) throw new ArgumentException("Token is required", nameof(resetPasswordDTO.Token));

            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email!);
            if (user is null)
                throw new Exception("There is no user with this Email");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDTO.Token!));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDTO.Password!);

            if (result.Succeeded)
                return "Password has been reset successfully";

            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        // ---------------- Login ----------------
        public async Task<AuthDTOs> LoginAsync(LoginDTOs loginDTOs)
        {
            if (loginDTOs == null) throw new ArgumentNullException(nameof(loginDTOs));
            if (string.IsNullOrWhiteSpace(loginDTOs.Email) || string.IsNullOrWhiteSpace(loginDTOs.Password))
                return new AuthDTOs { Message = "Invalid Email or Password!" };

            var user = await _userManager.FindByEmailAsync(loginDTOs.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTOs.Password))
                return new AuthDTOs { Message = "Invalid Email or Password!" };

            return await GenerateTokenAsync(user);
        }

        // ---------------- Generate JWT ----------------
        private async Task<AuthDTOs> GenerateTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: creds
            );

            return new AuthDTOs
            {
                Email = user.Email,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = roles.ToList(),
                IsAuthenticated = true,
                ExpiresOn = token.ValidTo
            };
        }

     
    }
}
