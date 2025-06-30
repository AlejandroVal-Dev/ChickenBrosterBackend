using Sales.Domain.Entities;
using Sales.Domain.Enums;

namespace Sales.Domain.Repositories
{
    public interface IOrderRepository
    {
        // Reading
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetFilteredAsync(OrderType? orderType, OrderStatus? status);

        // Writting
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}
