using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Review;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Trip
{
    public class GetAllReviewsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllReviewsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetReviewByIdDTO>> ExecuteAsync(string userId)
        {
            var review = await _unitOfWork.Review.GetAllAsync(x => x.UserId == userId);
            if (review == null) throw new ArgumentNullException(nameof(review));

            return _mapper.Map<IEnumerable<GetReviewByIdDTO>>(review);            
        }
    }
}
