using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository
{
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private readonly InventoryDbContext _database;

        public UnitOfMeasureRepository(InventoryDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<UnitOfMeasure>> GetAllAsync()
        {
            return await _database.UnitsOfMeasure
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<UnitOfMeasure>> GetActivesAsync()
        {
            return await _database.UnitsOfMeasure
                .Where(u => u.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UnitOfMeasure?> GetByIdAsync(int id)
        {
            return await _database.UnitsOfMeasure
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<UnitOfMeasure>> SearchByNameAsync(string name)
        {
            return await _database.UnitsOfMeasure
                .Where(u => u.IsActive && EF.Functions.ILike(u.Name, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _database.UnitsOfMeasure
                .AnyAsync(u => u.Name == name);
        }

        public async Task AddAsync(UnitOfMeasure unitOfMeasure)
        {
            await _database.UnitsOfMeasure
                .AddAsync(unitOfMeasure);
        }

        public Task UpdateAsync(UnitOfMeasure unitOfMeasure)
        {
            _database.UnitsOfMeasure.Update(unitOfMeasure);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(UnitOfMeasure unitOfMeasure)
        {
            _database.UnitsOfMeasure.Update(unitOfMeasure);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(UnitOfMeasure unitOfMeasure)
        {
            _database.UnitsOfMeasure.Update(unitOfMeasure);
            return Task.CompletedTask;
        }
    }
}
