using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class GetNearHotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetNearHotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // ================= Get Near Hotel UseCase =================
        public async Task<IEnumerable<Tourist.DOMAIN.model.Hotel>> GetHotelsNearAsync(
            double userLat,
            double userLng,
            double maxDistanceKm = 5)
        {
            var hotels = await _unitOfWork.Hotel.GetAllAsync(C=>true);

            return hotels.Where(h =>
                CalculateDistanceKm(
                    userLat, userLng,
                    h.Latitude, h.Longitude
                ) <= maxDistanceKm
            );
        }

        // Distance Calculation (Haversine)
        private double CalculateDistanceKm(
            double lat1, double lon1,
            double lat2, double lon2)
        {
            const double R = 6371; // Earth radius (km)

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) *
                Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double DegreesToRadians(double deg)
            => deg * (Math.PI / 180);
    }
}

