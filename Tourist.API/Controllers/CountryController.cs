using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.Country;
using Tourist.APPLICATION.UseCase.Country;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        [HttpGet("get/{Id:int}")]
        public async Task<ActionResult> GetByIdAsync([FromServices]GetCountryUseCase getCountryUseCase,int Id)
        {
            var Country = await getCountryUseCase.ExecuteAsync(Id);
            return StatusCode((int)Country.Item1, Country.Item2);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync([FromServices] GetAllCountryUseCase getAllCountryUseCase)
        {
            var Country = await getAllCountryUseCase.ExecuteAsync();
            return StatusCode((int)Country.Item1, Country.Item2);
        }
        [HttpPost("add")]
        public async Task<ActionResult<string>> AddCountryAsync([FromServices]AddCountryUseCase addCountryUseCase,[FromBody] AddCountryDTO CountryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Country = await addCountryUseCase.ExecuteAsync(CountryDTO);
            return StatusCode((int)Country.Item1, Country.Item2);
        }
        [HttpPut("update")]
        public async Task<ActionResult<string>> UpdateCountryAsync([FromServices]UpdateCountryUseCase updateCountryUseCase,[FromBody] UpdateCountryDTO CountryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Country = await updateCountryUseCase.ExecuteAsync(CountryDTO);
            return StatusCode((int)Country.Item1, Country.Item2);
        }
        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult<string>> DeleteCountryAsync([FromServices]DeleteCountryUseCase deleteCountryUseCase,[FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Country = await deleteCountryUseCase.ExecuteAsync(Id);
            return StatusCode((int)Country.Item1, Country.Item2);
        }
    }
}
