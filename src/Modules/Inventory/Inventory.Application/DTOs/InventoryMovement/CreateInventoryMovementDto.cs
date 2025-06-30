using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs.InventoryMovement
{
    public class CreateInventoryMovementDto
    {
        public int IngredientId { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public string? Reason { get; set; }
        public int? MadeByUserId { get; set; }
    }
}
