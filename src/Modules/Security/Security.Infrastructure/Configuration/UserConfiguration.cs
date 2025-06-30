using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Domain.Entities;
using SharedKernel.Entities;

namespace Security.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.PersonId)
                .IsRequired();

            builder.Property(u => u.RoleId)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt);

            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(u => u.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Role>()
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Username).IsUnique();
        }
    }
}
