using Microsoft.AspNetCore.Mvc;
using SuvixIT.Api.DTOs;
using SuvixIT.Api.Services;

namespace SuvixIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IEmailConfigurationService _configurationService;

    public AdminController(IEmailConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet("email-configuration")]
    public async Task<ActionResult<EmailConfigurationViewModel>> GetEmailConfiguration(CancellationToken cancellationToken)
    {
        var config = await _configurationService.GetConfigurationAsync(cancellationToken);
        if (config is null)
        {
            return NotFound();
        }

        return config;
    }

    [HttpPut("email-configuration")]
    public async Task<IActionResult> UpdateEmailConfiguration([FromBody] EmailConfigurationDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        await _configurationService.UpsertConfigurationAsync(dto, cancellationToken);
        return NoContent();
    }
}
