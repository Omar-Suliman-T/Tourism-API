using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Monument
{
    public class GetMonumentUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMonumentUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, DOMAIN.model.Monument)> ExecuteAsync(int id)
        {

            var result = await _unitOfWork.Monument.GetAsync(m => m.Id == id);
            if (result == null)
            {
                return (HttpStatusCode.NotFound,null!);
            }
            return (HttpStatusCode.OK,result);
        }
    }
}
