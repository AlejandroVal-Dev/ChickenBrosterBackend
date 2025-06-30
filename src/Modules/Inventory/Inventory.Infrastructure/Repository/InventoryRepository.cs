using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _database;

        public InventoryRepository(InventoryDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Domain.Entities.Inventory>> GetAllAsync()
        {
            return await _database.Inventories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.Inventory>> GetActivesAsync()
        {
            return await _database.Inventories
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Domain.Entities.Inventory?> GetByIngredientIdAsync(int ingredientId)
        {
            return await _database.Inventories
                .FindAsync(ingredientId);
        }

        public async Task<IReadOnlyList<Domain.Entities.Inventory>> GetLowStockAsync()
        {
            return await _database.Inventories
                .Where(i => i.IsActive && i.MinimumStock.HasValue && i.ActualStock < i.MinimumStock.Value)
                .ToListAsync();
        }

        public async Task AddAsync(Domain.Entities.Inventory inventory)
        {
            await _database.Inventories
                .AddAsync(inventory);
        }

        public Task UpdateAsync(Domain.Entities.Inventory inventory)
        {
            _database.Inventories.Update(inventory);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Domain.Entities.Inventory inventory)
        {
            _database.Inventories.Update(inventory);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Domain.Entities.Inventory inventory)
        {
            _database.Inventories.Update(inventory);
            return Task.CompletedTask;
        }
    }
}
