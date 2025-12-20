using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.Service.EmailService;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly JWTDTOs _jwt;
        private readonly ApplicationDbContext _context;

        public ICountryRepository Country { get; private set; }
        public ICityRepository City { get; private set; }
        public IPlaceRepository Place { get; private set; }
        public IHotelRepository Hotel { get; private set; }
        public ITripRepository Trip { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IReviewRepository Review { get; private set; }


        public UnitOfWork(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IOptions<JWTDTOs> jwt,
            IConfiguration configuration,
            ApplicationDbContext context,
            ITripRepository trip,
            IReviewRepository review)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _context = context;
            _jwt = jwt.Value;

            // إنشاء AuthRepository بشكل صحيح
            Auth = new AuthRepository(_userManager, _emailSender, _configuration, Options.Create(_jwt));
            Country = new CountryRepository(_context);
            City = new CityRepository(_context);
            Place = new PlaceRepository(_context);
            Hotel = new HotelRepository(_context);
            Payment = new PaymentRepository(_context);


            _context = context;
            Trip = trip;
            Review = review;
        }

        public IAuth Auth { get; private set; }
        public IUser User { get; private set; }

       

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
          await _context.SaveChangesAsync();  
        }
    }
}
