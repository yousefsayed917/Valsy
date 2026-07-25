using BuildingBlocks.Infrastructure.Repositories;
using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Products;
using Valsy.Domain.Products.Repository;

namespace Valsy.Infrastructure.Repositories
{
    public class ProductRepository : BaseDomainRepository<Product, int>, IProductRepository
    {
        public ProductRepository(IRepository<Product, int> genericRepository) : base(genericRepository)
        {
        }
    }
}
