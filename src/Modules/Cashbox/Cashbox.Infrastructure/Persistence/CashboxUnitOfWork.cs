using Cashbox.Application.Util;
using Cashbox.Domain.Repositories;
using Cashbox.Infrastructure.Repositories;

namespace Cashbox.Infrastructure.Persistence
{
    public class CashboxUnitOfWork : IUnitOfWork
    {
        private readonly CashboxDbContext _database;
        public ICashRegisterSessionRepository Sessions { get; }
        public ICashRegisterMovementRepository Movements { get; }
        public ICashRegisterSessionOrderRepository Orders { get; }

        public CashboxUnitOfWork(CashboxDbContext database)
        {
            _database = database;
            Sessions = new CashRegisterSessionRepository(_database);
            Movements = new CashRegisterMovementRepository(_database);
            Orders = new CashRegisterSessionOrderRepository(_database);
        }

        public Task<int> CommitAsync() => _database.SaveChangesAsync();

    }
}
