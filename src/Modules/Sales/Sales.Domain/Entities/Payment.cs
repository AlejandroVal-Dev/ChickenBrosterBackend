using Sales.Domain.Enums;

namespace Sales.Domain.Entities
{
    public class Payment
    {
        public int Id { get; private set; }

        // Core properties
        public int OrderId { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        // Constructors
        public Payment(int orderId, PaymentMethod paymentMethod, decimal amount)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            Date = DateTime.UtcNow;
        }

        private Payment() { }
    }
}
