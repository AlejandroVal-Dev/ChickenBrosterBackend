namespace Inventory.Application.DTOs.Ingredient
{
    public class CreateIngredientDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? SKU { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal UnitCost { get; set; }
        public bool IsPerishable { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal ActualStock { get; set; }
        public decimal? MinimumStock { get; set; }
    }
}
