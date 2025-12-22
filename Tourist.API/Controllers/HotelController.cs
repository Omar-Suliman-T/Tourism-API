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
        

        [HttpGet("get/{Id:int}")]
        public async Task<ActionResult> GetByIdAsync([FromServices]GetHotelUseCase getHotelUseCase,int Id)
        {
            var hotel = await getHotelUseCase.ExecuteAsync(Id);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync([FromServices] GetAllHotelUseCase getAllHotelUseCase)
        {
            var hotel = await getAllHotelUseCase.ExecuteAsync();
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }
        [HttpPost("add")]
        public async Task<ActionResult<string>> AddHotelAsync([FromServices]AddHotelUseCase addHotelUseCase,[FromBody] AddHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hotel = await addHotelUseCase.ExecuteAsync(hotelDTO);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }
        [HttpPut("update")]
        public async Task<ActionResult<string>> UpdateHotelAsync([FromServices]UpdateHotelUseCase updateHotelUseCase,[FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hotel = await updateHotelUseCase.ExecuteAsync(hotelDTO);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }
        [HttpDelete("delete/{Id:int}")]
        public async Task<ActionResult<string>> DeleteHotelAsync([FromServices]DeleteHotelUseCase deleteHotelUseCase,[FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hotel = await deleteHotelUseCase.ExecuteAsync(Id);
            return StatusCode((int)hotel.Item1, hotel.Item2);
        }

    }
}
