namespace Cashbox.Application.DTOs.CashRegisterSession
{
    public class OpenCashRegisterSessionDto
    {
        public int OpenedByUserId { get; set; }
        public decimal InitialAmount { get; set; }
    }
}
