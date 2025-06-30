using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Enums;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesDbContext _database;

        public OrderRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            return await _database.Orders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _database.Orders
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<Order>> GetFilteredAsync(OrderType? orderType, OrderStatus? status)
        {
            IQueryable<Order> query = _database.Orders.AsQueryable();

            if (orderType.HasValue)
                query = query.Where(o => o.OrderType == orderType);

            if (status.HasValue)
                query = query.Where(o => o.Status == status);

            return await query.ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _database.Orders
                .AddAsync(order);
        }

        public Task UpdateAsync(Order order)
        {
            _database.Orders.Update(order);
            return Task.CompletedTask;
        }

    }
}
