using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.City;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // api/city
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _unitOfWork.City.GetAllAsync(c => true,
                includeProperities: "Country"
            );
           

            return Ok(cities);
        }

        // api/city/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _unitOfWork.City.GetAsync(
                c => c.CityId == id,
                includeProperities: "Country,Hotels,Places,Trips"
            );

            if (city == null)
                return NotFound("City not found");

            return Ok(city);
        }

        // api/city/by-country/3
        [HttpGet("by-country/{countryId:int}")]//is this url right ?
        public async Task<IActionResult> GetByCountry(int countryId)
        {
            var cities = await _unitOfWork.City.GetAllAsync(
                c => c.CountryId == countryId,
                includeProperities: "Country"
            );

            return Ok(cities);
        }

        // api/city
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = new City {
                Name = dto.Name,
                CountryId = dto.CountryId
                
            };

            await _unitOfWork.City.AddAsync(city);
            await _unitOfWork.SaveChangesAsync();

            var result = new CityDTO
            {
                CityId = city.CityId,
                Name = city.Name,
                CountryId = city.CountryId,
                CountryName = (await _unitOfWork.Country.GetAsync(c => c.CountryId == city.CountryId))?.Name

            };


            return CreatedAtAction(
                nameof(GetById),
                new { id = city.CityId },
                result
            );
        }
        // api/city/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] City city)
        {
            if (id != city.CityId)
                return BadRequest("Id mismatch");

            var cityFromDb = await _unitOfWork.City
                .GetAsync(c => c.CityId == id);

            if (cityFromDb == null)
                return NotFound("City not found");

            cityFromDb.Name = city.Name;
            cityFromDb.CountryId = city.CountryId;


            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        // api/city/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _unitOfWork.City
                .GetAsync(c => c.CityId == id);

            if (city == null)
                return NotFound("City not found");

            await _unitOfWork.City.RemoveAsync(city);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
