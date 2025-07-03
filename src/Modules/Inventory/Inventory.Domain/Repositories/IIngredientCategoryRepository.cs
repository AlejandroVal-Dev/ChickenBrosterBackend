using Inventory.Domain.Entities;

namespace Inventory.Domain.Repositories
{
    public interface IIngredientCategoryRepository
    {
        // Reading
        Task<IReadOnlyList<IngredientCategory>> GetAllAsync();
        Task<IReadOnlyList<IngredientCategory>> GetActivesAsync();
        Task<IngredientCategory?> GetByIdAsync(int id);
        Task<IReadOnlyList<IngredientCategory>> GetByAssignedIngredientIdAsync(int ingredientId);

        // Searching
        Task<IReadOnlyList<IngredientCategory>> SearchByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(IngredientCategory category);
        Task UpdateAsync(IngredientCategory category);
        Task DeactivateAsync(IngredientCategory category);
        Task RestoreAsync(IngredientCategory category);

    }
}
