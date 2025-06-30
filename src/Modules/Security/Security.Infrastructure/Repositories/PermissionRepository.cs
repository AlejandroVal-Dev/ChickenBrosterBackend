using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Repositories;
using Security.Infrastructure.Persistence;
using System;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Security.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly SecurityDbContext _database;

        public PermissionRepository(SecurityDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Permission>> GetAllAsync()
        {
            return await _database.Permissions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Permission>> GetActivesAsync()
        {
            return await _database.Permissions
                .Where(p => p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _database.Permissions
                .FindAsync(id);
        }

        public async Task<Permission?> GetByCodeAsync(string code)
        {
            return await _database.Permissions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IReadOnlyList<Permission>> SearchByCodeAsync(string code)
        {
            return await _database.Permissions
                .Where(p => p.IsActive && EF.Functions.ILike(p.Code, $"%{code}%"))
                .ToListAsync();
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _database.Permissions
                .AnyAsync(p => p.Code == code);
        }

        public async Task AddAsync(Permission permission)
        {
            await _database.Permissions
                .AddAsync(permission);
        }

        public Task UpdateAsync(Permission permission)
        {
            _database.Permissions.Update(permission);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Permission permission)
        {
            _database.Permissions.Update(permission);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Permission permission)
        {
            _database.Permissions.Update(permission);
            return Task.CompletedTask;
        }
    }
}
