namespace Inventory.Domain.Entities
{
    public class IngredientCategory
    {
        public int Id { get; private set; }

        // Core properties
        public string Name { get; private set; } = null!;

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public IngredientCategory(string name)
        {
            Name = name;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private IngredientCategory() { }

        // Public methods
        public void UpdateName(string name)
        {
            Name = name;
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
