using Business.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseByEntityFramework.Users;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.Email)
            .HasConversion(e => e.Value, e => new Email(e));

        builder
            .Property(u => u.Name);

        builder
            .Property(u => u.CreatedAt);

        builder
            .Property(u => u.Password)
            .HasConversion(p => p.Value, p => new Password(p, s => s));

        builder
            .Property(u => u.Deleted);

        builder
            .Property(u => u.DeletedAt);

        builder
            .Property(u => u.Role)
            .HasConversion(r => r.Value, r => new Role(r));
    }
}