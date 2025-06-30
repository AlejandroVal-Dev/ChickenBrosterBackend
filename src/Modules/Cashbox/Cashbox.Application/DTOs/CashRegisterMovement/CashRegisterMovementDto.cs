using Cashbox.Domain.Enums;

namespace Cashbox.Application.DTOs.CashRegisterMovement
{
    public class CashRegisterMovementDto
    {
        public int Id { get; set; }
        public CashRegisterMovementType Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = null!;
        public int MadeByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
