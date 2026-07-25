using BuildingBlocks.Infrastructure.Repositories;
using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Orders;
using Valsy.Domain.Orders.Repository;

namespace Valsy.Infrastructure.Repositories
{
    public class OrderRepository : BaseDomainRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(IRepository<Order, int> genericRepository) : base(genericRepository)
        {
        }
    }
}
