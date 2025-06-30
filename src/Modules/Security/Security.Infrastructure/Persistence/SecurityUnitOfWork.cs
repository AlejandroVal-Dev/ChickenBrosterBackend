using Security.Application.Util;
using Security.Domain.Repositories;
using Security.Infrastructure.Repositories;

namespace Security.Infrastructure.Persistence
{
    public class SecurityUnitOfWork : IUnitOfWork
    {
        private readonly SecurityDbContext _database;

        public IUserRepository Users { get; }

        public IRoleRepository Roles { get; }

        public IPermissionRepository Permissions { get; }

        public IRolePermissionRepository RolePermissions { get; }

        public IPersonRepository People { get; }

        public SecurityUnitOfWork(SecurityDbContext database)
        {
            _database = database;
            Users = new UserRepository(_database);
            Roles = new RoleRepository(_database);
            Permissions = new PermissionRepository(_database);
            RolePermissions = new RolePermissionRepository(_database);
            People = new PersonRepository(_database);
        }

        public Task<int> CommitAsync() => _database.SaveChangesAsync();

    }
}
