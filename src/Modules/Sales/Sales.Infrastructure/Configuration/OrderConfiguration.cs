using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId);

            builder.Property(o => o.TableId);

            builder.Property(o => o.OrderType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.UpdatedAt);

            builder.HasOne<Table>()
                .WithMany()
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
