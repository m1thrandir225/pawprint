using Domain;

namespace Service.Interface;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
    Task SendRegistrationConfirmationAsync(string toEmail, UserType userType);
    Task SendPetListingAdoptionNotificationAsync(string toEmail, PetListingType petListingType, object petListing);
    Task SendAdoptionApprovalNotificationAsync(string toEmail, PetListingType petListingType, object petListing);
}

public enum UserType
{
    Shelter,
    Adopter
}

public enum PetListingType
{
    ShelterPetListing,
    OwnerPetListing
}