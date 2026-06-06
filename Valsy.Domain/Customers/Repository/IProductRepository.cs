using Valsy.Domain.Common.Abstractions;

namespace Valsy.Domain.Customers.Repository
{
    public interface ICustomerRepository : IDomainRepository<Customer, int>
    {
    }
}
