using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.UseCase.Auth;

namespace Tourist.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDTOs registerDTOs, 
            [FromServices] RegisterUseCase _registerUseCase)
        {
            var result = await _registerUseCase.ExecuteAsync(registerDTOs);
            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<string>> ChangePassword(
            [FromServices] ChangePasswordUseCase _changePasswordUseCase,
            [FromBody] ChangePasswordRequestDTO changePasswordRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _changePasswordUseCase.ExecuteAsync(User, changePasswordRequestDTO);
            return StatusCode((int)result.Item1, result.Item2);
        }

        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgetPassword(
            [FromBody] ForgetPasswordDTO forgetPasswordDTO, 
            [FromServices] ForgetPasswordUseCase forgetPasswordUseCase)
        {
            var result = await forgetPasswordUseCase.ForgetPassword(forgetPasswordDTO);
            return Ok(result);
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordDTO resetPasswordDTO, 
            [FromServices] ResetPasswordUseCase resetPasswordUseCase)
        {
            var result = await resetPasswordUseCase.ResetPassword(resetPasswordDTO);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginDTOs model, 
            [FromServices] LoginUseCase _loginUseCase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginUseCase.ExecuteAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
