using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Trip;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Trip
{
    public class AddTripUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddTripUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(AddTripDTO addTripDTO)
        {
            ArgumentNullException.ThrowIfNull(addTripDTO);

            var trip = _mapper.Map<Tourist.DOMAIN.model.Trip>(addTripDTO);

            await _unitOfWork.Trip.AddAsync(trip);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
