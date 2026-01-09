using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Favourite;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IFavouriteRepository
    {
        Task AddHotelToFavouriteAsync(FavouriteHotels entity);
        Task RemoveHotelFromFavouriteAsync(int favouriteHotelId);
        Task<IEnumerable<FavouriteHotels>> GetFavouriteHotelsByUserAsync(string userId);
    }
}