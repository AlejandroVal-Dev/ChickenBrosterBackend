using Sales.Application.DTOs.Product;
using Sales.Application.DTOs.ProductCategoryAssignment;
using SharedKernel.Util;

namespace Sales.Application.UseCases
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<ProductDto>>> GetActivesAsync();
        Task<Result<ProductDto>> GetByIdAsync(int productId);
        Task<Result<IReadOnlyList<ProductDto>>> GetByCategoryIdAsync(int categoryId);
        Task<Result<IReadOnlyList<ProductDto>>> SearchByNameAsync(string name);
        Task<Result<int>> CreateProductAsync(CreateProductDto request);
        Task<Result> UpdateProductAsync(UpdateProductDto request);
        Task<Result> DeleteProductAsync(int productId);
        Task<Result> RestoreProductAsync(int productId);
        Task<Result> AssignCategoryAsync(ProductCategoryAssignmentDto request);
        Task<Result> UnassignCategoryAsync(ProductCategoryAssignmentDto request);
    }
}
