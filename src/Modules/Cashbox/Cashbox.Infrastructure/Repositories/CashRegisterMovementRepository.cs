using Cashbox.Domain.Entities;
using Cashbox.Domain.Repositories;
using Cashbox.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cashbox.Infrastructure.Repositories
{
    public class CashRegisterMovementRepository : ICashRegisterMovementRepository
    {
        private readonly CashboxDbContext _database;

        public CashRegisterMovementRepository(CashboxDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<CashRegisterMovement>> GetAllAsync()
        {
            return await _database.Movements
                .ToListAsync();
        }

        public async Task<IReadOnlyList<CashRegisterMovement>> GetBySessionIdAsync(int sessionId)
        {
            return await _database.Movements
                .Where(m => m.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task AddAsync(CashRegisterMovement movement)
        {
            await _database.Movements
                .AddAsync(movement);
        }
    }
}
