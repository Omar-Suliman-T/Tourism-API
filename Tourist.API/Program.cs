
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tourist.DOMAIN.model;
using Tourist.PERSISTENCE;

namespace Tourist.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var ConnectionString = builder.Configuration.GetConnectionString("Tour");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(ConnectionString));

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                options.User.RequireUniqueEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();     

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
