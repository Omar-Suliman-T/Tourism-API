using Tourist.DOMAIN.model;

namespace Tourist.API.ApiDTOs.Review
{
    public class CreateReviewApiDTOs
    {
       
            public int UserId { get; set; }
            public Rating Rating { get; set; }
            public List<string>? WhatWasGreat { get; set; }
            public string? Comment { get; set; }
            public IFormFile? Image { get; set; }
            public int? TripId { get; set; }
            public int? HotelId { get; set; }
            
       
    
    }
}
