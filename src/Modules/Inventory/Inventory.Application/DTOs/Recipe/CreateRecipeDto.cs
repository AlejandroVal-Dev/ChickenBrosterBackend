using Inventory.Application.DTOs.RecipeIngredient;

namespace Inventory.Application.DTOs.Recipe
{
    public class CreateRecipeDto
    {
        public string Name { get; set; } = null!;
        public List<CreateRecipeIngredientDto> Ingredients { get; set; } = new();
    }
}
