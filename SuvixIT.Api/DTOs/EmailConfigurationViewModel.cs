namespace SuvixIT.Api.DTOs;

public class EmailConfigurationViewModel
{
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string Username { get; set; } = string.Empty;
    public string SenderAddress { get; set; } = string.Empty;
    public string RecipientAddress { get; set; } = string.Empty;
    public bool UseSsl { get; set; }
}
