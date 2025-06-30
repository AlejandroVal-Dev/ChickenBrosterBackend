using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository
{
    public class IngredientCategoryRepository : IIngredientCategoryRepository
    {
        private readonly InventoryDbContext _database;

        public IngredientCategoryRepository(InventoryDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<IngredientCategory>> GetAllAsync()
        {
            return await _database.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<IngredientCategory>> GetActivesAsync()
        {
            return await _database.Categories
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IngredientCategory?> GetByIdAsync(int id)
        {
            return await _database.Categories
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<IngredientCategory>> GetByAssignedIngredientIdAsync(int ingredientId) 
        {
            var query = from c in _database.Categories
                        join a in _database.IngredientCategoryAssignments
                            on c.Id equals a.CategoryId
                        where a.IngredientId == ingredientId
                        select c;

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<IngredientCategory>> SearchByNameAsync(string name)
        {
            return await _database.Categories
                .Where(c => c.IsActive && EF.Functions.ILike(c.Name, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _database.Categories
                .AnyAsync(c => c.Name == name);
        }

        public async Task AddAsync(IngredientCategory category)
        {
            await _database.Categories
                .AddAsync(category);
        }

        public Task UpdateAsync(IngredientCategory category)
        {
            _database.Categories.Update(category);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(IngredientCategory category)
        {
            _database.Categories.Update(category);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(IngredientCategory category)
        {
            _database.Categories.Update(category);
            return Task.CompletedTask;
        }

    }
}
