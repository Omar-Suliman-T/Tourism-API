using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Country
{
    public class DeleteCountryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCountryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, string)> ExecuteAsync(int Id)
        {
            var country = await _unitOfWork.Country.GetAsync(c => c.CountryId == Id && c.IsDeleted == false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, "Country Not Found");
            }
            country.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();

            return (HttpStatusCode.OK, "Country Deleted Successfully");
        }
    }
}
