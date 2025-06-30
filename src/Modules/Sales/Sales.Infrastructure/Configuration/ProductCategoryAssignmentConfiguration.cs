using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Configuration
{
    public class ProductCategoryAssignmentConfiguration : IEntityTypeConfiguration<ProductCategoryAssignment>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryAssignment> builder)
        {
            builder.ToTable("ProductCategoryAssignments");

            builder.HasKey(pca => pca.Id);

            builder.Property(pca => pca.ProductId)
                .IsRequired();

            builder.Property(pca => pca.CategoryId)
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(pca => pca.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<ProductCategory>()
                .WithMany()
                .HasForeignKey(pca => pca.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
