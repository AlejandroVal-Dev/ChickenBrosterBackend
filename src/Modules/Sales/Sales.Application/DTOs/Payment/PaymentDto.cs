using Sales.Domain.Enums;

namespace Sales.Application.DTOs.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
