using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Configuration
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Number)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.IsAvailable)
                .IsRequired();

            builder.Property(t => t.IsActive)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.UpdatedAt);
        }
    }
}
