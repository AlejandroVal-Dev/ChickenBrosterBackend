using Cashbox.Domain.Entities;
using Cashbox.Domain.Repositories;
using Cashbox.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cashbox.Infrastructure.Repositories
{
    public class CashRegisterSessionOrderRepository : ICashRegisterSessionOrderRepository
    {
        private readonly CashboxDbContext _database;

        public CashRegisterSessionOrderRepository(CashboxDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<CashRegisterSessionOrder>> GetAllAsync()
        {
            return await _database.SessionOrders
                .ToListAsync();
        }

        public async Task<IReadOnlyList<CashRegisterSessionOrder>> GetBySessionIdAsync(int sessionId)
        {
            return await _database.SessionOrders
                .Where(o => o.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task AddAsync(CashRegisterSessionOrder sessionOrder)
        {
            await _database.SessionOrders
                .AddAsync(sessionOrder);
        }

        public async Task AddRangeAsync(IEnumerable<CashRegisterSessionOrder> sessionOrders)
        {
            await _database.SessionOrders
                .AddRangeAsync(sessionOrders);
        }
    }
}
