using Cashbox.Domain.Enums;

namespace Cashbox.Domain.Entities
{
    public class CashRegisterSession
    {
        public int Id { get; private set; }

        // Core properties
        public DateTime OpenedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }
        public int OpenedByUserId { get; private set; }
        public int? ClosedByUserId { get; private set; }
        public decimal InitialAmount { get; private set; }
        public decimal ExpectedAmount { get; private set; }
        public decimal? CountedAmount { get; private set; }
        public decimal? Difference { get; private set; }
        public CashRegisterSessionStatus Status { get; private set; }

        // Constructors
        public CashRegisterSession(int openedByUserId, decimal initialAmount)
        {
            OpenedAt = DateTime.UtcNow;
            OpenedByUserId = openedByUserId;
            InitialAmount = initialAmount;
            ExpectedAmount = initialAmount;
            Status = CashRegisterSessionStatus.Open;
        }

        private CashRegisterSession() { }

        // Public methods
        public void Close(int closedByUserId, decimal countedAmount)
        {
            if (Status != CashRegisterSessionStatus.Open)
                throw new InvalidOperationException("Session is not open.");

            ClosedAt = DateTime.UtcNow;
            ClosedByUserId = closedByUserId;
            CountedAmount = countedAmount;
            Difference = countedAmount - ExpectedAmount;
            Status = CashRegisterSessionStatus.Closed;
        }

        public void AddSalesAmount(decimal salesAmount)
        {
            ExpectedAmount += salesAmount;
        }

        public void AddMovementAmount(decimal amount, bool isIncome)
        {
            ExpectedAmount += isIncome ? amount : -amount;
        }
    }
}
