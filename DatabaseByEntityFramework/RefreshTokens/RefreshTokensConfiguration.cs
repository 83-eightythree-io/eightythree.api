using Business.RefreshTokens;
using Business.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseByEntityFramework.RefreshTokens;

public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder
            .Property(rf => rf.Token);

        builder
            .Property(rf => rf.CreatedAt);

        builder
            .Property(rf => rf.ExpiresAt);

        builder
            .HasOne<User>()
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rf => rf.UserId);
    }
}