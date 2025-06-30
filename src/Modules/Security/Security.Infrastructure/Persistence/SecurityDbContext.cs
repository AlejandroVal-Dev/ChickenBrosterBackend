using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using SharedKernel.Entities;

namespace Security.Infrastructure.Persistence
{
    public class SecurityDbContext : DbContext
    {
        public DbSet<Person> People => Set<Person>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecurityDbContext).Assembly);
        }
    }
}
