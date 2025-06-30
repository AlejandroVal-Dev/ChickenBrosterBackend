using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SalesDbContext _database;

        public OrderItemRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<OrderItem>> GetAllAsync()
        {
            return await _database.OrderItems
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _database.OrderItems
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _database.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderItem?> GetByOrderAndProductAsync(int orderId, int productId)
        {
            return await _database.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _database.OrderItems
                .AddAsync(orderItem);
        }

        public Task UpdateAsync(OrderItem orderItem)
        {
            _database.OrderItems.Update(orderItem);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(OrderItem orderItem)
        {
            _database.OrderItems.Remove(orderItem);
            return Task.CompletedTask;
        }
    }
}
