using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tourist.APPLICATION.DTO.TourDto;
using Tourist.APPLICATION.UseCase.Tour;
using Tourist.DOMAIN.model;

namespace Tourist.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {

        [HttpGet("getAll")]
        public async Task<ActionResult<List<Tour>>> GetAll([FromServices] GetAllToursUseCase _getAllTourUseCase)
            => Ok(await _getAllTourUseCase.ExecuteAsync());

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Tour>> GetById([FromServices] GetTourUseCase _getTourUseCase,int id)
        {
            var tour = await _getTourUseCase.ExecuteAsync(id);
            return StatusCode((int)tour.Item1,tour.Item2);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Tour>> Create([FromServices] AddTourUseCase _addTourUseCase, [FromBody] TourDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tour = await _addTourUseCase.ExecuteAsync(dto);
            return Ok(tour);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<(HttpStatusCode,Tour)>> Update([FromServices] UpdateTourUseCase _updateTourUseCase, int id, [FromBody] TourDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _updateTourUseCase.ExeucuteAsync(id, dto);
            return StatusCode((int)updated.Item1,updated.Item2);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<(HttpStatusCode,string)>> Delete([FromServices] DeleteTourUseCase _deleteTourUseCase,int id)
        {
            var tour = await _deleteTourUseCase.ExecuteAsync(id);
            return StatusCode((int)tour.Item1, tour.Item2);
        }
    }
}
