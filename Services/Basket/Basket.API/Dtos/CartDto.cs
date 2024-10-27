namespace Basket.API.Dtos
{
    public class CartDto
    {
        public Guid UserId { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
