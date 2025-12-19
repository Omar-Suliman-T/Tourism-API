using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.UseCase.Auth;
using System.Net;

namespace Tourist.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDTOs registerDTOs,
            [FromServices] RegisterUseCase registerUseCase)
        {
            var result = await registerUseCase.ExecuteAsync(registerDTOs);
            return Ok(result);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(
            [FromBody] ConfirmEmailDTO confirmEmailDTO,
            [FromServices] ConfirmEmailUseCase confirmEmailUseCase)
        {
            var result = await confirmEmailUseCase.ExecuteAsync(confirmEmailDTO);
            return Ok(result);
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleAuth(
            [FromBody] GoogleLoginDTO googleLoginDTO,
            [FromServices] GoogleAuthUseCase googleAuthUseCase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await googleAuthUseCase.ExcuteAsync(googleLoginDTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);

        }

        [HttpPost("Change-Password")]
        public async Task<ActionResult<string>> ChangePassword(
            [FromServices] ChangePasswordUseCase changePasswordUseCase,
            [FromBody] ChangePasswordRequestDTO changePasswordRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await changePasswordUseCase.ExecuteAsync(User, changePasswordRequestDTO);

            return StatusCode((int)result.Item1, result.Item2);
        }

        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgetPassword(
            [FromBody] ForgetPasswordDTO forgetPasswordDTO,
            [FromServices] ForgetPasswordUseCase forgetPasswordUseCase)
        {
            var result = await forgetPasswordUseCase.ExecuteAsync(forgetPasswordDTO);
            return Ok(result);
        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordDTO resetPasswordDTO,
            [FromServices] ResetPasswordUseCase resetPasswordUseCase)
        {
            var result = await resetPasswordUseCase.ExecuteAsync(resetPasswordDTO);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginDTOs model,
            [FromServices] LoginUseCase loginUseCase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await loginUseCase.ExecuteAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
