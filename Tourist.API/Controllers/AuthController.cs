using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
