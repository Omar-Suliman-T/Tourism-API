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
        public async Task<IActionResult> Register([FromBody] RegisterDTOs registerDTOs, [FromServices] RegisterUseCase _registerUseCase)
        {
            var result = await _registerUseCase.ExecuteAsync(registerDTOs);
            return Ok(result);
        }
        [HttpPost("change-password")]
        public async Task<ActionResult<string>> changepassword([FromServices] ChangePasswordUseCase _changePasswordUseCase,ChangePasswordRequestDTO changePasswordRequestDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _changePasswordUseCase.ExecuteAsync(User,changePasswordRequestDTO);
            return StatusCode((int)result.Item1,result.Item2);
        }
    }
}
