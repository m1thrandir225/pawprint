using Domain;

namespace Service.Interface;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
    Task SendRegistrationConfirmationAsync(string toEmail, UserType userType);
    Task SendPetListingAdoptionNotificationAsync(string toEmail, PetListingType listingType);
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