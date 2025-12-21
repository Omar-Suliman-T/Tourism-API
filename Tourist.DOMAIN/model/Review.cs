using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public enum Rating
    {
        VeryBad = 1,
        Bad = 2,
        Average = 3,
        Good = 4,
        Excellent = 5
    }
    public class Review: BaseEntity
    {
        public int ReviewId { get; set; }
        public Rating Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public Trip? Trip { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
