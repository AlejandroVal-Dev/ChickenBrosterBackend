using Sales.Application.DTOs.Payment;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Entities;
using Sales.Domain.Enums;
using SharedKernel.Util;

namespace Sales.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<PaymentDto>>> GetAllAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            var result = payments.Select(t => new PaymentDto
            {
                Id = t.Id,
                OrderId = t.OrderId,
                PaymentMethod = t.PaymentMethod,
                Amount = t.Amount,
                Date = t.Date
            }).ToList();

            return Result<IReadOnlyList<PaymentDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<PaymentDto>>> GetByOrderIdAsync(int orderId)
        {
            var payments = await _unitOfWork.Payments.GetByOrderIdAsync(orderId);
            var result = payments.Select(t => new PaymentDto
            {
                Id = t.Id,
                OrderId = t.OrderId,
                PaymentMethod = t.PaymentMethod,
                Amount = t.Amount,
                Date = t.Date
            }).ToList();

            return Result<IReadOnlyList<PaymentDto>>.Success(result);
        }

        public async Task<Result<PaymentDto>> GetByIdAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment is null)
                return Result<PaymentDto>.Failure("Payment not found");

            var dto = new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,
                Date = payment.Date
            };

            return Result<PaymentDto>.Success(dto);
        }

        public async Task<Result<int>> AddPaymentAsync(CreatePaymentDto request)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order is null)
                return Result<int>.Failure("Order not found");

            if (order.Status == OrderStatus.Cancelled)
                return Result<int>.Failure("Cannot add payment to a cancelled order");

            if (order.Status == OrderStatus.Paid)
                return Result<int>.Failure("Order is already fully paid");

            var existingPayments = await _unitOfWork.Payments.GetByOrderIdAsync(order.Id);
            var totalPaid = existingPayments.Sum(p => p.Amount);

            var newTotalPaid = totalPaid + request.Amount;

            if (newTotalPaid > order.TotalAmount)
                return Result<int>.Failure("Payment exceeds total amount of the order");

            var payment = new Payment(
                orderId: order.Id,
                paymentMethod: request.PaymentMethod,
                amount: request.Amount
            );

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.CommitAsync();

            if (newTotalPaid == order.TotalAmount)
            {
                order.MarkAsPaid();
                await _unitOfWork.Orders.UpdateAsync(order);
                await _unitOfWork.CommitAsync();
            }

            return Result<int>.Success(payment.Id);
        }

        
    }
}
