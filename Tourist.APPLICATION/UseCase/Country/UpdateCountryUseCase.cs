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
    public class UpdateCountryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCountryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, string)> ExecuteAsync(UpdateCountryDTO countryDTO)
        {
            var country = await _unitOfWork.Country.GetAsync(c => c.CountryId == countryDTO.Id && c.IsDeleted == false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, "Country Not Found");
            }
            country.Name = countryDTO.Name;
            await _unitOfWork.SaveChangesAsync();

            return (HttpStatusCode.OK, "Country Updated Successfully");
        }

    }
}
