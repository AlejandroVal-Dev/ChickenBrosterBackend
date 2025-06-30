using Cashbox.Domain.Entities;

namespace Cashbox.Domain.Repositories
{
    public interface ICashRegisterSessionRepository
    {
        Task<IReadOnlyList<CashRegisterSession>> GetAllAsync();
        Task<CashRegisterSession?> GetByIdAsync(int id);
        Task<CashRegisterSession?> GetOpenSessionByUserAsync(int userId);
        Task<IReadOnlyList<CashRegisterSession>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task AddAsync(CashRegisterSession session);
        Task UpdateAsync(CashRegisterSession session);
    }
}
