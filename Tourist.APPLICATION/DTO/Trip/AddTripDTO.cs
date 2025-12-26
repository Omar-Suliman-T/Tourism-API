using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.DTO.Trip
{
    public class AddTripDTO
    {
        public double TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TripStatus? Status { get; set; }
        public string? UserId { get; set; } 
        public int CityId { get; set; }
        public int HotelId { get; set; }
    }
}
