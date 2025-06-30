using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs.InventoryMovement
{
    public class InventoryMovementFilterDto
    {
        public int? IngredientId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public MovementType? MovementType { get; set; }
        public int? MadeByUserId { get; set; }
    }
}
