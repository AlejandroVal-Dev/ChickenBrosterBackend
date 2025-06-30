namespace Inventory.Application.DTOs.RecipeIngredient
{
    public class CreateRecipeIngredientDto
    {
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public int UnitOfMeasureId { get; set; }
    }
}
