using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valsy.Domain;

namespace Valsy.Infrastructure.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.AddressLine1)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LastModifiedAt);
            builder.Property(c => c.LastModifiedBy).HasMaxLength(100);

            builder.Property(p => p.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();

        }
    }
}
