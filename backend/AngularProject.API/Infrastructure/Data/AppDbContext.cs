using AngularProject.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularProject.API.Infrastructure.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts => Set<Contact>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureContact(modelBuilder);

            modelBuilder.Entity<Contact>().HasData(

                new Contact
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@test.com",
                    PhoneNumber = "+919999999999",
                    Address = "Street 1",
                    City = "Chennai",
                    State = "Tamil Nadu",
                    Country = "India",
                    PostalCode = "600001",

                    CreatedAt = new DateTime(2025, 1, 1),
                    IsDeleted = false
                },

                new Contact
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane@test.com",
                    PhoneNumber = "+14155552671",
                    Address = "Main Road",
                    City = "New York",
                    State = "NY",
                    Country = "USA",
                    PostalCode = "10001",

                    CreatedAt = new DateTime(2025, 1, 2),
                    IsDeleted = false
                }
            );
        }

        private static void ConfigureContact(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Contact>();

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique().HasFilter("[IsDeleted] = 0");;

            entity.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.Address)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.State)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Country)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.PostalCode)
                .HasMaxLength(20)
                .IsRequired();

            entity.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}