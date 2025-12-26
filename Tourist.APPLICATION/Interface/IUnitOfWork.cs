using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
//using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAuth Auth { get; }
        IUser User { get; }
        ICountryRepository Country{get;} 
        ICityRepository City{get;} 
        IPlaceRepository Place{get;}    
        IHotelRepository Hotel{get;}      
        ITripRepository Trip{get;}      
        IPaymentRepository Payment{get;}    
        IReviewRepository Review { get; }
        INotificationRepository Notification{get;}
        Task<int> CompleteAsync();
        public Task SaveChangesAsync();
    }
}
