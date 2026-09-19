using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Discounts;

namespace Valsy.Domain.Orders.Repository
{
    public interface IDiscountRepository : IDomainRepository<Discount, int>
    {
    }
}
