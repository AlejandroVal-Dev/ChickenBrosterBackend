namespace Inventory.Application.DTOs.Recipe
{
    public class UpdateRecipeDto
    {
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
