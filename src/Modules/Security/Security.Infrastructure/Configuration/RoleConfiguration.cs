using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Domain.Entities;

namespace Security.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(200);

            builder.Property(r => r.IsSystem)
                .IsRequired();

            builder.Property(r => r.IsActive)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.UpdatedAt);

            builder.HasIndex(r => r.Name).IsUnique();
        }
    }
}
