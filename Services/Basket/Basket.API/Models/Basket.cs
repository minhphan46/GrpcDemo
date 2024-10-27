namespace Basket.API.Models
{
    public class Cart : IBaseModel
    {
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    }
}
