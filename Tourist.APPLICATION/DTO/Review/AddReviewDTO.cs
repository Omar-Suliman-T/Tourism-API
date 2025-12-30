using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.DTO.Review
{
    public class AddreviewDTO
    {
        public int ReviewId { get; set; }
        public Rating Rating { get; set; }
        public string? Comment { get; set; }
        public int TripId { get; set; }
        public string? UserId { get; set; }
    }
}
