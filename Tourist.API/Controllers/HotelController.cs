using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.UseCase.Hotel;
using Tourist.DOMAIN.model;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        // ================= CREATE =================
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel, [FromServices]CreateHotelUseCase createHotel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await createHotel.ExcuteAsync(hotel);
            return Ok("Hotel created successfully");
        }

        // ================= GET ALL =================
        [HttpGet]
        public async Task<IActionResult> GetAllHotels([FromServices] GetHotelUseCase getHotel)
        {
            var hotels = await getHotel.ExcuteAsync();
            return Ok(hotels);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id, [FromServices] GetByIdHotelUseCase getHotel)
        {
            var hotel = await getHotel.ExcuteAsync(id);

            if (hotel == null)
                return NotFound("Hotel not found");

            return Ok(hotel);
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] Hotel hotel, [FromServices] UpdateHotelUseCase updateHotel)
        {
            var result = await updateHotel.ExcuteAsync(id, hotel);

            if (!result)
                return NotFound("Hotel not found");

            return Ok("Hotel updated successfully");
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id, [FromServices]  DeleteHotelUseCase deletetHotel)
        {
            var result = await deletetHotel.ExcuteAsync(id);

            if (!result)
                return NotFound("Hotel not found");

            return Ok("Hotel deleted successfully");
        }
        //  NEAR ME
        [HttpGet("near")]
        public async Task<IActionResult> GetNearHotels([FromServices]GetNearHotelUseCase _hotelUseCase,
            [FromQuery] double lat,
            [FromQuery] double lng,
            [FromQuery] double distanceKm = 5)
        {
            var hotels = await _hotelUseCase
                .GetHotelsNearAsync(lat, lng, distanceKm);

            return Ok(hotels);
        }
    }

}

