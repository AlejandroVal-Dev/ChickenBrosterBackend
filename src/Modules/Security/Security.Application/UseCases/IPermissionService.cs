using Security.Application.DTOs.Permission;
using SharedKernel.Util;

namespace Security.Application.UseCases
{
    public interface IPermissionService
    {
        Task<Result<IReadOnlyList<PermissionDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<PermissionDto>>> GetActivesAsync();
        Task<Result<PermissionDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<PermissionDto>>> SearchByCodeAsync(string code);
        Task<Result<int>> CreateAsync(CreatePermissionDto request);
        Task<Result> UpdateAsync(UpdatePermissionDto request);
        Task<Result> DeleteAsync(int id);
        Task<Result> RestoreAsync(int id);
    }
}
