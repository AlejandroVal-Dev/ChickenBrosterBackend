using Cashbox.Domain.Enums;

namespace Cashbox.Domain.Entities
{
    public class CashRegisterMovement
    {
        public int Id { get; private set; }

        // Core properties
        public int SessionId { get; private set; }
        public CashRegisterMovementType Type { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; } = null!;
        public int MadeByUserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Constructors
        public CashRegisterMovement(int sessionId, CashRegisterMovementType type, decimal amount, string description, int madeByUserId)
        {
            SessionId = sessionId;
            Type = type;
            Amount = amount;
            Description = description;
            MadeByUserId = madeByUserId;
            CreatedAt = DateTime.UtcNow;
        }

        private CashRegisterMovement() { }
    }
}
