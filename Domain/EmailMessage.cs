using Domain.enums;

namespace Domain;

public class EmailMessage
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public EmailTemplateType TemplateType { get; set; }
}