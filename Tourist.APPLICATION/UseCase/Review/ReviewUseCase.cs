using Tourist.APPLICATION.DTO.Review;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;


namespace Tourist.APPLICATION.UseCase.Review
{
    public class ReviewUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetReviewDTOs>> GetAllAsync()
        {
            var reviews = await _unitOfWork.Review.GetAllWithDetailsAsync();

            return reviews.Select(r => new GetReviewDTOs
            {
                ReviewId = r.ReviewId,
                RatingText = r.Rating.ToString(),
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                imageUrl = r.imageUrl,
                HotelName = r.Hotel != null ? r.Hotel.Name : null,
                TripName = r.Trip != null ? r.Trip.Name : null,
                UserId = r.UserId
            });
        }

        public async Task<GetReviewDTOs?> GetByIdAsync(int id)
        {
            var r = await _unitOfWork.Review.GetByIdWithDetailsAsync(id);
            if (r == null || r.IsDeleted)
                return null;

            return new GetReviewDTOs
            {
                ReviewId = r.ReviewId,
                RatingText = r.Rating.ToString(),
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                imageUrl = r.imageUrl,
                HotelName= r.Hotel != null ? r.Hotel.Name : null,
                TripName = r.Trip != null ? r.Trip.Name : null,
                UserId = r.UserId
            };
        }

        public async Task<int> CreateAsync(CreateReviewDTOs dto)
        {
            var review = new DOMAIN.model.Review
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                imageUrl = dto.image,
                TripId = dto.TripId,
                HotelId = dto.HotelId,
                UserId = dto.UserId,
            };

            await _unitOfWork.Review.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return review.ReviewId;
        }

        public async Task<bool> UpdateAsync(UpdateReviewDTOs dto)
        {
            var review = await _unitOfWork.Review.GetByIdAsync(dto.ReviewId);
            if (review == null || review.IsDeleted)
                return false;

            if (dto.Rating.HasValue)
                review.Rating = dto.Rating.Value;

            if (dto.Comment != null)
                review.Comment = dto.Comment;

            if (dto.imageUrl != null)
                review.imageUrl = dto.imageUrl;

            _unitOfWork.Review.Update(review);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _unitOfWork.Review.GetByIdAsync(id);
            if (review == null || review.IsDeleted)
                return false;

            await _unitOfWork.Review.SoftDeleteAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
