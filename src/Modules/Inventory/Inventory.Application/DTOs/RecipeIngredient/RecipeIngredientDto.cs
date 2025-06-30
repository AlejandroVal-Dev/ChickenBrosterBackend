namespace Inventory.Application.DTOs.RecipeIngredient
{
    public class RecipeIngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string UnitOfMeasureAbbreviation { get; set; } = null!;
    }
}
