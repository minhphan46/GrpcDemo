﻿namespace Basket.API.Dtos
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
