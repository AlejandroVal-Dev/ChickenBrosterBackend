namespace Inventory.Application.DTOs.InventoryMovement
{
    public class InventoryMovementDto
    {
        public int Id { get; set; }
        public string IngredientName { get; set; } = null!;
        public string UnitOfMeasureAbbreviation { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string MovementType { get; set; } = null!;
        public string? Reason { get; set; }
        public int? MadeByUserId { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
