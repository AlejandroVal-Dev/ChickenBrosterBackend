using Security.Domain.Entities;

namespace Security.Domain.Repositories
{
    public interface IRolePermissionRepository
    {
        // Reading
        Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId);
        Task<IReadOnlyList<Role>> GetRolesByPermissionIdAsync(int permissionId);

        // Validation
        Task<bool> ExistsAsync(int roleId, int permissionId);

        // Writting
        Task AddAsync(RolePermission rolePermission);
        Task RemoveAsync(RolePermission rolePermission);
    }
}
