using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tourist.APPLICATION.DTO.Monument;
using Tourist.APPLICATION.UseCase.Monument;
using Tourist.DOMAIN.model;


namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonumentController : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<ActionResult<Monument>> AddAsync([FromServices] AddMonumentUseCase addMonumentUseCase, MonumentDto monumentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await addMonumentUseCase.ExecuteAsync(monumentDto);
            return Ok(result);
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<List<Monument>>> GetAllAsync([FromServices] GetAllMonumentsUseCase getAllMonumentsUseCase)
        {
            var result = await getAllMonumentsUseCase.ExecuteAsync();
            return Ok(result.ToList());
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<(HttpStatusCode,Monument)>> GetByIdAsync([FromServices] GetMonumentUseCase getMonumentUseCase,int id)
        {
            var result = await getMonumentUseCase.ExecuteAsync(id);
            return StatusCode((int)result.Item1,result.Item2);
        }
        [HttpDelete("remove/{id:int}")]
        public async Task<ActionResult<(HttpStatusCode,string)>> RemoveAsync([FromServices] DeleteMonumentUseCase deleteMonumentUseCase,int id)
        {
            var result = await deleteMonumentUseCase.ExecuteAsync(id);
            return StatusCode((int)result.Item1,result.Item2);
        }
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<(HttpStatusCode, Monument)>> UpdateAsync([FromServices] UpdateMonumentUseCase updateMonumentUseCase,int id , MonumentDto monumentDto)
        {
            var result = await updateMonumentUseCase.ExecuteAsync(id, monumentDto);
            return StatusCode((int)result.Item1,result.Item2);
        }
    }
}
