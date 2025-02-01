// Repository Layer: Concrete Email Service

using Domain;
using Domain.enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Service.Interface;


public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly IEmailTemplateService _emailTemplateService;

    public EmailService(
        EmailSettings emailSettings, 
        IEmailTemplateService emailTemplateService)
    {
        _emailSettings = emailSettings;
        _emailTemplateService = emailTemplateService;
    }

    public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        try
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress(emailMessage.ToEmail, emailMessage.ToEmail));
            message.Subject = emailMessage.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = emailMessage.Body
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable);
            client.Authenticate(_emailSettings.Username, _emailSettings.Password);
            client.Send(message);
            client.Disconnect(true);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public  async Task<bool> SendRegistrationConfirmationAsync(string toEmail, UserType userType)
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

        return await SendEmailAsync(emailMessage);
    }

    public async Task<bool> SendPetListingAdoptionNotificationAsync(string toEmail, PetListingType petListingType, object petListing)
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

        return await SendEmailAsync(emailMessage);
    }

    public async Task<bool> SendAdoptionApprovalNotificationAsync(string toEmail, PetListingType petListingType, object petListing)
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

            return await SendEmailAsync(emailMessage);
        }
    }
    