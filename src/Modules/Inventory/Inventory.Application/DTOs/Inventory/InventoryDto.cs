namespace Inventory.Application.DTOs.Inventory
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string IngredientName { get; set; } = null!;
        public decimal ActualStock { get; set; }
        public decimal? MinimumStock { get; set; }
        public bool UnderMinimum { get; set; }
        public DateTime? LastMovement { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
