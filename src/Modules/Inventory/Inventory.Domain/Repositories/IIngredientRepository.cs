using Inventory.Domain.Entities;

namespace Inventory.Domain.Repositories
{
    public interface IIngredientRepository
    {
        // Reading
        Task<IReadOnlyList<Ingredient>> GetAllAsync();
        Task<IReadOnlyList<Ingredient>> GetActivesAsync();
        Task<Ingredient?> GetByIdAsync(int id);
        Task<IReadOnlyList<Ingredient>> GetByCategoryAsync(int categoryId);

        // Searching
        Task<IReadOnlyList<Ingredient>> SearchByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(Ingredient ingredient);
        Task UpdateAsync(Ingredient ingredient);
        Task DeactivateAsync(Ingredient ingredient);
        Task RestoreAsync(Ingredient ingredient);

        // From category assign
        Task AssignCategoryAsync(IngredientCategoryAssigment assignment);
        Task UnassignCategoryAsync(IngredientCategoryAssigment assignment);
        Task<IReadOnlyList<int>> GetAssignedCategoryIdsAsync(int ingredientId);
    }
}
