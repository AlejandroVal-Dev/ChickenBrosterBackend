using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SalesDbContext _database;

        public PaymentRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Payment>> GetAllAsync()
        {
            return await _database.Payments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _database.Payments
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<Payment>> GetByOrderIdAsync(int orderId)
        {
            return await _database.Payments
                .Where(p => p.OrderId == orderId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Payment payment)
        {
            await _database.Payments
                .AddAsync(payment);
        }
    }
}
