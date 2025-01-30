using Domain.enums;

namespace Service.Interface;

public interface IEmailTemplateService
{
    string GenerateEmailTemplate(EmailTemplateType templateType, Dictionary<string, string> templateData);
}