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

    public async Task SendPetListingAdoptionNotificationAsync(string toEmail, PetListingType petListingType, object petListing)
    {
        var templateData = new Dictionary<string, string>();

        if (petListingType == PetListingType.OwnerPetListing)
        {
            if (petListing is OwnerPetListing ownerPetListing)
            {
                templateData["PetName"] = ownerPetListing.Pet.Name;
                templateData["Owner"] = ownerPetListing.Adopter.FirstName;
            }
        }
        else if (petListingType == PetListingType.ShelterPetListing)
        {
            if (petListing is ShelterPetListing shelterPetListing)
            {
                templateData["PetName"] = shelterPetListing.Pet.Name;
                templateData["ShelterName"] = shelterPetListing.Shelter.Name;
            }
        }

        var emailBody = _emailTemplateService.GenerateEmailTemplate(
            EmailTemplateType.PetListingAdoption, 
            templateData
        );

        var emailMessage = new EmailMessage
        {
            ToEmail = toEmail,
            Subject = "Your Pet has been listed successfully! ",
            Body = emailBody,
            TemplateType = EmailTemplateType.PetListingAdoption
        };

        await SendEmailAsync(emailMessage);
    }

    public async Task SendAdoptionApprovalNotificationAsync(string toEmail, PetListingType petListingType, object petListing)
    {
        var templateData = new Dictionary<string, string>();

        if (petListingType == PetListingType.OwnerPetListing && petListing is OwnerPetListing ownerPetListing)
        {
            templateData = new Dictionary<string, string>
            {
                {"AdopterName", ownerPetListing.Adopter.FirstName},
                {"PetName", ownerPetListing.Pet.Name},
                {"HandoverDate", DateTime.Today.ToString()} 
            };
        }
        else if (petListingType == PetListingType.ShelterPetListing && petListing is ShelterPetListing shelterPetListing)
        {
            templateData = new Dictionary<string, string>
            {
                {"ShelterName", shelterPetListing.Shelter.Name},
                {"PetName", shelterPetListing.Pet.Name},
                {"HandoverDate", DateTime.Today.ToString()}
            };
        }
        else
        {
            throw new ArgumentException("Invalid pet listing type or object.");
        }

        var emailBody = _emailTemplateService.GenerateEmailTemplate(
                EmailTemplateType.AdoptionApproval, templateData);

            var emailMessage = new EmailMessage
            {
                ToEmail = toEmail,
                Subject = "Your Adoption Request Has Been Approved!",
                Body = emailBody,
                TemplateType = EmailTemplateType.AdoptionApproval
            };

            await SendEmailAsync(emailMessage);
        }
    }
    