using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository
{
    public class InventoryMovementRepository : IInventoryMovementRepository
    {
        private readonly InventoryDbContext _database;

        public InventoryMovementRepository(InventoryDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<InventoryMovement>> GetAllAsync()
        {
            return await _database.InventoryMovements
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<InventoryMovement?> GetByIdAsync(int id)
        {
            return await _database.InventoryMovements
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<InventoryMovement>> GetFilteredAsync(int? ingredientId, DateTime? from, DateTime? to, MovementType? movementType, int? madeByUserId)
        {
            var query = _database.InventoryMovements.AsQueryable();

            if (ingredientId.HasValue)
                query = query.Where(m => m.IngredientId == ingredientId.Value);

            if (from.HasValue)
                query = query.Where(m => m.MovementDate >= from.Value);

            if (to.HasValue)
                query = query.Where(m => m.MovementDate <= to.Value);

            if (movementType.HasValue)
                query = query.Where(m => m.MovementType == movementType.Value);

            if (madeByUserId.HasValue)
                query = query.Where(m => m.MadeByUserId == madeByUserId.Value);

            return await query
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync();
        }

        public async Task AddAsync(InventoryMovement movement)
        {
            await _database.InventoryMovements
                .AddAsync(movement);
        }
    }
}
