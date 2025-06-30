using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(pc => pc.ParentCategoryId);

            builder.Property(pc => pc.IsActive)
                .IsRequired();

            builder.Property(pc => pc.CreatedAt)
                .IsRequired();

            builder.Property(pc => pc.UpdatedAt);

            builder.HasOne<ProductCategory>()
                .WithMany()
                .HasForeignKey(pc => pc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
