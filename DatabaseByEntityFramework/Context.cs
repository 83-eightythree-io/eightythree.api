using Business.RefreshTokens;
using Business.Users;
using DatabaseByEntityFramework.Users;
using Microsoft.EntityFrameworkCore;

namespace DatabaseByEntityFramework;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    public DbSet<User?> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}