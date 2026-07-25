using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valsy.Domain.Products;

namespace Valsy.Infrastructure.EntityConfigurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Size)
                .HasMaxLength(50);

            builder.Property(v => v.Color)
                .HasMaxLength(100);

            builder.Property(v => v.Image)
                .HasMaxLength(500);

            builder.Property(v => v.Stock)
                .IsRequired();

            builder.Property(v => v.ProductId)
                .IsRequired();

            builder.Property(v => v.CreatedAt).IsRequired();
            builder.Property(v => v.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(v => v.LastModifiedAt);
            builder.Property(v => v.LastModifiedBy).HasMaxLength(100);
        }
    }
}
