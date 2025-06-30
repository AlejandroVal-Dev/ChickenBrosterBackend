namespace Inventory.Application.DTOs.IngredientCategory
{
    public class UpdateIngredientCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
    }
}
