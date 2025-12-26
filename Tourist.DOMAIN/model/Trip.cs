using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public enum TripStatus
    {
        Pending = 1,
        Confirmed = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5
    }
    public class Trip: BaseEntity
    {
        public string Name { get; set; }
        public int TripId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TripStatus? Status { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("City")]
        public int CityId { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public City? City { get; set; }
        public Hotel? Hotel { get; set; }
        public ApplicationUser? User { get; set; }
        public IEnumerable<TripActivity>? Activities { get; set; }
        public IEnumerable<Payment>? Payments { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }

    }
}
