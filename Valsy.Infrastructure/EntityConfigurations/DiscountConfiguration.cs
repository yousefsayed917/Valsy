using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valsy.Domain.Discounts;

namespace Valsy.Infrastructure.EntityConfigurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discount");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.PromoCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.PromoCode)
            .IsUnique();

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Value)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.MinimumOrderAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.MaximumDiscountAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate);

        builder.Property(x => x.UsageLimit);

        builder.Property(x => x.UsageCount)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastModifiedBy)
            .HasMaxLength(100);

        builder.Property(x => x.IsDeleted)
            .IsRequired();
    }
}