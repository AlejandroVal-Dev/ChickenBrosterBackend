namespace Inventory.Application.DTOs.RecipeIngredient
{
    public class UpdateRecipeIngredientQuantityDto
    {
        public int IngredientId { get; set; }
        public decimal NewQuantity { get; set; }
    }
}
