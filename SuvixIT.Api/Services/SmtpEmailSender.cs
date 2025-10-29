using System.Net;
using System.Net.Mail;
using System.Text;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Services;

public class SmtpEmailSender : IEmailSender
{
    public async Task SendDemoRequestNotificationAsync(EmailConfiguration configuration, DemoRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(configuration.SmtpHost) || string.IsNullOrWhiteSpace(configuration.RecipientAddress))
        {
            return;
        }

        using var client = new SmtpClient(configuration.SmtpHost, configuration.SmtpPort)
        {
            EnableSsl = configuration.UseSsl,
            Credentials = string.IsNullOrWhiteSpace(configuration.Username)
                ? CredentialCache.DefaultNetworkCredentials
                : new NetworkCredential(configuration.Username, configuration.Password)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(string.IsNullOrWhiteSpace(configuration.SenderAddress)
                ? configuration.RecipientAddress
                : configuration.SenderAddress),
            Subject = $"New demo request from {request.CompanyName}",
            Body = BuildBody(request),
            IsBodyHtml = false
        };

        message.To.Add(configuration.RecipientAddress);

        using var registration = cancellationToken.Register(() => client.SendAsyncCancel());
        await client.SendMailAsync(message, cancellationToken);
    }

    private static string BuildBody(DemoRequest request)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Company: {request.CompanyName}");
        builder.AppendLine($"Contact: {request.ContactName}");
        builder.AppendLine($"Email: {request.ContactEmail}");
        builder.AppendLine($"Requested At (UTC): {request.RequestedAt:u}");

        if (!string.IsNullOrWhiteSpace(request.Message))
        {
            builder.AppendLine();
            builder.AppendLine("Message:");
            builder.AppendLine(request.Message);
        }

        return builder.ToString();
    }
}
