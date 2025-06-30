using Cashbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashbox.Infrastructure.Configuration
{
    public class CashRegisterSessionConfiguration : IEntityTypeConfiguration<CashRegisterSession>
    {
        public void Configure(EntityTypeBuilder<CashRegisterSession> builder)
        {
            builder.ToTable("CashRegisterSessions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OpenedAt)
                .IsRequired();

            builder.Property(x => x.ClosedAt);

            builder.Property(x => x.OpenedByUserId)
                .IsRequired();

            builder.Property(x => x.ClosedByUserId);

            builder.Property(x => x.InitialAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.ExpectedAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.CountedAmount)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Difference)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}
