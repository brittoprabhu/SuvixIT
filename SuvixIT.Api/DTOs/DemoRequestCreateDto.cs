using System.ComponentModel.DataAnnotations;

namespace SuvixIT.Api.DTOs;

public class DemoRequestCreateDto
{
    [Required]
    [MaxLength(200)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ContactName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Message { get; set; }
}
