using Basket.API.Data;
using Basket.API.Dtos;
using Basket.API.Models;
using Catalog.Grpc;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Repositories
{
    public class BasketRepository
    {
        private readonly AppDbContext dbContext;
        private readonly CatalogProtoService.CatalogProtoServiceClient catalogProtoco;

        public BasketRepository(AppDbContext dbContext, CatalogProtoService.CatalogProtoServiceClient catalogProtoco)
        {
            this.dbContext = dbContext;
            this.catalogProtoco = catalogProtoco;
        }

        // get
        public async Task<List<Cart>> GetCartsAsync()
        {
            List<Cart> carts = await dbContext.Carts
                .Include(x => x.Items)
                .ToListAsync();
            return carts;
        }

        // get by userId
        public async Task<List<Cart>> GetCartsByUserIdAsync(Guid userId)
        {
            var carts = await dbContext.Carts
                .Where(x => x.UserId == userId)
                .Include(x => x.Items)
                .ToListAsync();
            return carts;
        }

        // update
        public async Task<Cart?> UpdateAsync(Guid id, CartDto cartDto)
        {
            var existingCart = await dbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCart == null)
            {
                return null;
            }

            // get items from catalog service
            Cart cart = new Cart();
            List<CartItem> cartItems = new List<CartItem>();
            foreach (var item in cartDto.Items)
            {
                var productId = item.ProductId;
                // get Product from catalog service
                var request = new GetProductByIdRequest();
                request.ProductId = productId.ToString();
                var product = await catalogProtoco.GetProductByIdAsync(request);

                // add item to items
                CartItem cartItem = new CartItem()
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = item.Quantity
                };
                cartItems.Add(cartItem);
            }

            // map dto to domain model
            existingCart.UserId = cartDto.UserId;
            existingCart.Items = cartItems;

            await dbContext.SaveChangesAsync();
            return existingCart;
        }

        // add
        public async Task<Cart> CreateAsync(CartDto cartDto)
        {
            // get items from catalog service
            Cart cart = new Cart();
            List<CartItem> cartItems = new List<CartItem>();
            foreach (var item in cartDto.Items)
            {
                var productId = item.ProductId;
                // get Product from catalog service
                var request = new GetProductByIdRequest();
                request.ProductId = productId.ToString();
                var product = await catalogProtoco.GetProductByIdAsync(request);

                // add item to items
                CartItem cartItem = new CartItem()
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = item.Quantity
                };
                cartItems.Add(cartItem);
            }

            // map dto to domain model
            cart.UserId = cartDto.UserId;
            cart.Items = cartItems;

            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }
        // delete
        public async Task<Cart?> DeleteAsync(Guid id)
        {
            var existingCart = await dbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCart == null)
            {
                return null;
            }

            dbContext.Carts.Remove(existingCart);
            await dbContext.SaveChangesAsync();
            return existingCart;
        }
    }
}
