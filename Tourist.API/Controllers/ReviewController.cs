using Microsoft.AspNetCore.Mvc;
using Tourist.API.ApiDTOs.Review;
using Tourist.API.Services.UploadService;
using Tourist.APPLICATION.DTO.Review;
using Tourist.APPLICATION.UseCase.Review;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewUseCase _reviewUseCase;
        private readonly UploadService _uploadService;

        public ReviewController(
            ReviewUseCase reviewUseCase,
            UploadService uploadService)
        {
            _reviewUseCase = reviewUseCase;
            _uploadService = uploadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewUseCase.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _reviewUseCase.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
            [FromForm] CreateReviewApiDTOs dto)
        {
            string? imageUrl = null;

            if (dto.Image != null)
                imageUrl = await _uploadService.UploadReviewImageAsync(dto.Image);

            var appDto = new CreateReviewDTOs
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                image = imageUrl,
                TripId = dto.TripId,
                HotelId = dto.HotelId,
                UserId = dto.UserId.ToString()
            };

            var id = await _reviewUseCase.CreateAsync(appDto);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(
            [FromForm] UpdateReviewApiDTOs dto)
        {
            string? imageUrl = null;

            if (dto.Image != null)
                imageUrl = await _uploadService.UploadReviewImageAsync(dto.Image);

            var appDto = new UpdateReviewDTOs
            {
                ReviewId = dto.ReviewId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                imageUrl = imageUrl
            };

            var updated = await _reviewUseCase.UpdateAsync(appDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewUseCase.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
