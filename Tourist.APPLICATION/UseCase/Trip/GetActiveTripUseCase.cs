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
    public class GetActiveTripUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetActiveTripUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTripsByIdDTO> ExecuteAsync(string userId)
        {
            var trip = await _unitOfWork.Trip.GetActiveTripByIdAsync(userId);
            if (trip == null) throw new ArgumentNullException(nameof(trip));

            return _mapper.Map<GetTripsByIdDTO>(trip);            
        }
    }
}
