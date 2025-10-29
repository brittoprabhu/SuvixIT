using Microsoft.EntityFrameworkCore;
using SuvixIT.Api.Data;
using SuvixIT.Api.DTOs;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Services;

public class DemoRequestService : IDemoRequestService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailSender _emailSender;

    public DemoRequestService(ApplicationDbContext dbContext, IEmailSender emailSender)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
    }

    public async Task<DemoRequest> CreateRequestAsync(DemoRequestCreateDto dto, CancellationToken cancellationToken = default)
    {
        var request = new DemoRequest
        {
            CompanyName = dto.CompanyName,
            ContactName = dto.ContactName,
            ContactEmail = dto.ContactEmail,
            Message = dto.Message,
            RequestedAt = DateTime.UtcNow
        };

        await _dbContext.DemoRequests.AddAsync(request, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var configuration = await _dbContext.EmailConfigurations.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (configuration is not null)
        {
            await _emailSender.SendDemoRequestNotificationAsync(configuration, request, cancellationToken);
        }

        return request;
    }

    public Task<DemoRequest?> GetRequestAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.DemoRequests.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
