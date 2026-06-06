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
            builder.Property(v => v.Id).ValueGeneratedNever();

            builder.Property(v => v.Size)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Color)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Stock)
                .IsRequired();

            builder.Property(v => v.ProductId)
                .IsRequired();

            builder.Property(v => v.CreatedAt).IsRequired();
            builder.Property(v => v.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(v => v.LastModifiedAt);
            builder.Property(v => v.LastModifiedBy).HasMaxLength(100);

            builder.Property(p => p.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();


            builder.HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(v => new { v.ProductId, v.Size, v.Color })
                .IsUnique();

            builder.HasMany(v => v.OrderItems)
                .WithOne(i => i.ProductVariant)
                .HasForeignKey(i => i.ProductVariantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
