using Eindproject.DTOs;
using Eindproject.Models;

namespace Eindproject.Repositories;

public interface IAccountRepository
{
    Task<User?> GetByEmailAsync(String email);

    Task<User?> GetByIdAsync(int id);

    Task<bool> EmailExistsAsync(string email);

    Task<User> CreateAsync(User user);

    Task<PasswordResetToken?> GetPasswordResetTokenAsync(string tokenHash);

    Task AddPasswordResetTokenAsync(PasswordResetToken token);

    Task CompletePasswordResetAsync(
        User user,
        PasswordResetToken token,
        string passwordHash);
}