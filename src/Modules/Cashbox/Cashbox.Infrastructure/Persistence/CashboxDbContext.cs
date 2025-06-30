using Cashbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashbox.Infrastructure.Persistence
{
    public class CashboxDbContext : DbContext
    {
        public DbSet<CashRegisterSession> Sessions => Set<CashRegisterSession>();
        public DbSet<CashRegisterMovement> Movements => Set<CashRegisterMovement>();
        public DbSet<CashRegisterSessionOrder> SessionOrders => Set<CashRegisterSessionOrder>();

        public CashboxDbContext(DbContextOptions<CashboxDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CashboxDbContext).Assembly);
        }
    }
}
