namespace Sales.Application.DTOs.ProductRecipe
{
    public class ProductRecipeDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public decimal Quantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
