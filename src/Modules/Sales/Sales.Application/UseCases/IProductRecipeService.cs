using Sales.Application.DTOs.ProductRecipe;
using SharedKernel.Util;

namespace Sales.Application.UseCases
{
    public interface IProductRecipeService
    {
        Task<Result<IReadOnlyList<ProductRecipeDto>>> GetAllAsync();
        Task<Result<ProductRecipeDto>> GetByProductIdAsync(int id);
        Task<Result<int>> CreateAsync(CreateProductRecipeDto request);
        Task<Result> UpdateQuantityAsync(UpdateProductRecipeDto request);
        Task<Result> DeleteRecipeAsync(int productRecipeId);
        Task<Result> RestoreRecipeAsync(int productRecipeId);
    }
}
