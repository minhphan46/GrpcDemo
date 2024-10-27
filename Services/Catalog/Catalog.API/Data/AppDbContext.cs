using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;

namespace Catalog.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new
                {
                    Id = Guid.Parse("9048e7ee-b63b-44ee-a93a-79cf1a904d86"),
                    Name = "Product 1",
                    Description = "Description of Product 1",
                    Price = 100,
                    StockQuantity = 10,
                    Category = "Category 1",
                    ImageUrl = "https://example.com/image1.jpg",
                    CreatedAt = new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = Guid.Parse("222715a2-dbe8-47ba-85d3-b2948be8a55c"),
                    Name = "Product 1",
                    Description = "Description of Product 1",
                    Price = 100,
                    StockQuantity = 10,
                    Category = "Category 1",
                    ImageUrl = "https://example.com/image1.jpg",
                    CreatedAt = new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
