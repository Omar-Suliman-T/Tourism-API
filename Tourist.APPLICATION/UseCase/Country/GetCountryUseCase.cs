using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Country
{
    public class GetCountryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCountryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, DOMAIN.model.Country)> ExecuteAsync(int Id)
        {
            var country = await _unitOfWork.Country.GetAsync(c => c.CountryId == Id && c.IsDeleted == false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, null);
            }

            return (HttpStatusCode.OK, country);
        }
    }
}
