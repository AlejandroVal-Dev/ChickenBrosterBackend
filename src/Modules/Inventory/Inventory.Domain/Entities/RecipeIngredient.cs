namespace Inventory.Domain.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; private set; }
        public int RecipeId { get; private set; }
        public Recipe Recipe { get; private set; } = null!;
        public int IngredientId { get; private set; }
        public Ingredient Ingredient { get; private set; } = null!;
        public decimal Quantity { get; private set; }
        public int UnitOfMeasureId { get; private set; }

        // Constructors
        public RecipeIngredient(int recipeId, int ingredientId, decimal quantity, int unitOfMeasureId)
        {
            RecipeId = recipeId;
            IngredientId = ingredientId;
            Quantity = quantity;
            UnitOfMeasureId = unitOfMeasureId;
        }

        private RecipeIngredient() { }

        // Public methods
        public void UpdateQuantity(decimal quantity)
        {
            Quantity = quantity;
        }
    }
}
