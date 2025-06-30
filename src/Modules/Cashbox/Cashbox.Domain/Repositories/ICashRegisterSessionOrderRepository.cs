using Cashbox.Domain.Entities;

namespace Cashbox.Domain.Repositories
{
    public interface ICashRegisterSessionOrderRepository
    {
        Task<IReadOnlyList<CashRegisterSessionOrder>> GetAllAsync();
        Task<IReadOnlyList<CashRegisterSessionOrder>> GetBySessionIdAsync(int sessionId);
        Task AddAsync(CashRegisterSessionOrder sessionOrder);
        Task AddRangeAsync(IEnumerable<CashRegisterSessionOrder> sessionOrders);
    }
}
