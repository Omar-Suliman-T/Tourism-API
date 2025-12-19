using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Country;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Country
{
    public class CountryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(HttpStatusCode, string)> AddCountryAsync(AddCountryDTO countryDTO)
        {
            var country = new DOMAIN.model.Country()
            {
                Name = countryDTO.Name
            };
            await _unitOfWork.Country.AddAsync(country);
            await _unitOfWork.SaveChangesAsync();

            return (HttpStatusCode.OK, "Country Added Successfully");
        }
        public async Task<(HttpStatusCode, string)> UpateCountryAsync(UpdateCountryDTO countryDTO)
        {
            var country = await _unitOfWork.Country.GetAsync(c => c.CountryId == countryDTO.Id && c.IsDeleted == false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, "Country Not Found");
            }
            country.Name=countryDTO.Name;
            await _unitOfWork.SaveChangesAsync();

            return (HttpStatusCode.OK, "Country Updated Successfully");
        }
        public async Task<(HttpStatusCode, string)> DeleteCountryAsync(int Id)
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
        public async Task<(HttpStatusCode, DOMAIN.model.Country)> GetCountryByIdAsync(int Id)
        {
            var country = await _unitOfWork.Country.GetAsync(c => c.CountryId == Id&&c.IsDeleted==false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, null);
            }

            return (HttpStatusCode.OK, country);
        }
        public async Task<(HttpStatusCode,List< DOMAIN.model.Country>)> GetAllCountryAsync()
        {
            var country = await _unitOfWork.Country.GetAllAsync(c=>c.IsDeleted==false);
            if (country == null)
            {
                return (HttpStatusCode.NotFound, new List<DOMAIN.model.Country>());
            }

            return (HttpStatusCode.OK, country.ToList());
        }
    }
}
