namespace Basket.API.Models
{
    public class CartItem : IBaseModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
