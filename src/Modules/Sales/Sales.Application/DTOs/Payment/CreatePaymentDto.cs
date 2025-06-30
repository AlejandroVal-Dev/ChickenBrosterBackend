using Sales.Domain.Enums;

namespace Sales.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
