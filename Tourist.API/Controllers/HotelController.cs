using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.Hotel;
using Tourist.APPLICATION.UseCase.Hotel;
using Tourist.DOMAIN.model;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelUseCase _hotelUseCase;
        public HotelController(HotelUseCase hotelUseCase) 
        {
            _hotelUseCase=hotelUseCase;
        }

        [HttpGet("get/{Id:int}")]
        public async Task<ActionResult> GetByIdAsync(int Id)
        {
            var hotel = await _hotelUseCase.GetHotelByIdAsync(Id);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var hotel = await _hotelUseCase.GetAllHoteIdAsync();
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }
        [HttpPost("add")]
        public async Task<ActionResult<string>> AddHotelAsync([FromBody] AddHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hotel = await _hotelUseCase.AddHotelAsync(hotelDTO);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }
        [HttpPut("update")]
        public async Task<ActionResult<string>> AddHotelAsync([FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hotel = await _hotelUseCase.UpdateHotelAsync(hotelDTO);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }
        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult<string>> DeleteHotelAsync([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hotel = await _hotelUseCase.DeleteHotelAsync(Id);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }

    }
}
