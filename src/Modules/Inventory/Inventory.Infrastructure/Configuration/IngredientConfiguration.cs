using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredients");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.Description)
                .HasMaxLength(250);

            builder.Property(i => i.SKU)
                .HasMaxLength(50);

            builder.Property(i => i.UnitOfMeasureId)
                .IsRequired();

            builder.Property(i => i.UnitCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.IsPerishable)
                .IsRequired();

            builder.Property(i => i.ExpirationDate);

            builder.Property(i => i.IsActive)
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .IsRequired();

            builder.Property(i => i.UpdatedAt);

            builder.HasOne<UnitOfMeasure>()
                .WithMany()
                .HasForeignKey(i => i.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
