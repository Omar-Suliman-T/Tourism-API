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
    public class GetTripsByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTripsByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTripsByIdDTO>> ExecuteAsunc(string userId)
        {
            var trips = await _unitOfWork.Trip.GetAllAsync(x => x.UserId == userId);
            var tripDTO = _mapper.Map<IEnumerable<GetTripsByIdDTO>>(trips);
            return tripDTO;
        }
    }
}
