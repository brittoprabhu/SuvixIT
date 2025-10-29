using SuvixIT.Api.DTOs;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Services;

public interface IDemoRequestService
{
    Task<DemoRequest> CreateRequestAsync(DemoRequestCreateDto dto, CancellationToken cancellationToken = default);
    Task<DemoRequest?> GetRequestAsync(Guid id, CancellationToken cancellationToken = default);
}
