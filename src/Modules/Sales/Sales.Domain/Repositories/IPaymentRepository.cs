using Sales.Domain.Entities;

namespace Sales.Domain.Repositories
{
    public interface IPaymentRepository
    {
        // Reading
        Task<IReadOnlyList<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<IReadOnlyList<Payment>> GetByOrderIdAsync(int orderId);

        // Writting
        Task AddAsync(Payment payment);
    }
}
