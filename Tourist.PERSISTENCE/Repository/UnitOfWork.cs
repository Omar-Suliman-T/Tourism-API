using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ApplicationDbContext _context;
        //private readonly ITripRepository Trip { get; private set; }
        public ICountryRepository Country { get; private set; }
        public ICityRepository City { get; private set; }
        public IPlaceRepository Place { get; private set; }
        public IHotelRepository Hotel { get; private set; }
        public ITripRepository Trip { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IReviewRepository Review { get; private set; }

        public ITourRepository Tour { get; private set; }
        public IMonumentRepository Monument { get; private set; }

        public IAuth Auth { get; private set; }
        public IUser User { get; private set; }

        public UnitOfWork(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IConfiguration configuration,

            IOptions<JWTDTOs> jwt,
            ILogger<UnitOfWork>logger,
            ILoggerFactory loggerFactory,
            ApplicationDbContext context
            //IRepository<Trip> repository,
            //IReviewRepository review
            )
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _jwt = jwt.Value;
            _logger = logger;
            _loggerFactory = loggerFactory;
            _context = context;
            //_repository = repository;
            _context = context;
            //Review = review;

            // إنشاء AuthRepository بشكل صحيح
            Country = new CountryRepository(_context);
            City = new CityRepository(_context);
            Place = new PlaceRepository(_context);
            Hotel = new HotelRepository(_context);
            Payment = new PaymentRepository(_context);
            Tour = new TourRepository(_context);
            Monument = new MonumentRepository(_context);
            User = new UserRepository(_context);
            Trip = new TripRepository(_context);
            Review = new ReviewRepository(_context);


        }


        public async Task SaveChangesAsync()
        {
          await _context.SaveChangesAsync();  
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
