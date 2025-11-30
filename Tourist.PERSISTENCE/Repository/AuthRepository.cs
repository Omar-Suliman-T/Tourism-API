using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly JWTDTOs _jwt;
        public AuthRepository(UserManager<ApplicationUser> userManager, IOptions<JWTDTOs> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
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

        public async Task<AuthDTOs> LoginAsync(LoginDTOs loginDTOs)
        {
            var user = await _userManager.FindByEmailAsync(loginDTOs.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTOs.Password))
                return new AuthDTOs { Message = "Invalid Email or Password!" };

            return await GenerateTokenAsync(user);
        }
        private async Task<AuthDTOs> GenerateTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
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

