using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Domain.Entities.Inventory>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Inventory> builder)
        {
            builder.ToTable("Inventories");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.IngredientId)
                .IsRequired();

            builder.Property(i => i.ActualStock)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.MinimumStock)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.LastMovement);

            builder.Property(i => i.IsActive)
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .IsRequired();

            builder.Property(i => i.UpdatedAt);

            builder.HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(i => i.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.IngredientId).IsUnique();
        }
    }
}
