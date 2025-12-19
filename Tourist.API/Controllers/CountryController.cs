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
        private readonly CountryUseCase _CountryUseCase;
        public CountryController(CountryUseCase CountryUseCase)
        {
            _CountryUseCase = CountryUseCase;
        }

        [HttpGet("get/{Id:int}")]
        public async Task<ActionResult> GetByIdAsync(int Id)
        {
            var Country = await _CountryUseCase.GetCountryByIdAsync(Id);
            return StatusCode((int)Country.Item1, Country.Item2);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var Country = await _CountryUseCase.GetAllCountryAsync();
            return StatusCode((int)Country.Item1, Country.Item2);
        }
        [HttpPost("add")]
        public async Task<ActionResult<string>> AddCountryAsync([FromBody] AddCountryDTO CountryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Country = await _CountryUseCase.AddCountryAsync(CountryDTO);
            return StatusCode((int)Country.Item1, Country.Item2);
        }
        [HttpPut("update")]
        public async Task<ActionResult<string>> AddCountryAsync([FromBody] UpdateCountryDTO CountryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Country = await _CountryUseCase.UpateCountryAsync(CountryDTO);
            return StatusCode((int)Country.Item1, Country.Item2);
        }
        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult<string>> DeleteCountryAsync([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Country = await _CountryUseCase.DeleteCountryAsync(Id);
            return StatusCode((int)Country.Item1, Country.Item2);
        }
    }
}
