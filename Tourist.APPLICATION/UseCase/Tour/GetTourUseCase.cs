using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Tour
{
    public class GetTourUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTourUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode,DOMAIN.model.Tour)> ExecuteAsync(int id)
        {
            var tour = await _unitOfWork.Tour.GetAsync(t => t.Id == id);
            if (tour == null)
            {
                return (HttpStatusCode.NotFound, null);
            }
            return (HttpStatusCode.OK, tour);
        }
    }
}
