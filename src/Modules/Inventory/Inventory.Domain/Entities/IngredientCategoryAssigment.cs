namespace Inventory.Domain.Entities
{
    public class IngredientCategoryAssigment
    {
        public int Id { get; private set; }
        public int IngredientId { get; private set; }
        public int CategoryId { get; private set; }

        // Constructors
        public IngredientCategoryAssigment(int ingredientId, int categoryId)
        {
            IngredientId = ingredientId;
            CategoryId = categoryId;
        }

        private IngredientCategoryAssigment() { }

    }
}
