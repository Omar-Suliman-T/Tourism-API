public class AuthRepository : IAuth
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWTDTOs _jwt;
    private readonly IEmailSender _emailSender;

    public AuthRepository(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IOptions<JWTDTOs> jwt)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _jwt = jwt.Value;
    }

    public async Task<string> RegisterAsync(ApplicationUser user, string Password)
    {
        var result = await _userManager.CreateAsync(user, Password);
        if (result.Succeeded)
        {
            return "User registered successfully. Check your email to confirm.";
        }

        throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

    // ---------------- Forget Password ----------------

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

        var callBack = QueryHelpers.AddQueryString(dto.ClientUri!, param);

        var html = $@"<html> ... HTML ... </html>";

        var message = new Message([dto.Email!], "Reset your Password", html);

        await _emailSender.SendEmailAsync(message);

        return "Email was sent successfully";
    }

    public async Task<string> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
    {
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
