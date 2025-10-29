using SuvixIT.Api.DTOs;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Services;

public interface IEmailConfigurationService
{
    Task<EmailConfigurationViewModel?> GetConfigurationAsync(CancellationToken cancellationToken = default);
    Task<EmailConfiguration> UpsertConfigurationAsync(EmailConfigurationDto dto, CancellationToken cancellationToken = default);
}
