using Security.Domain.Repositories;

namespace Security.Application.Util
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IRolePermissionRepository RolePermissions { get; }
        IPersonRepository People { get; }
        Task<int> CommitAsync();
    }
}
