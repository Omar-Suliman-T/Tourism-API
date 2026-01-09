using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Favourite;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavouriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddHotelToFavouriteAsync(FavouriteHotels entity)
        {
            await _context.FavouriteHotels.AddAsync(entity);
        }

        public async Task RemoveHotelFromFavouriteAsync(int favouriteHotelId)
        {
           var favouriteHotel= await _context.FavouriteHotels.FirstOrDefaultAsync(f => f.FavouriteHotelId == favouriteHotelId);
            if (favouriteHotel != null)
                 _context.Remove(favouriteHotel);

        }

        public async Task<IEnumerable<FavouriteHotels>> GetFavouriteHotelsByUserAsync(string userId)
        {
            return await _context.FavouriteHotels
                .Include(f => f.Hotel)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }


    }
}
