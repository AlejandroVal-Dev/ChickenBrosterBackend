namespace Cashbox.Application.DTOs.CashRegisterSession
{
    public class CashRegisterSessionDto
    {
        public int Id { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public int OpenedByUserId { get; set; }
        public int? ClosedByUserId { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal ExpectedAmount { get; set; }
        public decimal? CountedAmount { get; set; }
        public decimal? Difference { get; set; }
        public string Status { get; set; } = null!;
    }
}
