namespace Inventory.Application.DTOs.UnitOfMeasure
{
    public class UnitOfMeasureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Abbreviation { get; set; }
        public bool IsDecimal { get; set; }
        public decimal ConversionFactor { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
