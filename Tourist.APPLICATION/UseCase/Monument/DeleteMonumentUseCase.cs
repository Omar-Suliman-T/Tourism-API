using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Monument
{
    public class DeleteMonumentUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteMonumentUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode,string)> ExecuteAsync(int id)
        {
            var result = await _unitOfWork.Monument.GetAsync(m => m.Id == id);
            if (result == null)
            {
                return (HttpStatusCode.NotFound,"Monument Not Exists");
            }
            result.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return (HttpStatusCode.OK,"Monument is Deleted Successfully");
        }
    }
}
