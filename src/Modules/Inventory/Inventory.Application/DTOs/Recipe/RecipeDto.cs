using Inventory.Application.DTOs.RecipeIngredient;

namespace Inventory.Application.DTOs.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<RecipeIngredientDto> Ingredients { get; set; } = new();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
