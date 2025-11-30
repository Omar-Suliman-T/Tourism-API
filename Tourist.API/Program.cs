
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Tourist.API.Middleware;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.Mapping.Auth;
using Tourist.APPLICATION.Service.EmailService;
using Tourist.APPLICATION.UseCase.Auth;
using Tourist.DOMAIN.model;
using Tourist.PERSISTENCE;
using Tourist.PERSISTENCE.Repository;

namespace Tourist.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var EmailConfig = builder.Configuration.GetSection("EmailConfiguration")
                .Get<EmailCofiguration>();
            builder.Services.AddSingleton(EmailConfig);
            builder.Services.AddScoped<IEmailSender,EmailSender>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<RegisterUseCase>();
            builder.Services.AddScoped<ForgetPasswordUseCase>();
            builder.Services.AddScoped<ResetPasswordUseCase>();
            builder.Services.AddScoped<RegisterMap>();

            var ConnectionString = builder.Configuration.GetConnectionString("Tour");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(ConnectionString));

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                options.User.RequireUniqueEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDataProtection();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
