using Security.Application.DTOs.Permission;
using Security.Application.UseCases;
using Security.Application.Util;
using Security.Domain.Entities;
using SharedKernel.Util;

namespace Security.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<PermissionDto>>> GetAllAsync()
        {
            var permissions = await _unitOfWork.Permissions.GetAllAsync();
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

        public async Task<Result<IReadOnlyList<PermissionDto>>> GetActivesAsync()
        {
            var permissions = await _unitOfWork.Permissions.GetActivesAsync();
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

        public async Task<Result<PermissionDto>> GetByIdAsync(int id)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(id);
            if (permission is null)
                return Result<PermissionDto>.Failure("Permission not found.");

            var dto = new PermissionDto
            {
                Id = permission.Id,
                Code = permission.Code,
                Description = permission.Description,
                IsActive = permission.IsActive,
                CreatedAt = permission.CreatedAt,
                UpdatedAt = permission.UpdatedAt
            };

            return Result<PermissionDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<PermissionDto>>> SearchByCodeAsync(string code)
        {
            var permissions = await _unitOfWork.Permissions.SearchByCodeAsync(code);
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

        public async Task<Result<int>> CreateAsync(CreatePermissionDto request)
        {
            if (await _unitOfWork.Permissions.ExistsByCodeAsync(request.Code))
                return Result<int>.Failure("Permission with same code already exists.");

            var permission = new Permission(request.Code, request.Description);
            await _unitOfWork.Permissions.AddAsync(permission);
            await _unitOfWork.CommitAsync();

            var result = new PermissionDto
            {
                Id = permission.Id,
                Code = permission.Code,
                Description = permission.Description,
                IsActive = permission.IsActive,
                CreatedAt = permission.CreatedAt,
                UpdatedAt = permission.UpdatedAt
            };

            return Result<int>.Success(result.Id);
        }

        public async Task<Result> UpdateAsync(UpdatePermissionDto request)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(request.PermissionId);
            if (permission is null)
                return Result<PermissionDto>.Failure("Permission not found.");

            permission.UpdateDescription(request.Description);
            await _unitOfWork.Permissions.UpdateAsync(permission);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(id) ?? throw new Exception("Permission not found.");
            if (permission is null)
                return Result<PermissionDto>.Failure("Permission not found");

            permission.Delete();
            await _unitOfWork.Permissions.DeactivateAsync(permission);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreAsync(int id)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(id);
            if (permission is null)
                return Result<PermissionDto>.Failure("Permission not found.");

            permission.Restore();
            await _unitOfWork.Permissions.RestoreAsync(permission);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

    }
}
