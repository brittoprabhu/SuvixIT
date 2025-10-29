using SuvixIT.Api.Models;

namespace SuvixIT.Api.Services;

public interface IEmailSender
{
    Task SendDemoRequestNotificationAsync(EmailConfiguration configuration, DemoRequest request, CancellationToken cancellationToken = default);
}
