using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Entities;

namespace Security.Infrastructure.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LastName1)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LastName2)
                .HasMaxLength(100);

            builder.Property(p => p.DocumentId)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.DocumentType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .HasMaxLength(120);

            builder.Property(p => p.PersonType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt);

            builder.HasIndex(p => p.DocumentId).IsUnique();
            builder.HasIndex(p => p.PhoneNumber);
            builder.HasIndex(p => p.Email);
        }
    }
}
