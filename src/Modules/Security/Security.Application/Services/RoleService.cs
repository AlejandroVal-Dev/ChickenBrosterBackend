using Security.Application.DTOs.Permission;
using Security.Application.DTOs.Role;
using Security.Application.UseCases;
using Security.Application.Util;
using Security.Domain.Entities;
using SharedKernel.Util;
using System.Data;

namespace Security.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<RoleDto>>> GetAllAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            var result = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IsSystem = r.IsSystem,
                IsActive = r.IsActive,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<RoleDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<RoleDto>>> GetActivesAsync()
        {
            var roles = await _unitOfWork.Roles.GetActivesAsync();
            var result = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IsSystem = r.IsSystem,
                IsActive = r.IsActive,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<RoleDto>>.Success(result);
        }

        public async Task<Result<RoleDto>> GetByIdAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            if (role is null)
                return Result<RoleDto>.Failure("Role not found.");

            var dto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsSystem = role.IsSystem,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };

            return Result<RoleDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<PermissionDto>>> GetPermissionsAsync(int roleId)
        {
            var permissions = await _unitOfWork.RolePermissions.GetPermissionsByRoleIdAsync(roleId);
            var result = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Code = p.Code,
                Description = p.Description,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<PermissionDto>>.Success(result);
        }

        public async Task<Result<int>> CreateAsync(CreateRoleDto request)
        {
            if (await _unitOfWork.Roles.ExistsByNameAsync(request.Name))
                return Result<int>.Failure("Role with same name exists.");

            var role = new Role(request.Name, request.Description);
            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CommitAsync();

            var dto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive,
                IsSystem = role.IsSystem,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };

            return Result<int>.Success(dto.Id);
        }

        public async Task<Result> UpdateAsync(UpdateRoleDto request)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId);
            if (role is null)
                return Result<RoleDto>.Failure("Role not found.");

            role.Rename(request.Name);
            role.UpdateDescription(request.Description);

            await _unitOfWork.Roles.UpdateAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            if (role is null)
                return Result<RoleDto>.Failure("Role not found.");

            role.Delete();
            await _unitOfWork.Roles.DeactivateAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            if (role is null)
                return Result<RoleDto>.Failure("Role not found.");

            role.Restore();
            await _unitOfWork.Roles.RestoreAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> AssignPermissionAsync(AssignPermissionDto request)
        {
            if (await _unitOfWork.RolePermissions.ExistsAsync(request.RoleId, request.PermissionId))
                return Result.Success();

            var rolePerm = new RolePermission(request.RoleId, request.PermissionId);
            await _unitOfWork.RolePermissions.AddAsync(rolePerm);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RemovePermissionAsync(AssignPermissionDto request)
        {
            var rolePerm = new RolePermission(request.RoleId, request.PermissionId);
            await _unitOfWork.RolePermissions.RemoveAsync(rolePerm);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

    }
}
