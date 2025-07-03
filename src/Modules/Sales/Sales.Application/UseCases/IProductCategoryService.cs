using Sales.Application.DTOs.ProductCategory;
using SharedKernel.Util;

namespace Sales.Application.UseCases
{
    public interface IProductCategoryService
    {
        Task<Result<IReadOnlyList<ProductCategoryDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<ProductCategoryDto>>> GetActivesAsync();
        Task<Result<ProductCategoryDto>> GetByIdAsync(int categoryId);
        Task<Result<IReadOnlyList<ProductCategoryDto>>> SearchByNameAsync(string name);
        Task<Result<int>> CreateCategoryAsync(CreateProductCategoryDto request);
        Task<Result> UpdateCategoryAsync(UpdateProductCategoryDto request);
        Task<Result> DeleteCategoryAsync(int categoryId);
        Task<Result> RestoreCategoryAsync(int categoryId);
        Task<Result<IReadOnlyList<int>>> GetAssignedByProduct(int productId);
    }
}
