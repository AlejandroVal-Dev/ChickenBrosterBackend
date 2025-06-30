namespace Sales.Domain.Entities
{
    public class ProductRecipe
    {
        public int Id { get; set; }

        // Core entities
        public int ProductId { get; private set; }
        public int RecipeId { get; private set; }
        public decimal Quantity { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructos
        public ProductRecipe(int productId, int recipeId, decimal quantity)
        {
            ProductId = productId;
            RecipeId = recipeId;
            Quantity = quantity;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private ProductRecipe() { }

        // Public methods
        public void UpdateQuantity(decimal quantity)
        {
            Quantity = quantity;
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
