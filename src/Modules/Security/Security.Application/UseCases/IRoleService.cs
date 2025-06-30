using Security.Application.DTOs.Permission;
using Security.Application.DTOs.Role;
using SharedKernel.Util;

namespace Security.Application.UseCases
{
    public interface IRoleService
    {
        Task<Result<IReadOnlyList<RoleDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<RoleDto>>> GetActivesAsync();
        Task<Result<RoleDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<PermissionDto>>> GetPermissionsAsync(int roleId);
        Task<Result<int>> CreateAsync(CreateRoleDto request);
        Task<Result> UpdateAsync(UpdateRoleDto request);
        Task<Result> DeleteAsync(int id);
        Task<Result> RestoreAsync(int id);
        Task<Result> AssignPermissionAsync(AssignPermissionDto request);
        Task<Result> RemovePermissionAsync(AssignPermissionDto request);
    }
}
