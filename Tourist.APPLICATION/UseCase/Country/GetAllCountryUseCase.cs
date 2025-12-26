using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Country;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Country
{
    public class GetAllCountryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCountryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(HttpStatusCode, List<DOMAIN.model.Country>)> ExecuteAsync()
        {
            var country = await _unitOfWork.Country.GetAllAsync(c => c.IsDeleted == false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, new List<DOMAIN.model.Country>());
            }

            return (HttpStatusCode.OK, country.ToList());
        }
    }
}
