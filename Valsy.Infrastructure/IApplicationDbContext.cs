using Microsoft.EntityFrameworkCore;
using Valsy.Domain.Customers;
using Valsy.Domain.Orders;
using Valsy.Domain.Products;

namespace Valsy.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<ProductVariant> ProductVariants { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
