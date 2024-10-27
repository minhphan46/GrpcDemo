using Basket.API.Data;
using Catalog.Grpc;

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
    }
}
