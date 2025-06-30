namespace Inventory.Application.DTOs.UnitOfMeasure
{
    public class CreateUnitOfMeasureDto
    {
        public string Name { get; set; } = null!;
        public string? Abbreviation { get; set; }
        public bool IsDecimal { get; set; }
        public decimal ConversionFactor { get; set; }
    }
}
