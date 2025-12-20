
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;
using Tourist.API.Middleware;
using Tourist.APPLICATION.DTO.Auth;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.Mapping;
using Tourist.APPLICATION.Mapping.Auth;
using Tourist.APPLICATION.Service.EmailService;
using Tourist.APPLICATION.UseCase.Auth;
using Tourist.APPLICATION.UseCase.Review;
using Tourist.APPLICATION.UseCase.Trip;
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
            builder.Services.AddScoped <ConfirmEmailUseCase>();
            builder.Services.AddScoped<GoogleAuthUseCase>();
            builder.Services.AddScoped<RegisterMap>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


            builder.Services.AddScoped<ChangePasswordUseCase>();
            builder.Services.AddScoped<AddReviewUseCase>();
            builder.Services.AddScoped<GetAllReviewsUseCase>();
            builder.Services.AddScoped<RemoveReviewUseCase>();
            builder.Services.AddScoped<GetTripsByIdUseCase>();
            builder.Services.AddScoped<AddTripUseCase>();
            builder.Services.AddScoped<GetActiveTripUseCase>();
            builder.Services.AddScoped<RemoveTripUseCase>();



            builder.Services.AddScoped<LoginUseCase>();
            builder.Services.AddScoped<LoginMap>();


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

            builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationAssemblyMarker).Assembly);

            builder.Services.Configure<JWTDTOs>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAuthentication(options => {
                //Check JWT Token Header
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //[authrize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//unauth
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>//verified key
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

                };
            }).AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            });


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
