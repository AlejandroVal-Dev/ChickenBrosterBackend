using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly SalesDbContext _database;

        public ProductCategoryRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetAllAsync()
        {
            return await _database.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductCategory>> GetActivesAsync()
        {
            return await _database.Categories
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            return await _database.Categories
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductCategory>> GetChildrenAsync(int parentId)
        {
            return await _database.Categories
                .Where(pc => pc.ParentCategoryId == parentId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductCategory>> SearchByNameAsync(string name)
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

        public async Task AddAsync(ProductCategory category)
        {
            await _database.Categories
                .AddAsync(category);
        }

        public Task UpdateAsync(ProductCategory category)
        {
            _database.Categories.Update(category);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(ProductCategory category)
        {
            _database.Categories.Update(category);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(ProductCategory category)
        {
            _database.Categories.Update(category);
            return Task.CompletedTask;
        }

    }
}
