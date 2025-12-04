using Microsoft.AspNetCore.Identity;
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


        public UnitOfWork(UserManager<ApplicationUser> userManager, IEmailSender emailSender,IOptions<JWTDTOs> jwt) { 
            _userManager = userManager;
            _emailSender = emailSender;
            Auth = new AuthRepository(_userManager, _emailSender, jwt);

        }
        public IAuth Auth { get; private set; }

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
