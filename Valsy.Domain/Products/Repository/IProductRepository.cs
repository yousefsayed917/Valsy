using Valsy.Domain.Common.Abstractions;

namespace Valsy.Domain.Products.Repository
{
    public interface IProductRepository : IDomainRepository<Product, int>
    {
    }
}
