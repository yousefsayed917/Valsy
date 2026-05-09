using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain;

namespace Valsy.Infrastructure;

public class ValsyDbContext : DbContext, IApplicationDbContext
{
    public ValsyDbContext(DbContextOptions<ValsyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductVariant> ProductVariants { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
}
