namespace Sales.Application.DTOs.ProductCategory
{
    public class CreateProductCategoryDto
    {
        public string Name { get; set; } = null!;
        public int? ParentCategoryId { get; set; }
    }
}
