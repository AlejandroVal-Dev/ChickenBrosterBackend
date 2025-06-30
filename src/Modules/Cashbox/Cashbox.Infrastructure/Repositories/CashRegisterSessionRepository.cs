using Cashbox.Domain.Entities;
using Cashbox.Domain.Enums;
using Cashbox.Domain.Repositories;
using Cashbox.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cashbox.Infrastructure.Repositories
{
    public class CashRegisterSessionRepository : ICashRegisterSessionRepository
    {
        private readonly CashboxDbContext _database;

        public CashRegisterSessionRepository(CashboxDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<CashRegisterSession>> GetAllAsync()
        {
            return await _database.Sessions
                .ToListAsync();
        }

        public async Task<CashRegisterSession?> GetByIdAsync(int id)
        {
            return await _database.Sessions
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<CashRegisterSession?> GetOpenSessionByUserAsync(int userId)
        {
            return await _database.Sessions
                .FirstOrDefaultAsync(s =>
                    s.OpenedByUserId == userId &&
                    s.Status == CashRegisterSessionStatus.Open);
        }

        public async Task<IReadOnlyList<CashRegisterSession>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _database.Sessions
                .Where(s => s.OpenedAt >= from && s.OpenedAt <= to)
                .ToListAsync();
        }

        public async Task AddAsync(CashRegisterSession session)
        {
            await _database.Sessions
                .AddAsync(session);
        }

        public Task UpdateAsync(CashRegisterSession session)
        {
            _database.Sessions.Update(session);
            return Task.CompletedTask;
        }
    }
}
