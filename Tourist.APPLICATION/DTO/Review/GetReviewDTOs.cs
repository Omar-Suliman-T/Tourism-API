using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.DTO.Review
{
    public class GetReviewDTOs
    {
        public int ReviewId { get; set; }
        public string RatingText { get; set; }
        public string? Comment { get; set; }
        public List<string>? WhatWasGreat { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? imageUrl { get; set; }

        public string? TripName { get; set; }

        public string? HotelName { get; set; }

        public string UserId { get; set; }
    }
}
