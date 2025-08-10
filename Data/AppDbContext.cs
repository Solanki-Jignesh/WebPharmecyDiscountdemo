using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPharmecyDiscountdemo.Models.Entities;

namespace WebPharmecyDiscountdemo.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DiscountCode> TlbDiscountCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for DiscountCode
            modelBuilder.Entity<DiscountCode>()
                .Property(d => d.Value)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DiscountCode>()
                .Property(d => d.MinimumCartValue)
                .HasPrecision(18, 2);

            // Password hasher
            var hasher = new PasswordHasher<User>();

            // Three dummy users
            var user1 = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser1",
                NormalizedUserName = "TESTUSER1",
                Email = "testuser1@example.com",
                NormalizedEmail = "TESTUSER1@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "User One"
            };
            user1.PasswordHash = hasher.HashPassword(user1, "Password@123");

            var user2 = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser2",
                NormalizedUserName = "TESTUSER2",
                Email = "testuser2@example.com",
                NormalizedEmail = "TESTUSER2@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "User Two"
            };
            user2.PasswordHash = hasher.HashPassword(user2, "Password@123");

            var user3 = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser3",
                NormalizedUserName = "TESTUSER3",
                Email = "testuser3@example.com",
                NormalizedEmail = "TESTUSER3@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "User Three"
            };
            user3.PasswordHash = hasher.HashPassword(user3, "Password@123");

            // Seed all users
            modelBuilder.Entity<User>().HasData(user1, user2, user3);
        }
    }
}
