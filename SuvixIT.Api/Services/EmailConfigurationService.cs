using Microsoft.EntityFrameworkCore;
using SuvixIT.Api.Data;
using SuvixIT.Api.DTOs;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Services;

public class EmailConfigurationService : IEmailConfigurationService
{
    private readonly ApplicationDbContext _dbContext;

    public EmailConfigurationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmailConfigurationViewModel?> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        var config = await _dbContext.EmailConfigurations.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        if (config is null)
        {
            return null;
        }

        return new EmailConfigurationViewModel
        {
            SmtpHost = config.SmtpHost,
            SmtpPort = config.SmtpPort,
            Username = config.Username,
            SenderAddress = config.SenderAddress,
            RecipientAddress = config.RecipientAddress,
            UseSsl = config.UseSsl
        };
    }

    public async Task<EmailConfiguration> UpsertConfigurationAsync(EmailConfigurationDto dto, CancellationToken cancellationToken = default)
    {
        var config = await _dbContext.EmailConfigurations.FirstOrDefaultAsync(cancellationToken);

        if (config is null)
        {
            config = new EmailConfiguration();
            _dbContext.EmailConfigurations.Add(config);
        }

        config.SmtpHost = dto.SmtpHost;
        config.SmtpPort = dto.SmtpPort;
        config.Username = dto.Username;
        config.Password = dto.Password;
        config.SenderAddress = dto.SenderAddress;
        config.RecipientAddress = dto.RecipientAddress;
        config.UseSsl = dto.UseSsl;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return config;
    }
}
