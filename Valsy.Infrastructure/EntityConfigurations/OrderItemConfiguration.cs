using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valsy.Domain;

namespace Valsy.Infrastructure.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.Property(i => i.OrderId).IsRequired();
            builder.Property(i => i.ProductId).IsRequired();
            builder.Property(i => i.ProductVariantId).IsRequired();

            builder.HasIndex(i => new { i.OrderId, i.ProductVariantId }).IsUnique();

            builder.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.Size)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Color)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.Quantity).IsRequired();

            builder.Ignore(i => i.TotalPrice);

            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(i => i.LastModifiedAt);
            builder.Property(i => i.LastModifiedBy).HasMaxLength(100);

            builder.Property(p => p.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();


            builder.HasOne(i => i.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.ProductVariant)
                .WithMany(v => v.OrderItems)
                .HasForeignKey(i => i.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
