using Tourist.DOMAIN.model;

namespace Tourist.API.ApiDTOs.Review
{
    public class UpdateReviewApiDTOs
    {
        public int ReviewId { get; set; }
        public Rating? Rating { get; set; }
        public string? Comment { get; set; }
        public IFormFile? Image { get; set; }
    }
}
