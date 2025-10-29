using System.ComponentModel.DataAnnotations;

namespace SuvixIT.Api.Models;

public class EmailConfiguration
{
    [Key]
    public int Id { get; set; }

    [MaxLength(200)]
    public string SmtpHost { get; set; } = string.Empty;

    public int SmtpPort { get; set; } = 25;

    [MaxLength(200)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Password { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(200)]
    public string SenderAddress { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(200)]
    public string RecipientAddress { get; set; } = string.Empty;

    public bool UseSsl { get; set; } = true;
}
