using Cashbox.Domain.Repositories;

namespace Cashbox.Application.Util
{
    public interface IUnitOfWork
    {
        ICashRegisterSessionRepository Sessions { get; }
        ICashRegisterMovementRepository Movements {  get; }
        ICashRegisterSessionOrderRepository Orders { get; }
        Task<int> CommitAsync();
    }
}
