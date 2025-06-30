using Cashbox.Domain.Entities;

namespace Cashbox.Domain.Repositories
{
    public interface ICashRegisterMovementRepository
    {
        Task<IReadOnlyList<CashRegisterMovement>> GetAllAsync();
        Task<IReadOnlyList<CashRegisterMovement>> GetBySessionIdAsync(int sessionId);
        Task AddAsync(CashRegisterMovement movement);
    }
}
