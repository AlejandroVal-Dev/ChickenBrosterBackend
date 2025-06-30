using Inventory.Application.DTOs.Ingredient;
using Inventory.Application.DTOs.IngredientCategoryAssignment;
using SharedKernel.Util;

namespace Inventory.Application.UseCases
{
    public interface IIngredientService
    {
        Task<Result<IReadOnlyList<IngredientDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<IngredientDto>>> GetActivesAsync();
        Task<Result<IngredientDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<IngredientDto>>> GetByCategoryIdAsync(int categoryId);
        Task<Result<IReadOnlyList<IngredientDto>>> SearchByNameAsync(string name);
        Task<Result<int>> CreateAsync(CreateIngredientDto request);
        Task<Result> UpdateAsync(UpdateIngredientDto request);
        Task<Result> DeleteAsync(int id);
        Task<Result> RestoreAsync(int id);
        Task<Result> AssignCategoryAsync(IngredientCategoryAssignmentDto request);
        Task<Result> UnassignCategoryAsync(IngredientCategoryAssignmentDto request);
    }
}
