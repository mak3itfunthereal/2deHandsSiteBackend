using Eindproject.Data;
using Eindproject.Models;
using Microsoft.EntityFrameworkCore;

namespace Eindproject.Repositories;

public class AccountRepository(ApplicationDbContext context)
    : IAccountRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var normalizedEmail = email.Trim().ToLower();

        return await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user =>
                user.Email.ToLower() == normalizedEmail);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var normalizedEmail = email.Trim().ToLower();

        return await context.Users
            .AnyAsync(user => user.Email.ToLower() == normalizedEmail);
    }

    public async Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public Task<PasswordResetToken?> GetPasswordResetTokenAsync(string tokenHash)
    {
        return context.PasswordResetTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(token => token.TokenHash == tokenHash);
    }

    public async Task AddPasswordResetTokenAsync(PasswordResetToken token)
    {
        context.PasswordResetTokens.Add(token);
        await context.SaveChangesAsync();
    }

    public async Task CompletePasswordResetAsync(
        User user,
        PasswordResetToken token,
        string passwordHash)
    {
        user.PasswordHash = passwordHash;
        token.UsedAt = DateTime.UtcNow;

        context.Users.Update(user);
        context.PasswordResetTokens.Update(token);
        await context.SaveChangesAsync();
    }
}