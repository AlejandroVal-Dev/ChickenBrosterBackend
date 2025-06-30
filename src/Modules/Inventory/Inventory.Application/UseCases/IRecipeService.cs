using Inventory.Application.DTOs.Recipe;
using Inventory.Application.DTOs.RecipeIngredient;
using SharedKernel.Util;

namespace Inventory.Application.UseCases
{
    public interface IRecipeService
    {
        Task<Result<IReadOnlyList<RecipeDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<RecipeDto>>> GetActivesAsync();
        Task<Result<RecipeDto>> GetByIdAsync(int id);
        Task<Result<RecipeDto>> GetByNameAsync(string name);
        Task<Result<int>> CreateAsync(CreateRecipeDto request);
        Task<Result> UpdateAsync(UpdateRecipeDto request);
        Task<Result> DeleteAsync(int id);
        Task<Result> RestoreAsync(int id);
        Task<Result> AddIngredientAsync(int recipeId, CreateRecipeIngredientDto request);
        Task<Result> RemoveIngredientAsync(int recipeId, int ingredientId);
        Task<Result> UpdateIngredientQuantityAsync(int recipeId, UpdateRecipeIngredientQuantityDto request);
    }
}
