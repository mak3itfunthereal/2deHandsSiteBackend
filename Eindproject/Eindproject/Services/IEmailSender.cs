namespace Eindproject.Services;

public interface IEmailSender
{
    Task SendPasswordResetAsync(string email, string resetLink);
}
