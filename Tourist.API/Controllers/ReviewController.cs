using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourist.APPLICATION.DTO.Review;
using Tourist.APPLICATION.UseCase.Review;
using Tourist.APPLICATION.UseCase.Trip;

namespace Tourist.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddReview(
            [FromServices] AddReviewUseCase addReviewUseCase,
            [FromBody] AddreviewDTO addreviewDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await addReviewUseCase.ExecuteAsync(addreviewDTO);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReview(
            [FromServices] GetAllReviewsUseCase getAllReviewsUseCase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await getAllReviewsUseCase.ExecuteAsync(userId!);

            return Ok(result);
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> RemoveReview(
            [FromServices] RemoveReviewUseCase removeReviewUseCase,
            [FromRoute] int reviewId)
        {
            await removeReviewUseCase.ExecuteAsync(reviewId);
            return Ok();
        }
    }
}
