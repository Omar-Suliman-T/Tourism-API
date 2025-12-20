using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Review;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Review
{
    public class AddReviewUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddReviewUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(AddreviewDTO addreviewDTO)
        {
            ArgumentNullException.ThrowIfNull(addreviewDTO);

            var review = _mapper.Map<Tourist.DOMAIN.model.Review>(addreviewDTO);

            await _unitOfWork.Review.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
