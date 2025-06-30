using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configuration
{
    public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.ToTable("InventoryMovements");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.IngredientId)
                .IsRequired();

            builder.Property(m => m.UnitOfMeasureId)
                .IsRequired();

            builder.Property(m => m.Quantity)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(m => m.MovementType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(m => m.Reason)
                .HasMaxLength(250);

            builder.Property(m => m.MadeByUserId);

            builder.Property(m => m.MovementDate)
                .IsRequired();

            builder.HasOne<Ingredient>()
                .WithMany()
                .HasForeignKey(m => m.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<UnitOfMeasure>()
                .WithMany()
                .HasForeignKey(m => m.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
