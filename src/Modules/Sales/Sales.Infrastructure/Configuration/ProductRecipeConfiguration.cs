using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Configuration
{
    public class ProductRecipeConfiguration : IEntityTypeConfiguration<ProductRecipe>
    {
        public void Configure(EntityTypeBuilder<ProductRecipe> builder)
        {
            builder.ToTable("ProductRecipes");

            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.ProductId)
                .IsRequired();

            builder.Property(pr => pr.RecipeId)
                .IsRequired();

            builder.Property(pr => pr.Quantity)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property(pr => pr.IsActive)
                .IsRequired();

            builder.Property(pr => pr.CreatedAt)
                .IsRequired();

            builder.Property(pr => pr.UpdatedAt);
        }
    }
}
