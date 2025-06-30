namespace Sales.Application.DTOs.ProductCategory
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
