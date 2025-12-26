using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public class Hotel: BaseEntity
    {
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public double PricePerNight { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City? City { get; set; }
        public IEnumerable<Trip>? Trips { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
    }
}
