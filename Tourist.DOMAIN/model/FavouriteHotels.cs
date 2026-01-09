using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public class FavouriteHotels
    {
        [Key]
        public int FavouriteHotelId { get; set; }

        [Required]
        public string UserId { get; set; } 

        [Required]
        public int HotelId { get; set; }

        public ApplicationUser User { get; set; }
        public Hotel Hotel { get; set; }
    }
}