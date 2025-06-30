using Cashbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashbox.Infrastructure.Configuration
{
    public class CashRegisterMovementConfiguration : IEntityTypeConfiguration<CashRegisterMovement>
    {
        public void Configure(EntityTypeBuilder<CashRegisterMovement> builder)
        {
            builder.ToTable("CashRegisterMovements");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SessionId)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.MadeByUserId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne<CashRegisterSession>()
                .WithMany()
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
