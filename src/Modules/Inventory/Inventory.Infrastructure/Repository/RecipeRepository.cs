using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly InventoryDbContext _database;

        public RecipeRepository(InventoryDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Recipe>> GetAllAsync()
        {
            return await _database.Recipes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Recipe>> GetActivesAsync()
        {
            return await _database.Recipes
                .Where(r => r.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _database.Recipes
                .FindAsync(id);
        }

        public async Task<Recipe?> GetByNameAsync(string name)
        {
            return await _database.Recipes
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _database.Recipes
                .AnyAsync(r => r.Name == name);
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _database.Recipes
                .AddAsync(recipe);
        }

        public Task UpdateAsync(Recipe recipe)
        {
            _database.Recipes.Update(recipe);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Recipe recipe)
        {
            _database.Recipes.Update(recipe);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Recipe recipe)
        {
            _database.Recipes.Update(recipe);
            return Task.CompletedTask;
        }
    }
}
