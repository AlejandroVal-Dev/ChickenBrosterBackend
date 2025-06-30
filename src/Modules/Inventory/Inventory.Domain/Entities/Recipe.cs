namespace Inventory.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public List<RecipeIngredient> Ingredients { get; private set; } = new();

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<RecipeIngredient>();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Recipe() { }

        // Public methods
        public void UpdateInformation(string name)
        {
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddIngredient(int ingredientId, decimal quantity, int unitOfMeasureId)
        {
            Ingredients.Add(new RecipeIngredient(Id, ingredientId, quantity, unitOfMeasureId));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveIngredient(int ingredientId)
        {
            var existing = Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
            if (existing is not null)
                Ingredients.Remove(existing);

            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateIngredientQuantity(int ingredientId, decimal newQuantity)
        {
            var existing = Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
            if (existing is not null)
                existing.UpdateQuantity(newQuantity);

            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            if (!IsActive)
                return;

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            if (!IsActive)
            {
                IsActive = true;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
