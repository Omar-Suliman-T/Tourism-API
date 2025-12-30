using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Mapping
{
    public class ApplicationAssemblyMarker
    {
        public int ReviewId { get; set; }
        public Rating Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? imageUrl { get; set; }

        public int? TripId { get; set; }

        public int? HotelId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
