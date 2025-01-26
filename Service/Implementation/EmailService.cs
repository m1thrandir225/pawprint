// Repository Layer: Concrete Email Service

using System.Net;
using System.Net.Mail;
using Domain;
using Domain.enums;
using Microsoft.Extensions.Logging;
using Service.Interface;

public class SmtpEmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(
        EmailSettings emailSettings, 
        IEmailTemplateService emailTemplateService,
        ILogger<SmtpEmailService> logger)
    {
        _emailSettings = emailSettings;
        _emailTemplateService = emailTemplateService;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        try
        {
            using var client = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = _emailSettings.UseSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = emailMessage.Subject,
                Body = emailMessage.Body,
                IsBodyHtml = true
            };
            
            mailMessage.To.Add(emailMessage.ToEmail);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation($"Email sent to {emailMessage.ToEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Email sending failed: {ex.Message}");
            throw;
        }
    }

    public async Task SendRegistrationConfirmationAsync(string toEmail, UserType userType)
    {
        var templateData = new Dictionary<string, string>
        {
            {"FirstName", "User"},
            {"VerificationLink", "https://pawprint.com/verify"}
        };

        var emailBody = _emailTemplateService.GenerateEmailTemplate(
            userType == UserType.Shelter 
                ? EmailTemplateType.ShelterRegistration 
                : EmailTemplateType.UserRegistration, 
            templateData
        );

        var emailMessage = new EmailMessage
        {
            ToEmail = toEmail,
            Subject = "Welcome to PawPrint!",
            Body = emailBody,
            TemplateType = userType == UserType.Shelter 
                ? EmailTemplateType.ShelterRegistration 
                : EmailTemplateType.UserRegistration
        };

        await SendEmailAsync(emailMessage);
    }

    public async Task SendPetListingAdoptionNotificationAsync(string toEmail, PetListingType listingType)
    {
        var templateData = new Dictionary<string, string>
        {
            {"PetName", "Fluffy"},
            {"ListingType", listingType.ToString()}
        };

        var emailBody = _emailTemplateService.GenerateEmailTemplate(
            EmailTemplateType.PetListingAdoption, 
            templateData
        );

        var emailMessage = new EmailMessage
        {
            ToEmail = toEmail,
            Subject = "Pet Listing Adoption Update",
            Body = emailBody,
            TemplateType = EmailTemplateType.PetListingAdoption
        };

        await SendEmailAsync(emailMessage);
    }
}