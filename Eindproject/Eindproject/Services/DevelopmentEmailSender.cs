using Microsoft.Extensions.Logging;

namespace Eindproject.Services;

public class DevelopmentEmailSender(ILogger<DevelopmentEmailSender> logger)
    : IEmailSender
{
    public Task SendPasswordResetAsync(string email, string resetLink)
    {
        logger.LogWarning(
            "Development password reset email for {Email}. Reset link: {ResetLink}",
            email,
            resetLink);

        return Task.CompletedTask;
    }
}
