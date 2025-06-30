namespace Sales.Application.DTOs.Product
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string NewName { get; set; } = null!;
        public string? NewDescription { get; set; }
        public decimal NewPrice { get; set; }
    }
}
