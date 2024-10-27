using AutoMapper;
using Catalog.API.Data;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllAsync(string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Product> products = dbContext.Products;

            // Sorting 
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(x => x.CreatedAt) : products.OrderByDescending(x => x.CreatedAt);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await products
                .Skip(skipResults)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Category = product.Category;
            existingProduct.ImageUrl = product.ImageUrl;

            await dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
