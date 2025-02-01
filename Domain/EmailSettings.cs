namespace Domain;

public class EmailSettings
{
    public string Host { get; set; } = Environment.GetEnvironmentVariable("SMTP_HOST") ?? string.Empty;
    public int Port { get; set; } = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var port) ? port : 2587;
    public string SenderName { get; set; } = Environment.GetEnvironmentVariable("SMTP_SENDER_NAME") ?? string.Empty;
    public string SenderEmail { get; set; } = Environment.GetEnvironmentVariable("SMTP_SENDER_EMAIL") ?? string.Empty;
    public string Username { get; set; } = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? string.Empty;
    public string Password { get; set; } = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? string.Empty;
    public bool UseSsl { get; set; } = true;
}