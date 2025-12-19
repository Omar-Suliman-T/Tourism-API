using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.Hotel
{
    public class AddHotelDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public double PricePerNight { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? ImageUrl { get; set; }
        public int CityId { get; set; }
    }
}
