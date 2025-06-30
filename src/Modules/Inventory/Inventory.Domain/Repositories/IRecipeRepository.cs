using Inventory.Domain.Entities;

namespace Inventory.Domain.Repositories
{
    public interface IRecipeRepository
    {
        // Reading
        Task<IReadOnlyList<Recipe>> GetAllAsync();
        Task<IReadOnlyList<Recipe>> GetActivesAsync();
        Task<Recipe?> GetByIdAsync(int id);
        Task<Recipe?> GetByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(Recipe recipe);
        Task UpdateAsync(Recipe recipe);
        Task DeactivateAsync(Recipe recipe);
        Task RestoreAsync(Recipe recipe);
    }
}
