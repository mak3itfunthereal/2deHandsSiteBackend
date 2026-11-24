using Microsoft.EntityFrameworkCore;
using Eindproject.Models;

namespace Eindproject.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).HasColumnName("id");
            entity.Property(user => user.Username).HasColumnName("username");
            entity.Property(user => user.Email).HasColumnName("email");
            entity.Property(user => user.PasswordHash).HasColumnName("password_hash");
            entity.Property(user => user.IsVerified).HasColumnName("is_verified");
            entity.Property(user => user.CreatedAt).HasColumnName("created_at");
            entity.Property(user => user.Role).HasColumnName("role");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.ToTable("password_reset_tokens");
            entity.HasKey(token => token.Id);
            entity.Property(token => token.Id).HasColumnName("id");
            entity.Property(token => token.UserId).HasColumnName("user_id");
            entity.Property(token => token.TokenHash).HasColumnName("token_hash");
            entity.Property(token => token.ExpiresAt).HasColumnName("expires_at");
            entity.Property(token => token.UsedAt).HasColumnName("used_at");
            entity.HasIndex(token => token.TokenHash).IsUnique();
        });
    }
}
