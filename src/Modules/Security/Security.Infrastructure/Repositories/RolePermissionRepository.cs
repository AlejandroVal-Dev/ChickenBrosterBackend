using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Repositories;
using Security.Infrastructure.Persistence;

namespace Security.Infrastructure.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly SecurityDbContext _database;

        public RolePermissionRepository(SecurityDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            return await _database.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Join(_database.Permissions,
                      rp => rp.PermissionId,
                      p => p.Id,
                      (rp, p) => p)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Role>> GetRolesByPermissionIdAsync(int permissionId)
        {
            return await _database.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Join(_database.Roles,
                      rp => rp.RoleId,
                      r => r.Id,
                      (rp, r) => r)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int roleId, int permissionId)
        {
            return await _database.RolePermissions
            .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public async Task AddAsync(RolePermission rolePermission)
        {
            await _database.RolePermissions
                .AddAsync(rolePermission);
        }

        public Task RemoveAsync(RolePermission rolePermission)
        {
            _database.RolePermissions.Remove(rolePermission);
            return Task.CompletedTask;
        }
    }
}
