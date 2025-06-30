using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.ToTable("RecipeIngredients");

            builder.HasKey(ri => ri.Id);

            builder.Property(ri => ri.RecipeId)
                .IsRequired();

            builder.Property(ri => ri.IngredientId)
                .IsRequired();

            builder.Property(ri => ri.Quantity)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(ri => ri.UnitOfMeasureId)
                .IsRequired();

            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
