using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Tour
{
    public class DeleteTourUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        public DeleteTourUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode,string)> ExecuteAsync(int id)
        {
            var tour = await _unitOfWork.Tour.GetAsync(t=>t.Id==id);
            if (tour == null) return (HttpStatusCode.NotFound,"Tour Not Found");
            tour.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return (HttpStatusCode.OK, "Tour Deleted Successfully");
        }
    }
}
