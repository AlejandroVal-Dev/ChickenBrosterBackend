namespace Inventory.Application.DTOs.Inventory
{
    public class UpdateInventoryMinimumStockDto
    {
        public int IngredientId { get; set; }
        public decimal? MinimumStock { get; set; }
    }
}
