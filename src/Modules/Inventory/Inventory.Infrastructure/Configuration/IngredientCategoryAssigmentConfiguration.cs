using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    public class IngredientCategoryAssigmentConfiguration : IEntityTypeConfiguration<IngredientCategoryAssigment>
    {
        public void Configure(EntityTypeBuilder<IngredientCategoryAssigment> builder)
        {
            builder.ToTable("IngredientCategoryAssigments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.IngredientId)
                .IsRequired();

            builder.Property(a => a.CategoryId)
                .IsRequired();

            builder.HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(a => a.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<IngredientCategory>()
                .WithMany()
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
