using Eindproject.DTOs;
using Eindproject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Eindproject.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController(IAccountService accountService) : ControllerBase
{
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(
        ForgotPasswordRequestDto request)
    {
        await accountService.RequestPasswordResetAsync(request.Email);

        return Ok(new
        {
            message = "If an account exists for that email, a reset link has been sent."
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        ResetPasswordRequestDto request)
    {
        var reset = await accountService.ResetPasswordAsync(request);

        if (!reset)
        {
            return BadRequest(new
            {
                message = "The reset token is invalid or expired."
            });
        }

        return Ok(new
        {
            message = "Your password has been reset."
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult<AccountDto>> Register(RegisterRequestDto request)
    {
        var account = await accountService.RegisterAsync(request);

        if (account is null)
        {
            return Conflict(new
            {
                message = "An account with that email already exists."
            });
        }

        return CreatedAtAction(nameof(Me), account);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
    {
        var response = await accountService.LoginAsync(request);

        if (response is null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<AccountDto>> Me()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var user = await accountService.GetByIdAsync(userId);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(new AccountDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsVerified = user.IsVerified,
            CreatedAt = user.CreatedAt,
            Role = user.Role
        });
    }
}
