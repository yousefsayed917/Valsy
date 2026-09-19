using BuildingBlocks.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Valsy.Domain.Common.Abstractions;
using Valsy.Domain.Discounts;
using Valsy.Domain.Orders.Repository;

namespace Valsy.Infrastructure.Repositories
{
    public class DiscountRepository : BaseDomainRepository<Discount, int>, IDiscountRepository
    {
        public DiscountRepository(IRepository<Discount, int> genericRepository) : base(genericRepository)
        {
        }
    }
}
