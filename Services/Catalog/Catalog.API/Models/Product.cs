using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models
{
    public class Product : IBaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}