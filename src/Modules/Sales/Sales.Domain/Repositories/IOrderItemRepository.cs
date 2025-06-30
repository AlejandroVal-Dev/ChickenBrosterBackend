using Sales.Domain.Entities;

namespace Sales.Domain.Repositories
{
    public interface IOrderItemRepository
    {
        // Reading
        Task<IReadOnlyList<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(int id);
        Task<IReadOnlyList<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<OrderItem?> GetByOrderAndProductAsync(int orderId, int productId);

        // Writting
        Task AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(OrderItem orderItem);
    }
}
