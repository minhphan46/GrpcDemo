using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("222715a2-dbe8-47ba-85d3-b2948be8a55c"), "Category 1", new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Description of Product 1", "https://example.com/image1.jpg", "Product 1", 100, 10, new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9048e7ee-b63b-44ee-a93a-79cf1a904d86"), "Category 1", new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Description of Product 1", "https://example.com/image1.jpg", "Product 1", 100, 10, new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
