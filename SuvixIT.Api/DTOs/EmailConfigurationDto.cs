using System.ComponentModel.DataAnnotations;

namespace SuvixIT.Api.DTOs;

public class EmailConfigurationDto
{
    [Required]
    [MaxLength(200)]
    public string SmtpHost { get; set; } = string.Empty;

    [Range(1, 65535)]
    public int SmtpPort { get; set; } = 25;

    [MaxLength(200)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string SenderAddress { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string RecipientAddress { get; set; } = string.Empty;

    public bool UseSsl { get; set; } = true;
}
