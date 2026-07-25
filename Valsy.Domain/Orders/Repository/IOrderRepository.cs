using Valsy.Domain.Common.Abstractions;

namespace Valsy.Domain.Orders.Repository
{
    public interface IOrderRepository : IDomainRepository<Order, int>
    {
    }
}
