using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Repositories;
using Security.Infrastructure.Persistence;

namespace Security.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SecurityDbContext _database;

        public RoleRepository(SecurityDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync()
        {
            return await _database.Roles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Role>> GetActivesAsync()
        {
            return await _database.Roles
                .Where(r => r.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _database.Roles
                .FindAsync(id);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _database.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _database.Roles
                .AnyAsync(r => r.Name == name);
        }

        public async Task AddAsync(Role role)
        {
            await _database.Roles
                .AddAsync(role);
        }

        public Task UpdateAsync(Role role)
        {
            _database.Roles.Update(role);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Role role)
        {
            _database.Roles.Update(role);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Role role)
        {
            _database.Roles.Update(role);
            return Task.CompletedTask;
        }
    }
}
