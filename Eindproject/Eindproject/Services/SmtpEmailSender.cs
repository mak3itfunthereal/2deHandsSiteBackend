using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Eindproject.Models;

namespace Eindproject.Services;

#pragma warning disable SYSLIB0014
public class SmtpEmailSender(IOptions<EmailOptions> options) : IEmailSender
{
    private readonly EmailOptions emailOptions = options.Value;

    public async Task SendPasswordResetAsync(string email, string resetLink)
    {
        ValidateOptions();

        using var message = new MailMessage
        {
            From = new MailAddress(emailOptions.FromAddress, emailOptions.FromName),
            Subject = "Reset your password",
            Body = $"<p>Use the following link to reset your password:</p>"
                + $"<p><a href=\"{resetLink}\">Reset password</a></p>",
            IsBodyHtml = true
        };
        message.To.Add(email);

        using var client = new SmtpClient(emailOptions.Host, emailOptions.Port)
        {
            EnableSsl = emailOptions.EnableSsl,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                emailOptions.Username,
                emailOptions.Password)
        };

        await client.SendMailAsync(message);
    }

    private void ValidateOptions()
    {
        if (string.IsNullOrWhiteSpace(emailOptions.Host)
            || emailOptions.Port <= 0
            || string.IsNullOrWhiteSpace(emailOptions.FromAddress))
        {
            throw new InvalidOperationException(
                "Email SMTP settings are incomplete.");
        }
    }
}
#pragma warning restore SYSLIB0014
