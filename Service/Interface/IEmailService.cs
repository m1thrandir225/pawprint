using Domain;

namespace Service.Interface;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailMessage emailMessage);
    Task<bool> SendRegistrationConfirmationAsync(string toEmail, UserType userType);
    Task<bool> SendPetListingAdoptionNotificationAsync(string toEmail, PetListingType petListingType, object petListing);
    Task<bool> SendAdoptionApprovalNotificationAsync(string toEmail, PetListingType petListingType, object petListing);
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