using Eindproject.DTOs;
using Eindproject.Models;

namespace Eindproject.Services;

public interface IAccountService
{
    Task<User?> ValidateLoginAsync(LoginRequestDto request);

    Task<User?> GetByIdAsync(int id);

    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

    Task<AccountDto?> RegisterAsync(RegisterRequestDto request);

    Task RequestPasswordResetAsync(string email);

    Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);
}