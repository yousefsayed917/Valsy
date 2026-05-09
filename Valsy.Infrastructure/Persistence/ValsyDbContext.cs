using Microsoft.EntityFrameworkCore;
using Valsy.Domain;

namespace Valsy.Infrastructure
{
    public class ValsyDbContext : DbContext
    {
        public ValsyDbContext(DbContextOptions<ValsyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
      => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
    }
}
