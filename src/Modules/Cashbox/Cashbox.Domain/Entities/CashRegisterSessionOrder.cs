namespace Cashbox.Domain.Entities
{
    public class CashRegisterSessionOrder
    {
        public int Id { get; private set; }

        // Core properties
        public int SessionId { get; private set; }
        public int OrderId { get; private set; }

        // Constructors
        public CashRegisterSessionOrder(int sessionId, int orderId)
        {
            SessionId = sessionId;
            OrderId = orderId;
        }

        private CashRegisterSessionOrder() { }
    }
}
