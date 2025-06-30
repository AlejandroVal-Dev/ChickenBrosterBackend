using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;
using System.Xml.Linq;

namespace Sales.Infrastructure.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly SalesDbContext _database;

        public TableRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Table>> GetAllAsync()
        {
            return await _database.Tables
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Table>> GetActivesAsync()
        {
            return await _database.Tables
                .Where(t => t.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _database.Tables
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<Table>> GetByAvailablityAsync(bool isAvailable)
        {
            return await _database.Tables
                .Where(t => t.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Table>> SearchByNumberAsync(string number)
        {
            return await _database.Tables
                .Where(t => t.IsActive && EF.Functions.ILike(t.Number, $"%{number}%"))
                .ToListAsync();
        }

        public async Task<bool> ExistsByNumberAsync(string number)
        {
            return await _database.Tables
                .AnyAsync(u => u.Number == number);
        }

        public async Task AddAsync(Table table)
        {
            await _database.Tables
                .AddAsync(table);
        }

        public Task UpdateAsync(Table table)
        {
            _database.Tables.Update(table);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Table table)
        {
            _database.Tables.Update(table);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Table table)
        {
            _database.Tables.Update(table);
            return Task.CompletedTask;
        }
    }
}
