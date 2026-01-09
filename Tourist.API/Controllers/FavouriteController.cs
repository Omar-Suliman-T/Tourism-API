using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.Favourite;
using Tourist.APPLICATION.UseCase.Favourite;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly FavouriteUseCase _favouriteUseCase;

        public FavouriteController(FavouriteUseCase favouriteUseCase)
        {
            _favouriteUseCase = favouriteUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddHotelToFavourite(AddHotelToFavouriteDTOs dto)
        {
            await _favouriteUseCase.AddHotelToFavouriteAsync(dto);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserFavouriteHotels(string userId)
        {
            var result = await _favouriteUseCase.GetUserFavouriteHotelsAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{favouriteHotelId:int}")]
        public async Task<IActionResult> RemoveHotelFromFavourite(int favouriteHotelId)
        {
            await _favouriteUseCase.RemoveHotelFromFavouriteAsync(favouriteHotelId);

            return NoContent();
        }
    }
}
