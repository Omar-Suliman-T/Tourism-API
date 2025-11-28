using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Notification> Norifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripActivity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Review>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

            builder.Entity<Notification>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

            builder.Entity<Payment>()
            .Property(t => t.Date)
            .HasDefaultValueSql("GETDATE()");

            builder.Entity<City>(c =>
            {
                c.HasMany(h => h.Hotels)
                    .WithOne(ho => ho.City)
                    .HasForeignKey(ho => ho.CityId)
                    .OnDelete(DeleteBehavior.NoAction);

                c.HasMany(h => h.Places)
                    .WithOne(ho => ho.City)
                    .HasForeignKey(ho => ho.CityId)
                    .OnDelete(DeleteBehavior.NoAction);

                c.HasMany(h => h.Trips)
                    .WithOne(ho => ho.City)
                    .HasForeignKey(ho => ho.CityId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Country>(c =>
                    c.HasMany(h => h.Cities)
                    .WithOne(c => c.Country)
                    .HasForeignKey(c => c.CountryId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<Category>(e =>
                    e.HasMany(h => h.Places)
                    .WithOne(w => w.Category)
                    .HasForeignKey(f => f.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<Hotel>(h => 
                    h.HasMany(m => m.Trips)
                    .WithOne(h=> h.Hotel)
                    .HasForeignKey(t => t.HotelId)
                    .OnDelete(DeleteBehavior.NoAction));                   

            builder.Entity<Place>(p => 
                    p.HasMany(p => p.Activities)
                    .WithOne(w => w.Place)
                    .HasForeignKey(c => c.PlaceId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<Trip>(t =>
            {
                t.HasMany(h => h.Activities)
                .WithOne(t => t.Trip)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.NoAction);

                t.HasMany(h => h.Payments)
                .WithOne(t => t.Trip)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.NoAction);

                t.HasMany(h => h.Reviews)
                .WithOne(t => t.Trip)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ApplicationUser>(a =>
            {
                a.HasMany(t => t.Trips)
                .WithOne(r => r.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                a.HasMany(t => t.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                a.HasMany(t => t.Notifications)
                .WithOne(r => r.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Country>(c =>
                    c.HasIndex(e => e.Name));

            builder.Entity<City>(c =>
                    c.HasIndex(e => e.Name));

            builder.Entity<Place>(c =>
                    c.HasIndex(e => e.Name));
        }
    }
}
