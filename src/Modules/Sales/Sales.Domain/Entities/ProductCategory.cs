namespace Sales.Domain.Entities
{
    public class ProductCategory
    {
        public int Id { get; private set; }

        // Core properties
        public string Name { get; private set; } = null!;
        public int? ParentCategoryId { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public ProductCategory(string name, int? parentCategoryId)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private ProductCategory() { }

        // Public methods
        public void UpdateName(string newName)
        {
            Name = newName;
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
