using Catalog.API.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Grpc.Services
{
    public class CatalogService : CatalogProtoService.CatalogProtoServiceBase
    {
        private readonly ILogger<CatalogService> _logger;
        private readonly AppDbContext dbContext;
        public CatalogService(ILogger<CatalogService> logger, AppDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        // GetProductByIdRequest
        public override async Task<ProductModel> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call from client: ", request.ProductId);
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id.ToString() == request.ProductId);

            if (product is null)
                product = new API.Models.Product()
                {
                    Name = "No Product",
                    Description = "",
                    Price = 0,
                    StockQuantity = 0,
                    Category = "",
                    ImageUrl = "",
                };

            _logger.LogInformation("Product is retrieved: ", product.Id, product.Name, product.StockQuantity);

            var couponModel = new ProductModel()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
            };

            return couponModel;
        }

    }
}
