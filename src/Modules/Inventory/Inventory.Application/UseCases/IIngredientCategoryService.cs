using Inventory.Application.DTOs.IngredientCategory;
using SharedKernel.Util;

namespace Inventory.Application.UseCases
{
    public interface IIngredientCategoryService
    {
        Task<Result<IReadOnlyList<IngredientCategoryDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<IngredientCategoryDto>>> GetActivesAsync();
        Task<Result<IngredientCategoryDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<IngredientCategoryDto>>> GetByIngredientIdAsync(int ingredientId);
        Task<Result<IReadOnlyList<IngredientCategoryDto>>> SearchByNameAsync(string name);
        Task<Result<int>> CreateAsync(CreateIngredientCategoryDto request);
        Task<Result> UpdateAsync(UpdateIngredientCategoryDto request);
        Task<Result> DeleteAsync(int id);
        Task<Result> RestoreAsync(int id);
        Task<Result<IReadOnlyList<int>>> GetAssignedByIngredient(int ingredientId);
    }
}
