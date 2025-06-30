using Sales.Domain.Entities;

namespace Sales.Domain.Repositories
{
    public interface IProductRecipeRepository
    {
        // Reading
        Task<IReadOnlyList<ProductRecipe>> GetAllAsync();
        Task<IReadOnlyList<ProductRecipe>> GetActivesAsync();
        Task<ProductRecipe?> GetByProductIdAsync(int productId);
        
        // Writting
        Task AddAsync(ProductRecipe productRecipe);
        Task UpdateAsync(ProductRecipe productRecipe);
        Task DeactivateAsync(ProductRecipe productRecipe);
        Task RestoreAsync(ProductRecipe productRecipe);
    }
}
