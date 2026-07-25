using BuildingBlocks.Infrastructure.Repositories;
using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Customers;
using Valsy.Domain.Customers.Repository;

namespace Valsy.Infrastructure.Repositories
{
    public class CustomerRepository : BaseDomainRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IRepository<Customer, int> genericRepository) : base(genericRepository)
        {
        }
    }
}
