using Microsoft.AspNetCore.Mvc;
using SuvixIT.Api.DTOs;
using SuvixIT.Api.Models;
using SuvixIT.Api.Services;

namespace SuvixIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemoRequestsController : ControllerBase
{
    private readonly IDemoRequestService _demoRequestService;

    public DemoRequestsController(IDemoRequestService demoRequestService)
    {
        _demoRequestService = demoRequestService;
    }

    [HttpPost]
    public async Task<ActionResult<DemoRequest>> SubmitDemoRequest([FromBody] DemoRequestCreateDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var request = await _demoRequestService.CreateRequestAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetDemoRequest), new { id = request.Id }, request);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DemoRequest>> GetDemoRequest(Guid id, CancellationToken cancellationToken)
    {
        var request = await _demoRequestService.GetRequestAsync(id, cancellationToken);
        if (request is null)
        {
            return NotFound();
        }

        return request;
    }
}
