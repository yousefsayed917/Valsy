using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever();


        // Customer
        builder.Property(o => o.CustomerId)
            .IsRequired();

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);


        // Status
        builder.Property(o => o.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(o => o.Status);


        // Shipping Address
        builder.OwnsOne(
            o => o.ShippingAddress,
            address =>
            {
                address.Property(a => a.AddressLine1)
                    .HasColumnName("ShippingAddressLine1")
                    .HasMaxLength(500)
                    .IsRequired();

                address.Property(a => a.City)
                    .HasColumnName("ShippingAddressCity")
                    .HasMaxLength(100)
                    .IsRequired();

                address.Property(a => a.Country)
                    .HasColumnName("ShippingAddressCountry")
                    .HasMaxLength(100)
                    .IsRequired();
            });


        // Contact Phone
        builder.Property(o => o.ContactPhone)
            .HasMaxLength(30)
            .IsRequired();


        // Order Amounts
        builder.Property(o => o.ItemsAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.Discount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.ShippingCost)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();


        // Created At
        builder.HasIndex(o => o.CreatedAt);


        // Audit
        builder.Property(o => o.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.LastModifiedBy)
            .HasMaxLength(100);

        builder.Property(o => o.IsDeleted)
            .IsRequired();

        builder.Property(o => o.PromoCode)
            .HasMaxLength(50);

        // Row Version
        builder.Property(o => o.RowVersion)
            .IsRowVersion();


        // Order Items
        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}