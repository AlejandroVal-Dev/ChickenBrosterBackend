using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    public class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.ToTable("UnitsOfMeasure");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Abbreviation)
                .HasMaxLength(10);

            builder.Property(u => u.IsDecimal)
                .IsRequired();

            builder.Property(u => u.ConversionFactor)
                .HasColumnType("decimal(18,6)");

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt);
        }
    }
}
