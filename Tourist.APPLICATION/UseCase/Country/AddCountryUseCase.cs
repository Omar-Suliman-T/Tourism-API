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
    public class AddCountryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddCountryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, string)> ExecuteAsync(AddCountryDTO countryDTO)
        {
            var country = new DOMAIN.model.Country()
            {
                Name = countryDTO.Name
            };
            await _unitOfWork.Country.AddAsync(country);
            await _unitOfWork.SaveChangesAsync();

            return (HttpStatusCode.OK, "Country Added Successfully");
        }
    }
}
