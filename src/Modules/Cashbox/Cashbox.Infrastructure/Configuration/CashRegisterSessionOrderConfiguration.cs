using Cashbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashbox.Infrastructure.Configuration
{
    public class CashRegisterSessionOrderConfiguration : IEntityTypeConfiguration<CashRegisterSessionOrder>
    {
        public void Configure(EntityTypeBuilder<CashRegisterSessionOrder> builder)
        {
            builder.ToTable("CashRegisterSessionOrders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SessionId)
                .IsRequired();

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.HasOne<CashRegisterSession>()
                .WithMany()
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
