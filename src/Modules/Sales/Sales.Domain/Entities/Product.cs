namespace Sales.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }

        // Core properties
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public Product(string name, decimal price, string? description)
        {
            Name = name;
            Price = price;
            Description = description;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Product() { }

        // Public methods
        public void UpdateInformation(string newName, string? newDescription, decimal newPrice)
        {
            Name = newName;
            Description = newDescription;
            Price = newPrice;
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
