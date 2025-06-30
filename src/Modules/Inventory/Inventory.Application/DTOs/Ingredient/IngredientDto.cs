namespace Inventory.Application.DTOs.Ingredient
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? SKU { get; set; }
        public string UnitOfMeasureAbbreviation { get; set; } = null!;
        public decimal UnitCost { get; set; }
        public bool IsPerishable { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
