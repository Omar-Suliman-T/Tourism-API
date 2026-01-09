using Tourist.APPLICATION.DTO.Favourite;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Favourite
{
    public class FavouriteUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavouriteUseCase(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddHotelToFavouriteAsync(AddHotelToFavouriteDTOs dto)
        {
            
            var favourite = new FavouriteHotels
            {
                UserId = dto.userId,
                HotelId = dto.hotelId
            };
            await _unitOfWork.Favourite.AddHotelToFavouriteAsync(favourite);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveHotelFromFavouriteAsync(int favouriteId)
        {
             await _unitOfWork.Favourite.RemoveHotelFromFavouriteAsync(favouriteId);


            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetFavouriteHotelsDTOs>> GetUserFavouriteHotelsAsync(string userId)
        {
            var favourites = await _unitOfWork
                .Favourite.GetFavouriteHotelsByUserAsync(userId);

            return favourites.Select(f => new GetFavouriteHotelsDTOs
            {
                HotelId = f.HotelId,
                Name = f.Hotel.Name,
                Description = f.Hotel.Description,
                Stars = f.Hotel.Stars,
                PricePerNight = f.Hotel.PricePerNight,
                Address = f.Hotel.Address,
                Phone = f.Hotel.Phone,
                ImageUrl = f.Hotel.ImageUrl
            });
        }
    }
}
