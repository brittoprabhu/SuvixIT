using System.ComponentModel.DataAnnotations;

namespace SuvixIT.Api.DTOs;

public class SubscriptionCreateDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? FullName { get; set; }
}
