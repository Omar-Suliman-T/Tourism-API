using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Trip
{
    public class RemoveReviewUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveReviewUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int reviewId)
        {
             _unitOfWork.Review.SoftRmoveAsync(reviewId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
