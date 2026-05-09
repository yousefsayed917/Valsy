using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valsy.Domain;

namespace Valsy.Infrastructure.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();

            builder.Property(o => o.CustomerId).IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(o => o.ShippingAddressLine1)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(o => o.ShippingCity)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.ShippingCountry)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.ContactPhone)
                .IsRequired()
                .HasMaxLength(30);

            builder.Ignore(o => o.TotalAmount);

            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(o => o.LastModifiedAt);
            builder.Property(o => o.LastModifiedBy).HasMaxLength(100);

            builder.Property(o => o.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
