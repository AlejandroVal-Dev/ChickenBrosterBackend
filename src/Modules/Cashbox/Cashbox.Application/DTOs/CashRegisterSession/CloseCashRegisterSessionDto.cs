namespace Cashbox.Application.DTOs.CashRegisterSession
{
    public class CloseCashRegisterSessionDto
    {
        public int SessionId { get; set; }
        public int ClosedByUserId { get; set; }
        public decimal CountedAmount { get; set; }
        public List<int> OrderIds { get; set; } = new();
    }
}
