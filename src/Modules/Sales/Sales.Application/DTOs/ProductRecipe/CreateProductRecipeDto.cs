namespace Sales.Application.DTOs.ProductRecipe
{
    public class CreateProductRecipeDto
    {
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public decimal Quantity { get; set; }
    }
}
