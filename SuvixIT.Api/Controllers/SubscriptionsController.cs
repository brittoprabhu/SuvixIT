using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuvixIT.Api.Data;
using SuvixIT.Api.DTOs;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public SubscriptionsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<Subscription>> CreateSubscription([FromBody] SubscriptionCreateDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var existing = await _dbContext.Subscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Email == dto.Email, cancellationToken);

        if (existing is not null)
        {
            return Conflict($"Subscription already exists for {dto.Email}.");
        }

        var subscription = new Subscription
        {
            Email = dto.Email,
            FullName = dto.FullName,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Subscriptions.AddAsync(subscription, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetSubscription), new { id = subscription.Id }, subscription);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Subscription>> GetSubscription(Guid id, CancellationToken cancellationToken)
    {
        var subscription = await _dbContext.Subscriptions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (subscription is null)
        {
            return NotFound();
        }

        return subscription;
    }
}
