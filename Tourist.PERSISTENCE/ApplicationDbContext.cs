using Microsoft.AspNetCore.Identity;
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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripActivity> Activities { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Monument> Monuments { get; set; }
        public DbSet<FavouriteHotels> FavouriteHotels { get; set; }

        //public DbSet<TourMonument> TourMonuments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var adminRoleId = "11111111-1111-1111-1111-111111111111";
            var customerRoleId = "22222222-2222-2222-2222-222222222222";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1"
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2"
                }
            );

            var adminUserId = "aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa";
            var customerUserId = "bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb";

            var admin = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "Saif Komi",
                NormalizedUserName = "SAIF KOMI",
                Email = "saifalkomi@gmail.com",
                NormalizedEmail = "SAIFALKOMI@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "cccccccc-1111-1111-1111-cccccccccccc",
                ConcurrencyStamp = "dddddddd-1111-1111-1111-dddddddddddd",
                PhoneNumber = "+972592131946",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEDHhLq+Xep0cKJz7xXoA+yVJpVn+7L+5pXZ3RYw0nQ6fS4M4G6tGZ7kE8fVwV3Wp0w=="
            };

            var customer = new ApplicationUser
            {
                Id = customerUserId,
                UserName = "Omar Suliman",
                NormalizedUserName = "OMAR SULIMAN",
                Email = "omarsit20004031@gmail.com",
                NormalizedEmail = "OMARSIT20004031@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "eeeeeeee-2222-2222-2222-eeeeeeeeeeee",
                ConcurrencyStamp = "ffffffff-2222-2222-2222-ffffffffffff",
                PhoneNumber = "+962798461282",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEG5ccOAsgYYXQ9ndQ8YN6Ckv3GCdkkcDlMdDO4k47hcYfU/QZjnzXxZMqRdQ7Gz6Jw=="
            };

            builder.Entity<ApplicationUser>().HasData(admin, customer);

           
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = customerUserId,
                    RoleId = customerRoleId
                }
            );

           
            builder.Entity<IdentityRoleClaim<string>>().HasData(
                new IdentityRoleClaim<string>
                {
                    Id = 1,
                    RoleId = adminRoleId,
                    ClaimType = "Permission",
                    ClaimValue = "CanManageUsers"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 2,
                    RoleId = customerRoleId,
                    ClaimType = "Permission",
                    ClaimValue = "CanBookTrips"
                }
            );

           
            builder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = adminUserId,
                    ClaimType = "FullName",
                    ClaimValue = "Saif Komi"
                },
                new IdentityUserClaim<string>
                {
                    Id = 2,
                    UserId = customerUserId,
                    ClaimType = "FullName",
                    ClaimValue = "Omar Suliman"
                }
            );


            builder.Entity<Review>(e =>
            {
                e.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
                e.Property(r => r.ReviewId).UseIdentityColumn();
            });

            builder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.NotificationId);
                entity.Property(n => n.NotificationId)
                      .ValueGeneratedOnAdd();
            });


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
            {
                h.HasMany(m => m.Trips)
                .WithOne(h => h.Hotel)
                .HasForeignKey(t => t.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

                h.HasMany(h => h.Reviews)
                .WithOne(r => r.Hotel)
                .HasForeignKey(h => h.HotelId)
                .OnDelete(DeleteBehavior.NoAction);
            });



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
            builder.Entity<FavouriteHotels>(entity =>
            {
                entity.HasKey(f => f.FavouriteHotelId);
                entity.Property(f => f.FavouriteHotelId).UseIdentityColumn();

                entity.HasOne(f => f.User)
                      .WithMany()
                      .HasForeignKey(f => f.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(f => f.Hotel)
                      .WithMany()
                      .HasForeignKey(f => f.HotelId)
                      .OnDelete(DeleteBehavior.NoAction);
            });


            builder.Entity<Country>(c =>
                    c.HasIndex(e => e.Name));

            builder.Entity<City>(c =>
                    c.HasIndex(e => e.Name));

            builder.Entity<Place>(c =>
                    c.HasIndex(e => e.Name));

            //builder.Entity<TourMonument>()
            //   .HasKey(tm => new { tm.TourId, tm.MonumentId });

            //builder.Entity<TourMonument>()
            //    .HasOne(tm => tm.Tour)
            //    .WithMany(t => t.TourMonuments)
            //    .HasForeignKey(tm => tm.TourId);

            //builder.Entity<TourMonument>()
            //    .HasOne(tm => tm.Monument)
            //    .WithMany(m => m.TourMonuments)
            //    .HasForeignKey(tm => tm.MonumentId);
        }
    }
}
