using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Dtos
{
    public class AddProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
