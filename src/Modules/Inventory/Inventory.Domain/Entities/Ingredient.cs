namespace Inventory.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        // Core properties
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public string? SKU { get; private set; }
        public int UnitOfMeasureId { get; private set; }
        public decimal UnitCost { get; private set; }
        public bool IsPerishable { get; private set; }
        public DateTime? ExpirationDate { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public Ingredient(string name, string? description, string? sKU, int unitOfMeasureId, decimal unitCost, bool isPerishable, DateTime? expirationDate)
        {
            Name = name;
            Description = description;
            SKU = sKU;
            UnitOfMeasureId = unitOfMeasureId;
            UnitCost = unitCost;
            IsPerishable = isPerishable;
            ExpirationDate = expirationDate;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Ingredient() { }

        // Public Methods
        public void UpdateInformation(string name, string? description, string? sKU, int unitOfMeasureId, decimal unitCost, bool isPerishable, DateTime? expirationDate)
        {
            Name = name;
            Description = description;
            SKU = sKU;
            UnitOfMeasureId = unitOfMeasureId;
            UnitCost = unitCost;
            IsPerishable = isPerishable;
            ExpirationDate = expirationDate;
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
