namespace Inventory.Domain.Entities
{
    public class UnitOfMeasure
    {
        public int Id { get; private set; }

        // Core properties
        public string Name { get; private set; } = null!;
        public string? Abbreviation { get; private set; }
        public bool IsDecimal { get; private set; }
        public decimal ConversionFactor { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public UnitOfMeasure(string name, string? abbreviation, bool isDecimal, decimal conversionFactor)
        {
            Name = name;
            Abbreviation = abbreviation;
            IsDecimal = isDecimal;
            ConversionFactor = conversionFactor;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private UnitOfMeasure() { }

        // Public methods
        public void UpdateInformation(string name, string? abbreviation, bool isDecimal, decimal conversionFactor)
        {
            Name = name;
            Abbreviation = abbreviation;
            IsDecimal = isDecimal;
            ConversionFactor = conversionFactor;
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
