using Eindproject.DTOs;
using Eindproject.Models;
using Eindproject.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;

namespace Eindproject.Services;

public class AccountService(
    IAccountRepository accountRepository,
    IConfiguration configuration,
    IEmailSender emailSender)
    : IAccountService
{
    public string HashPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public async Task<User?> ValidateLoginAsync(LoginRequestDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Email);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Password);

        var user = await accountRepository.GetByEmailAsync(request.Email);

        if (user is null || !user.IsVerified)
        {
            return null;
        }

        return VerifyPassword(request.Password, user.PasswordHash)
            ? user
            : null;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return accountRepository.GetByIdAsync(id);
    }

    public async Task<AccountDto?> RegisterAsync(RegisterRequestDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var username = request.Username.Trim();
        var email = request.Email.Trim().ToLowerInvariant();

        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Password);

        if (request.Password.Length < 8)
        {
            throw new ArgumentException(
                "Password must contain at least 8 characters.",
                nameof(request.Password));
        }

        if (await accountRepository.EmailExistsAsync(email))
        {
            return null;
        }

        var user = await accountRepository.CreateAsync(new User
        {
            Username = username,
            Email = email,
            PasswordHash = HashPassword(request.Password),
            IsVerified = true,
            CreatedAt = DateTime.UtcNow,
            Role = "user"
        });

        return new AccountDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsVerified = user.IsVerified,
            CreatedAt = user.CreatedAt,
            Role = user.Role
        };
    }

    public async Task RequestPasswordResetAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var normalizedEmail = email.Trim().ToLowerInvariant();
        var user = await accountRepository.GetByEmailAsync(normalizedEmail);

        if (user is null)
        {
            return;
        }

        var token = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(32));
        var tokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        var expirationMinutes = configuration.GetValue<int>(
            "PasswordReset:ExpirationMinutes",
            30);
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        await accountRepository.AddPasswordResetTokenAsync(new PasswordResetToken
        {
            UserId = user.Id,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt
        });

        var frontendUrl = configuration["PasswordReset:FrontendUrl"]
            ?? throw new InvalidOperationException(
                "PasswordReset:FrontendUrl is not configured.");
        var resetLink = $"{frontendUrl}?token={Uri.EscapeDataString(token)}";

        await emailSender.SendPasswordResetAsync(user.Email, resetLink);
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Token)
            || string.IsNullOrWhiteSpace(request.NewPassword)
            || request.NewPassword.Length < 8)
        {
            return false;
        }

        var tokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(request.Token)));
        var resetToken = await accountRepository
            .GetPasswordResetTokenAsync(tokenHash);

        if (resetToken is null
            || resetToken.UsedAt is not null
            || resetToken.ExpiresAt <= DateTime.UtcNow)
        {
            return false;
        }

        var user = await accountRepository.GetByIdAsync(resetToken.UserId);

        if (user is null)
        {
            return false;
        }

        await accountRepository.CompletePasswordResetAsync(
            user,
            resetToken,
            HashPassword(request.NewPassword));

        return true;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await ValidateLoginAsync(request);

        if (user is null)
        {
            return null;
        }

        var expirationMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes", 60);
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);
        var key = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is not configured.");
        var issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt:Issuer is not configured.");
        var audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt:Audience is not configured.");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt
        };
    }
}