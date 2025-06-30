using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repository
{
    public class ProductRecipeRepository : IProductRecipeRepository
    {
        private readonly SalesDbContext _database;

        public ProductRecipeRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<ProductRecipe>> GetAllAsync()
        {
            return await _database.ProductRecipes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductRecipe>> GetActivesAsync()
        {
            return await _database.ProductRecipes
                .Where(pr => pr.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductRecipe?> GetByProductIdAsync(int productId)
        {
            return await _database.ProductRecipes
                .Where(pr => pr.ProductId == productId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(ProductRecipe productRecipe)
        {
            await _database.ProductRecipes
                .AddAsync(productRecipe);
        }

        public Task UpdateAsync(ProductRecipe productRecipe)
        {
            _database.ProductRecipes.Update(productRecipe);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(ProductRecipe productRecipe)
        {
            _database.ProductRecipes.Update(productRecipe);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(ProductRecipe productRecipe)
        {
            _database.ProductRecipes.Update(productRecipe);
            return Task.CompletedTask;
        }
    }
}
