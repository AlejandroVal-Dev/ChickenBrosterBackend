using Sales.Application.DTOs.Payment;
using SharedKernel.Util;

namespace Sales.Application.UseCases
{
    public interface IPaymentService
    {
        Task<Result<IReadOnlyList<PaymentDto>>> GetAllAsync();
        Task<Result<PaymentDto>> GetByIdAsync(int paymentId);
        Task<Result<IReadOnlyList<PaymentDto>>> GetByOrderIdAsync(int orderId);
        Task<Result<int>> AddPaymentAsync(CreatePaymentDto request);
    }
}
