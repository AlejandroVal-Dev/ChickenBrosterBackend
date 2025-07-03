using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly InventoryDbContext _database;

        public IngredientRepository(InventoryDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Ingredient>> GetAllAsync()
        {
            return await _database.Ingredients
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Ingredient>> GetActivesAsync()
        {
            return await _database.Ingredients
                .Where(i => i.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ingredient?> GetByIdAsync(int id)
        {
            return await _database.Ingredients
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<Ingredient>> GetByCategoryAsync(int categoryId)
        {
            return await (
                from i in _database.Ingredients
                join a in _database.IngredientCategoryAssignments
                    on i.Id equals a.IngredientId
                where a.CategoryId == categoryId && i.IsActive
                select i
            ).ToListAsync();
        }

        public async Task<IReadOnlyList<Ingredient>> SearchByNameAsync(string name)
        {
            return await _database.Ingredients
                .Where(i => i.IsActive && EF.Functions.ILike(i.Name, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _database.Ingredients
                .AnyAsync(i => i.Name == name);
        }

        public async Task AddAsync(Ingredient ingredient)
        {
            await _database.Ingredients
                .AddAsync(ingredient);
        }

        public Task UpdateAsync(Ingredient ingredient)
        {
            _database.Ingredients.Update(ingredient);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Ingredient ingredient)
        {
            _database.Ingredients.Update(ingredient);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Ingredient ingredient)
        {
            _database.Ingredients.Update(ingredient);
            return Task.CompletedTask;
        }

        public async Task AssignCategoryAsync(IngredientCategoryAssigment assignment)
        {
            var exists = await _database.IngredientCategoryAssignments
                .AnyAsync(a => a.IngredientId == assignment.IngredientId && a.CategoryId == assignment.CategoryId);

            if (!exists)
            {
                var newAssignment = new IngredientCategoryAssigment(assignment.IngredientId, assignment.CategoryId);
                _database.IngredientCategoryAssignments.Add(assignment);
                await _database.SaveChangesAsync();
            }
        }

        public async Task UnassignCategoryAsync(IngredientCategoryAssigment assignment)
        {
            var newAssignment = await _database.IngredientCategoryAssignments
                .FirstOrDefaultAsync(a => a.IngredientId == assignment.IngredientId && a.CategoryId == assignment.CategoryId);

            if (assignment != null)
            {
                _database.IngredientCategoryAssignments.Remove(assignment);
                await _database.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<int>> GetAssignedCategoryIdsAsync(int ingredientId)
        {
            return await _database.IngredientCategoryAssignments
                .Where(a => a.IngredientId == ingredientId)
                .Select(a => a.CategoryId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<RecipeIngredient>> GetByRecipeId(int recipeId)
        {
            return await _database.RecipeIngredients
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();
        }
    }
}
