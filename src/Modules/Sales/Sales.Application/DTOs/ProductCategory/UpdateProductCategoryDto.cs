namespace Sales.Application.DTOs.ProductCategory
{
    public class UpdateProductCategoryDto
    {
        public int CategoryId { get; set; }
        public string NewName { get; set; } = null!;
    }
}
